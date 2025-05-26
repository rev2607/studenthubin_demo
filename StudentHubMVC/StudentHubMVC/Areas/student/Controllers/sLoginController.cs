using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Areas.student.Controllers
{
    public class sLoginController : Controller
    {
        StudentHubMVC.Liason.Students obj_Student = new Liason.Students();


        // GET: student/Login
        public ActionResult Index()
        {
            return View();
        }


        // POST: student/Login
        [HttpPost]
        public ActionResult Index(string email, string password, string rememberme)
        {
            string Response = obj_Student.StudentLogin(email, password);

            if (Response == "SUCCESS")
                return RedirectToAction("Index", "Portal");

            ViewBag.Message = Response;

            return View();
        }
    }
}