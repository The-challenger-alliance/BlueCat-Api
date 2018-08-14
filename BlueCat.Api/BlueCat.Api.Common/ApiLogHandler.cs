using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
               // LogRecorder.BeginMonitor(request.RequestUri.ToString());
                RecordRequestInfo(request, cancellationToken);
                var result = base.SendAsync(request, cancellationToken);
                result.ContinueWith((task, state) => RecordResponseInfo(task.Result), null,
                    TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.ExecuteSynchronously);

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

                return result;
            }
            catch (Exception)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
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

            //your logs

            //            var args = new StringBuilder();
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
                //LogRecorder.MonitorTrace($"Result：{task.Result}");
            }
            catch (Exception e)
            {
                //LogRecorder.MonitorTrace($"Result：{e.Message}");
            }
            //LogRecorder.EndMonitor();
        }

        /// <summary>
        /// 异常输出
        /// </summary>
        public class ExceptionResponse
        {
        }
    }
}
