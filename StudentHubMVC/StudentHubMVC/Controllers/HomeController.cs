using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Data;
using StudentHubMVC.Models.ViewModels.Home;

namespace StudentHubMVC.Controllers
{
    public class HomeController : Controller
    {
        Liason.Help obj_Help = new Liason.Help();
        Entities db = new Entities();
        Liason.Front obj_front = new Liason.Front();

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "StudentHub.in - One Stop Course Training";

            VMHomeIndex home = new VMHomeIndex();

            home.Colleges = obj_front.GetFeaturedColleges();
            home.News = obj_front.GetLatestNewsAlerts();

            return View(home);
        }


        //public ActionResult Home()
        //{
        //    VMHomeIndex home = new VMHomeIndex();

        //    home.Streams = db.DropdownTypes.Where(s => s.GroupName == "COURSESTREAM").OrderBy(s => s.TypeValue).ToList(); //.Take(6) //.Include("shub_maincourses")
        //    home.Courses = db.MainCourses.ToList();
        //    home.News = db.NewsAlerts.ToList();
        //    home.Placements = db.Placements.ToList();
        //    home.Institutions = db.Institutions.Take(30).ToList();
        //    home.Banners = db.shub_settings.Where(s => s.set_Name == "Banner1" || s.set_Name == "Banner2" || s.set_Name == "Banner3" || s.set_Name == "Banner4" || s.set_Name == "Banner5").Select(s => s.set_Value).ToList();

        //    return View(home);
        //}


        [HttpPost]
        public ActionResult Home(FormCollection collection)
        {
            return LoginLogic(collection);
        }

        public ActionResult Courses()
        {
            ViewBag.Title = "Training Courses - StudentHub.in";

            //Liason.Front obj_Front = new Liason.Front();

            //ViewBag.Courses = obj_Front.GetAllCourses();

            return View();
        }


        public ActionResult Institutions()
        {
            ViewBag.Title = "Training Institutions - StudentHub.in";

            return View();
        }


        // LOGIN
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            return LoginLogic(collection);
        }


        // LOGIN LOGIC
        private ActionResult LoginLogic(FormCollection collection)
        {
            string LoginType = collection["logintype"];
            string ReturnView = "";
            string Response = "";
            System.Data.DataTable dt_LoginDetails = new System.Data.DataTable();

            string email = collection["loginemail"];

            if (LoginType == "STUDENT")
            {
                Liason.Students obj_Student = new Liason.Students();
                Response = obj_Student.StudentLogin(collection);

                if (Response == "SUCCESS")
                {
                    dt_LoginDetails = obj_Student.GetLoginDetails(email);
                }
            }

            else if (LoginType == "INSTRUCTOR")
            {
                Liason.Instructors obj_Instructor = new Liason.Instructors();
                Response = obj_Instructor.InstructorLogin(collection);

                if (Response == "SUCCESS")
                {
                    dt_LoginDetails = obj_Instructor.GetLoginDetails(email);
                }
            }
            

            // LOGIN SUCCESSFUL
            if (Response == "SUCCESS")
            {
                if (dt_LoginDetails.Rows.Count > 0)
                {
                    if (LoginType == "STUDENT")
                    {
                        Session["StudentFullName"] = dt_LoginDetails.Rows[0]["FullName"];
                        Session["StudentEmail"] = dt_LoginDetails.Rows[0]["Email"];
                        Session["StudentID"] = dt_LoginDetails.Rows[0]["ID"];
                        Session["StudentProfilePic"] = dt_LoginDetails.Rows[0]["ProfilePic"];
                        return RedirectToAction("Dashboard", "Student");
                    }

                    else if (LoginType == "INSTRUCTOR")
                    {
                        Session["InstructorFullName"] = dt_LoginDetails.Rows[0]["FullName"];
                        Session["InstructorEmail"] = dt_LoginDetails.Rows[0]["Email"];
                        Session["InstructorID"] = dt_LoginDetails.Rows[0]["ID"];
                        Session["InstructorProfilePic"] = dt_LoginDetails.Rows[0]["ProfilePic"];
                        return RedirectToAction("Dashboard", "Instructor");
                    }
                    
                }

                

                
            }
            else
            {
                ViewBag.Response = LoginType + " " + Response;

                ReturnView = "Login";
            }

            return View(ReturnView);
        }


        // Admin/studentregistration
        public ActionResult StudentRegister()
        {
            ViewBag.Title = "Student Registration - StudentHub.in";

            return View();
        }

        // POST: Admin/studentregistration
        [HttpPost]
        public ActionResult StudentRegister(FormCollection collection)
        {
            Liason.Students obj_Student = new Liason.Students();

            string Response = obj_Student.ProcessData_Student("ADD", "", collection, null);
            string ReturnView = "";

            if(Response == "STUDENT EMAIL ALREADY EXISTS")
            {
                ViewBag.Title = "Student Registration - StudentHub.in";
            }
            else
            {
                System.Data.DataTable dt_Data = obj_Student.GetVerificationCode(Response);

                try
                {
                    string URL = "http://studenthub.in/verifyemail/" + dt_Data.Rows[0]["VerificationCode"].ToString() + "/" + dt_Data.Rows[0]["ID"].ToString();

                    StringBuilder Body = new StringBuilder();
                    Body.AppendLine("<h2>VERIFICATION EMAIL - STUDENTHUB.IN</h2> <br><br>");
                    Body.AppendLine("<b>Please Follow the Below Link to Verify your Email or <a href=\"" + URL + "\">CLICK HERE..</a> </b><br><br>");
                    //Body.AppendLine();
                    Body.AppendLine("<b>Verification Link: </b><a href=\"" + URL + "\">" + URL + "</a> <br><br>");

                    obj_Help.send_email("Verfication Email from StudentHub.in from Website", Body.ToString(), dt_Data.Rows[0]["Email"].ToString(), "no-reply@studenthub.in");

                    //response = "SUBMITTED";
                }
                catch (Exception Ex)
                {
                    Response = "ERROR SENDING VERIFICATION EMAIL - CONTACT ADMINISTRATOR";
                    obj_Help.ExcpetionsHandling(Ex);
                }

                ReturnView = "RegistrationSuccessful";
            }

            ViewBag.SaveMessage = Response;

            return View(ReturnView);
        }


        public ActionResult CourseDetails(string coursename)
        {
            //var content_CourseDetails = "";

            //switch(coursename)
            //{
            //    case "fab" :
            //        content_CourseDetails = System.IO.File.ReadAllText(Server.MapPath(@"~/Views/Home/Content/fab.html"));
            //        ViewBag.Title = "FAB - StudentHub.in";
            //        break;

            //    case "hadoop":
            //        content_CourseDetails = System.IO.File.ReadAllText(Server.MapPath(@"~/Views/Home/Content/hadoop.html"));
            //        ViewBag.Title = "HADOOP - StudentHub.in";
            //        break;

            //    case "nrt":
            //        content_CourseDetails = System.IO.File.ReadAllText(Server.MapPath(@"~/Views/Home/Content/nrt.html"));
            //        ViewBag.Title = "NRT - StudentHub.in";
            //        break;
            //}

            //ViewBag.Content = content_CourseDetails;

            //Liason.Front obj_Front = new Liason.Front();
            //Liason.EntityLayer.CourseEN obj_Course = new Liason.EntityLayer.CourseEN();

            //DataSet ds_CourseData = obj_Front.GetCourseData(coursename, out obj_Course);

            //ViewData.Model = obj_Course;

            //if(obj_Course.Name == "" || obj_Course.Name == null)
            //{
            //    return View("Error404");
            //}

            return View();
        }


        public ActionResult VerifyEmail(string VerificationCode, string Email)
        {
            Liason.Students obj_Student = new Liason.Students();

            string Response = obj_Student.VerifyEmail(VerificationCode, Email);

            ViewBag.Response = Response;

            return View("StudentRegisterComplete");
        }


        // --------------------------------------------------- OTHER LINKS
        public ActionResult Pages(string PageName)
        {
            Liason.Pages obj_Page = new Liason.Pages();

            ViewData.Model = obj_Page.GetPageData(PageName);

            return View("Page");
        }


        //public ActionResult TermsConditions()
        //{
        //    ViewBag.Title = "Terms and Conditions - StudentHub.in";

        //    //var content_TermsConditions = System.IO.File.ReadAllText(Server.MapPath(@"~/Views/Home/Content/termsandconditions.html"));

        //    Liason.Pages obj_Page = new Liason.Pages();

        //    ViewData.Model = obj_Page.GetPageData("termsandconditions");

        //    //ViewBag.Content = content_TermsConditions;

        //    return View();
        //}


        //public ActionResult PrivacyPolicy()
        //{
        //    ViewBag.Title = "Privacy Policy - StudenthHub.in";

        //    var content_PrivacyPolicy = System.IO.File.ReadAllText(Server.MapPath(@"~/Views/Home/Content/privacypolicy.html"));

        //    ViewBag.Content = content_PrivacyPolicy;

        //    return View();
        //}


        //public ActionResult RefundPolicy()
        //{
        //    ViewBag.Title = "Refund Policy - StudentHub.in";

        //    var content_RefundPolicy = System.IO.File.ReadAllText(Server.MapPath(@"~/Views/Home/Content/refundpolicy.html"));

        //    ViewBag.Content = content_RefundPolicy;

        //    return View();
        //}


        //public ActionResult Disclaimer()
        //{
        //    ViewBag.Title = "Disclaimer - StudentHub.in";

        //    var content_Disclaimer = System.IO.File.ReadAllText(Server.MapPath(@"~/Views/Home/Content/disclaimer.html"));

        //    ViewBag.Content = content_Disclaimer;

        //    return View();
        //}


        // --------------------------------------------------- ERROR PAGES

        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;

            ViewBag.Title = "404 - StudentHub.in";

            var content_Error404 = System.IO.File.ReadAllText(Server.MapPath("~/Views/Home/Content/404.html"));

            ViewBag.Content = content_Error404;

            return View();
        }
    }
}