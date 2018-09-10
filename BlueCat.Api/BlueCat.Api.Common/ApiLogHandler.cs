using BlueCat.Api.Common.Context;
using BlueCat.Api.Common.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlueCat.Api.Common
{
    /// <summary>
    /// Http进站出站的日志记录
    /// </summary>
    public sealed class ApiLogHandler : DelegatingHandler
    {

        public ApiLogHandler()
        {
            LogRecorder.GetRequestIdFunc = () =>
            {

                if (ApiContext.RequestContext == null || ApiContext.RequestContext.RequestId == null)
                {
                    return Guid.NewGuid();
                }

                return ApiContext.RequestContext.RequestId;

                //return Guid.NewGuid();
            };
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                UserProfile userProfile = new UserProfile();

                this.CreateApiContext(userProfile);

                //设置全局变量
                ApiContext.SetRequestContext(new InternalCallContext
                {
                    RequestId = Guid.NewGuid(),
                    UserId = -2
                });

                // LogRecorder.BeginMonitor(request.RequestUri.ToString());
                RecordRequestInfo(request, cancellationToken);
                var result = base.SendAsync(request, cancellationToken);
                result.ContinueWith((task, state) => RecordResponseInfo(task.Result), null,
                    TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.ExecuteSynchronously).ContinueWith((task, state) =>
                    {
                        CallContext.LogicalSetData("ApiContext", null);

                    }, null, cancellationToken); ;
                //bool hasErrorCode = false;

                //string errorMessage = string.Empty;

                //result.ContinueWith<HttpResponseMessage>((responseToCompleteTask) =>
                //{
                //    HttpResponseMessage response = responseToCompleteTask.Result;
                //    HttpError error = null;
                //    if (response.TryGetContentValue<HttpError>(out error))
                //    {
                //        //添加自定义错误处理
                //        //error.Message = "Your Customized Error Message";

                //        hasErrorCode = true;
                //    }

                //    if (error != null)
                //    {
                //        errorMessage = error.Message;
                //        ////获取抛出自定义异常，有拦截器统一解析
                //        //throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                //        //{
                //        //    //封装处理异常信息，返回指定JSON对象
                //        //    //Content = new StringContent(JsonHelper.ToJson(new ErrorModel(404, 0, error.Message)), Encoding.UTF8, "application/json"),
                //        //    Content = new StringContent(error.Message, Encoding.UTF8, "application/json"),
                //        //    ReasonPhrase = "Exception"
                //        //});


                //        var tsc = new TaskCompletionSource<HttpResponseMessage>();
                //        tsc.SetResult(response);


                //        return response;
                //    }

                //    return response;

                //});

                LogRecorder.Record("", LogType.Message,true);

                return result;
            }
            catch (Exception)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    CallContext.LogicalSetData("ApiContext", null);
                    LogRecorder.Record("", LogType.Message, true);
                    return request.ToResponse(ApiResult<string>.ErrorResult(ErrorCode.NetworkError, "网络异常"));
                });
            }
        }

        /// <summary>
        /// 记录API请求
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        private void RecordRequestInfo(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            #region .net core
            //             var args = new StringBuilder();
            //            args.Append("Headers：");
            //            foreach (var head in request.Headers)
            //            {
            //                args.Append($"【{head.Key}】{head.Value.LinkToString('|')}");
            //            }
            //            LogRecorder.MonitorTrace(args.ToString());
            //            LogRecorder.MonitorTrace($"Method：{request.Method}");

            //            LogRecorder.MonitorTrace($"QueryString：{request.RequestUri.Query}");

            //            StringBuilder code = new StringBuilder();
            //            if (request.Method == HttpMethod.Get)
            //            {
            //                code.Append($@"
            //                {{
            //                    caller.Bear = ""{ExtractToken(request)}"";
            //                    var result = caller.Get/*<>*/(""{request.RequestUri}"");
            //                    Console.WriteLine(JsonConvert.SerializeObject(result));
            //                }}");
            //            }
            //            else
            //            {
            //                var task = request.Content.ReadAsStringAsync();
            //                task.Wait(cancellationToken);
            //                LogRecorder.MonitorTrace($"Content：{task.Result}");
            //                code.Append($@"
            //                {{
            //                    caller.Bear = ""{ExtractToken(request)}"";
            //                    var result = caller.Post/*<>*/(""{request.RequestUri}"", new Dictionary<string, string>
            //                    {{");
            //                var di = FormatParams(task.Result);
            //                foreach (var item in di)
            //                {
            //                    code.Append($@"
            //                        {{""{item.Key}"",""{item.Value}""}},");
            //                }
            //                code.Append($@"
            //                    }});
            //                    Console.WriteLine(JsonConvert.SerializeObject(result));
            //                }}");
            //            }
            //            LogRecorder.Record(code.ToString(), LogType.Message);

            #endregion

            var args = new StringBuilder();
            args.Append("Headers：");
            foreach (var head in request.Headers)
            {
                args.Append(string.Format("【{0}】{1}", head.Key, head.Value.LinkToString('|')));
            }
            LogRecorder.MonitorTrace(args.ToString());

            LogRecorder.MonitorTrace(String.Format("Method：{0}", request.Method));

            LogRecorder.MonitorTrace(string.Format("QueryString：{0}", request.RequestUri.Query));

            StringBuilder code = new StringBuilder();

            if (request.Method == HttpMethod.Get)
            {
                code.Append(string.Format(@"{{
    Api.Bear = ""{0}"";
    Api Method = Get/*<>*/(""{1}"");
}}", ExtractToken(request), request.RequestUri));
            }
            else
            {
                var task = request.Content.ReadAsStringAsync();
                task.Wait(cancellationToken);
                //string.Format("Content：{0}",task.Result);
                LogRecorder.MonitorTrace(string.Format("Content：{0}", task.Result));

                code.Append(string.Format(@"
                {{
                    caller.Bear = ""{0}"";
                    var result = caller.Post/*<>*/(""{1}"", new Dictionary<string, string>
                    {{", ExtractToken(request), request.RequestUri));

                var di = FormatParams(task.Result);


                foreach (var item in di)
                {

                    code.Append(string.Format(@" 
                                            {{""{0}"",""{1}""}},", item.Key, item.Value));
                }
                code.Append(string.Format(@"
                    }});
                    Console.WriteLine(JsonConvert.SerializeObject(result));
                }}"));
            }

            LogRecorder.Record(code.ToString(), LogType.Message);
        }

        /// <summary>
        /// 记录API返回
        /// </summary>
        /// <param name="response"></param>
        private static void RecordResponseInfo(HttpResponseMessage response)
        {
            try
            {
                var task = response.Content.ReadAsStringAsync();
                task.Wait();
                LogRecorder.Record(string.Format("Result：{0}", task.Result));
            }
            catch (Exception e)
            {
                LogRecorder.MonitorTrace(string.Format("Result：{0}", e.Message));
            }
            LogRecorder.EndMonitor();
        }

        /// <summary>
        /// 取请求头的身份验证令牌
        /// </summary>
        /// <returns></returns>
        private string ExtractToken(HttpRequestMessage request)
        {
            const string bearer = "Bearer";
            var authz = request.Headers.Authorization;
            if (authz != null)
                return string.Equals(authz.Scheme, bearer, StringComparison.OrdinalIgnoreCase) ? authz.Parameter : null;
            if (!request.Headers.Contains("Authorization"))
                return null;
            string au = request.Headers.GetValues("Authorization").FirstOrDefault();
            if (au == null)
                return null;
            var aus = au.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (aus.Length < 2 || aus[0] != bearer)
                return null;
            return aus[1];
        }

        /// <summary>
        /// 参数格式化
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private Dictionary<string, string> FormatParams(string args)
        {
            if (string.IsNullOrWhiteSpace(args))
                return new Dictionary<string, string>();
            var result = new Dictionary<string, string>();
            var kw = args.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            if (kw.Length == 0)
                return result;
            foreach (var item in kw)
            {
                var words = item.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                switch (words.Length)
                {
                    case 0:
                        continue;
                    case 1:
                        result.Add(words[0], null);
                        continue;
                    default:
                        result.Add(words[0], words[1]);
                        continue;
                }
            }
            return result;
        }

        /// <summary>
        /// 异常输出
        /// </summary>
        public class ExceptionResponse
        {
        }

        private void CreateApiContext(UserProfile customer)
        {
            ApiContext.SetCustomer(customer);
            ApiContext.SetRequestContext(new InternalCallContext
            {
                RequestId = Guid.NewGuid(),
                //ServiceKey = GlobalVariable.ServiceKey,
                UserId = customer.UserId,
                ThreadId = Thread.CurrentThread.ManagedThreadId
            });
        }
    }
}
