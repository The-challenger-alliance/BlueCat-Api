using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BlueCat.Api
{
    public class BlueCatValidationFilter :ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            //to do -- check token 
            base.OnActionExecuting(filterContext);
          
        } 
    }
}