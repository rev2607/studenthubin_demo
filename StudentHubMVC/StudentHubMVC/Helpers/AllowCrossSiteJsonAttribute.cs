﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Http.Filters;
using System.Web.Mvc;

namespace StudentHubMVC.Helpers
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            base.OnActionExecuting(filterContext);
        }
    }
}