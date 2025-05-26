using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentHubMVC
{
    public class Auth : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //base.OnAuthorization(filterContext);

            // Check for authorization
            if (HttpContext.Current.Session["ID"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Admin", action = "LoginAdmin" }));
                //filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}