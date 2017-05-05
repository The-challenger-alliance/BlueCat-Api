using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BlueCat.Api
{
    public class BlueCatValidationFilter :ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            base.OnActionExecuting(filterContext);
          
        } 
    }
}