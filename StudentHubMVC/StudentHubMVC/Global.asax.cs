﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentHubMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AutoMapperConfiguration.Configure();    
        }


        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    Response.StatusCode = 404;
        //    Response.TrySkipIisCustomErrors = true;

        //}
    }
}
