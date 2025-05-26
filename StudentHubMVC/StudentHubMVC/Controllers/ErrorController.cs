using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound()
        {
            var statusCode = (int)System.Net.HttpStatusCode.NotFound;
            Response.StatusCode = statusCode;
            Response.TrySkipIisCustomErrors = true;
            HttpContext.Response.StatusCode = statusCode;
            HttpContext.Response.TrySkipIisCustomErrors = true;

            ViewBag.Title = "404 - StudentHub.in ";
            var content_Error404 = System.IO.File.ReadAllText(Server.MapPath("~/Views/Home/Content/404.html"));
            ViewBag.Content = content_Error404;

            if(Request.Url.ToString().ToLower().Contains("admin"))
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            return View("~/Views/Home/Error404.cshtml");
        }

        public ActionResult Error()
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            Response.TrySkipIisCustomErrors = true;

            ViewBag.Title = "404 - StudentHub.in";
            var content_Error404 = System.IO.File.ReadAllText(Server.MapPath("~/Views/Home/Content/404.html"));
            ViewBag.Content = content_Error404;

            return View("~/Views/Home/Error404.cshtml");
        }
    }
}