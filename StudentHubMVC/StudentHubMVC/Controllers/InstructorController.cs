using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Controllers
{
    public class InstructorController : Controller
    {
        private void LogoutCheck()
        {
            if (Session["InstructorID"] == null || Session["InstructorID"].ToString() == "")
            {
                Logout();
            }
        }


        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login", "Home");
        }


        // GET: Instructor
        public ActionResult Dashboard()
        {
            if (Session["InstructorID"] == null || Session["InstructorID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ProfilePic = Session["InstructorProfilePic"].ToString();

            Liason.Instructors obj_Instructor = new Liason.Instructors();
            ViewBag.InstructorBatches = obj_Instructor.GetInstructorBatches(Session["InstructorID"].ToString());

            return View();
        }


        // GET: /dashboard/batch/5
        public ActionResult BatchStudents(string BatchId)
        {
            if (Session["InstructorID"] == null || Session["InstructorID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ProfilePic = Session["InstructorProfilePic"].ToString();

            Liason.Instructors obj_Instructor = new Liason.Instructors();
            ViewBag.InstructorBatches = obj_Instructor.GetInstructorBatches(Session["InstructorID"].ToString());
            ViewBag.BatchStudents = obj_Instructor.GetInstructorBatchStudents(BatchId);

            return View("Dashboard");
        }


        // GET: Profile
        public ActionResult Profile()
        {
            if (Session["InstructorID"] == null || Session["InstructorID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ProfilePic = Session["InstructorProfilePic"].ToString();

            Liason.Instructors obj_Instructor = new Liason.Instructors();

            ViewData.Model = obj_Instructor.EditInstructor(Session["InstructorID"].ToString());

            ViewBag.Title = "Edit Profile";

            return View();
        }


        // GET: Profile SAVE
        [HttpPost]
        public ActionResult Profile(FormCollection collection)
        {
            if (Session["InstructorID"] == null || Session["InstructorID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ProfilePic = Session["InstructorProfilePic"].ToString();

            try
            {
                HttpPostedFileBase file = Request.Files["ProfilePicUpload"];

                // TODO: Add update logic here
                Liason.Instructors obj_Instructor = new Liason.Instructors();

                string Response = obj_Instructor.ProcessData_Instructor("UPDATE", Session["InstructorID"].ToString(), collection, file);

                ViewBag.SaveMessage = Response;

                ViewData.Model = obj_Instructor.EditInstructor(Session["InstructorID"].ToString());

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