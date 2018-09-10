using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueCat.Api.Common
{
    public interface IApiResultData
    {
    }

    public interface IApiResult
    {
        /// <summary>
        /// api 调用是否成功
        /// </summary>
        bool success { get; set; }

        /// <summary>
        /// API 状态码
        /// </summary>
        string resultCode { get; set; }

        /// <summary>
        /// API 内部描述
        /// </summary>
        string resultDesc { get; set; }

        /// <summary>
        /// 弹框消息
        /// </summary>
        string message { get; set; }
    }


    public class ApiResult : IApiResult
    {
        public bool success { get; set; }

        /// <summary>
        /// API 状态码
        /// </summary>
        public string resultCode { get; set; }

        /// <summary>
        /// API 内部描述
        /// </summary>
        public string resultDesc { get; set; }

        /// <summary>
        /// 弹框消息
        /// </summary>
        public string message { get; set; }

        public static ApiResult Succees()
        {
            return new ApiResult
            {
                success = true,
                resultDesc = string.Empty,
                resultCode = string.Empty,
                message = string.Empty
            };
        }

        public static ApiResult Succees(string code)
        {
            return new ApiResult
            {
                success = true,
                resultDesc = string.Empty,
                resultCode = code,
                message = string.Empty
            };
        }
    }

    public class ApiResult<TData> : IApiResult where TData : class
    {
        public bool success { get; set; }

        /// <summary>
        /// API 状态码
        /// </summary>
        public string resultCode { get; set; }

        /// <summary>
        /// API 内部描述
        /// </summary>
        public string resultDesc { get; set; }

        /// <summary>
        /// 弹框消息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 返回内容
        /// </summary>
        public TData resultData { get; set; }

        /// <summary>
        /// 生成一个成功的标准返回
        /// </summary>
        /// <returns></returns>
        public static ApiResult<TData> Succees(string code, TData data)
        {
            return new ApiResult<TData>
            {
                success = true,
                resultData = null,
                resultDesc = string.Empty,
                resultCode = code,
                message = string.Empty
            };
        }
        /// <summary>
        /// 生成一个包含错误码的标准返回
        /// </summary>
        /// <param name="errCode">错误码</param>
        /// <returns></returns>
        public static ApiResult<TData> ErrorResult(string errCode, string desc)
        {
            return new ApiResult<TData>
            {
                success = false,
                resultData = null,
                resultDesc = desc,
                resultCode = errCode,
                message = string.Empty
            };
        }
        /// <summary>
        /// 生成一个包含错误码的标准返回
        /// </summary>
        /// <param name="errCode">错误码</param>
        /// <param name="desc">内部描述信息</param>
        /// <param name="message">弹出信息</param>
        /// <returns></returns>
        public static ApiResult<TData> ErrorResult(string errCode, string desc, string message)
        {
            return new ApiResult<TData>
            {
                success = false,
                resultData = null,
                resultDesc = desc,
                resultCode = "",
                message = message
            };
        }
    }
}
