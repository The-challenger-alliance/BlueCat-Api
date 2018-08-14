using BlueCat.Api.App_Start;
using BlueCat.Api.Common;
using BlueCat.Api.Filter;
using FluentValidation.Attributes;
using FluentValidation.Mvc;
using System.ComponentModel.Composition.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlueCat.Api
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //鉴权应该是在gateway去做
            //GlobalConfiguration.Configuration.MessageHandlers.Add(new BearerHandler());
            GlobalConfiguration.Configuration.Filters.Add(new ApiExceptionFilterAttribute());
            //全局日志
            GlobalConfiguration.Configuration.MessageHandlers.Add(new ApiLogHandler());

            //添加到全局配置
         

            AutoFacConfig.Register();
        }

    }
}