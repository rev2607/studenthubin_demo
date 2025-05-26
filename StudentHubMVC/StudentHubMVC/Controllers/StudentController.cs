using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Controllers
{
    public class StudentController : Controller
    {
        private void LogoutCheck()
        {
            if (Session["StudentID"] == null || Session["StudentID"].ToString() == "")
            {
                Logout();
            }
        }


        public ActionResult Logout()
        {
            Session.RemoveAll();
            return Redirect("/Login");
            //throw new Exception("Unauthorized Access");

        }


        // GET: Dashboard
        public ActionResult Dashboard()
        {
            if (Session["StudentID"] == null || Session["StudentID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ProfilePic = Session["StudentProfilePic"].ToString();

            Liason.Students obj_Student = new Liason.Students();
            DataSet ds_Student = obj_Student.GetStudentDashboard(Session["StudentID"].ToString());
            
            return View(ds_Student);
        }


        // GET: Dashboard/batchregister/{id}
        public ActionResult BatchResult(string BatchId)
        {
            if (Session["StudentID"] == null || Session["StudentID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ProfilePic = Session["StudentProfilePic"].ToString();

            Liason.Students obj_Student = new Liason.Students();

            string Response = obj_Student.RegisterCourseBatch(BatchId, Session["StudentID"].ToString());
            int ID;
            if(int.TryParse(Response, out ID))
            {
                Response = "Registered to Batch";
            }

            ViewBag.Response = Response;

            DataSet ds_Student = obj_Student.GetStudentDashboard(Session["StudentID"].ToString());

            return View("Dashboard", ds_Student);
        }


        // GET: Profile
        public ActionResult Profile()
        {
            if (Session["StudentID"] == null || Session["StudentID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ProfilePic = Session["StudentProfilePic"].ToString();

            Liason.Students obj_Student = new Liason.Students();

            ViewData.Model = obj_Student.EditStudent(Session["StudentID"].ToString());

            ViewBag.Title = "Edit Profile";

            return View();
        }


        // GET: Profile SAVE
        [HttpPost]
        public ActionResult Profile(FormCollection collection)
        {

            if (Session["StudentID"] == null || Session["StudentID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ProfilePic = Session["StudentProfilePic"].ToString();

            try
            {
                HttpPostedFileBase file = Request.Files["ProfilePicUpload"];

                // TODO: Add update logic here
                Liason.Students obj_Student = new Liason.Students();

                string Response = obj_Student.ProcessData_Student("UPDATE", Session["StudentID"].ToString(), collection, file);

                ViewBag.SaveMessage = Response;

                ViewData.Model = obj_Student.EditStudent(Session["StudentID"].ToString());

                ViewBag.Title = "Edit Profile";

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

            return View();
        }

        
    }
}