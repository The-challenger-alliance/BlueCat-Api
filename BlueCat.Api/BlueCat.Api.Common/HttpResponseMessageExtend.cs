using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlueCat.Api.Common
{
    /// <summary>
    /// HttpResponseMessage 扩展对象
    /// </summary>
    public static class HttpResponseMessageExtend
    {

        /// <summary>
        /// 生成一个标准返回对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>HttpResponseMessage对象</returns>
        /// <param name="statusCode">HTTP状态码</param>
        public static ApiResponseMessage ToResponse(this HttpRequestMessage request, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiResponseMessage(statusCode)
            {
                RequestMessage = request
            };
        }

        /// <summary>
        /// 生成一个标准返回对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>HttpResponseMessage对象</returns>
        /// <param name="statusCode">HTTP状态码</param>
        public static ApiResponseMessage<ApiResult<TResult>> ToResponse<TResult>(this HttpRequestMessage request, ApiResult<TResult> result, HttpStatusCode statusCode = HttpStatusCode.OK)
             where TResult : class
        {

            if (result == null)
            {
                return new ApiResponseMessage<ApiResult<TResult>>(statusCode)
                {
                    RequestMessage = request
                };
            }

            var response = new ApiResponseMessage<ApiResult<TResult>>(statusCode)
            {
                RequestMessage = request,
                Content = new StringContent(JsonConvert.SerializeObject(result))
            };

            return response;
        }
    }

    /// <summary>
    /// API返回专用的ResponseMessage
    /// </summary>
    public class ApiResponseMessage: HttpResponseMessage
    {
        protected static readonly string SuccessMessage;

        static ApiResponseMessage()
        {
            SuccessMessage = JsonConvert.SerializeObject(ApiResult.Succees());
        }

        /// <summary>
        /// 默认构造（状态码为200
        /// </summary>
        public ApiResponseMessage()
            : base(HttpStatusCode.OK)
        {
            Content = new StringContent(SuccessMessage);
        }

        /// <summary>
        /// 状态构造
        /// </summary>
        /// <param name="statusCode">状态</param>
        public ApiResponseMessage(HttpStatusCode statusCode)
            : base(statusCode)
        {
            Content = new StringContent(SuccessMessage);
        }
    }

    public class ApiResponseMessage<TResult> : ApiResponseMessage
        where TResult : IApiResult
    {
        public ApiResponseMessage()
            : base(HttpStatusCode.OK)
        {
            Content = new StringContent(SuccessMessage);
        }


        public ApiResponseMessage(HttpStatusCode statusCode)
            : base(statusCode)
        {
            Content = new StringContent(SuccessMessage);
        }
    }
}
