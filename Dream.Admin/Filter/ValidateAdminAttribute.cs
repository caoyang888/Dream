using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NiceCode;

namespace Dream.Admin
{
    public class ValidateAdminAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["admin"] == null)
            {
                RouteValueDictionary dictionary = new RouteValueDictionary
                (new
                {
                    controller = "Portal",
                    action = "SignIn",
                    returnUrl = filterContext.HttpContext.Request.RawUrl
                });
                filterContext.Result = new RedirectToRouteResult(dictionary);
            }
        }
    }
}