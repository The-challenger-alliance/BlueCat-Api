using BlueCat.Api.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace BlueCat.Api.Filter
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //重写基类的异常处理方法
        //public override void OnException(HttpActionExecutedContext actionExecutedContext)
        //{
        //    //1.异常日志记录（正式项目里面一般是用log4net记录异常日志）
        //    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "——" +
        //        actionExecutedContext.Exception.GetType().ToString() + "：" + actionExecutedContext.Exception.Message + "——堆栈信息：" +
        //        actionExecutedContext.Exception.StackTrace);

        //    actionExecutedContext.Response = actionExecutedContext.Request.ToResponse(ApiResult<string>.ErrorResult(ErrorCode.NetworkError, actionExecutedContext.Exception.Message));

        //    base.OnException(actionExecutedContext);
        //}
    }
}