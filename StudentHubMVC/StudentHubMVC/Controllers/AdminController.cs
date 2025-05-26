using StudentHubMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Configuration;
using AutoMapper;
using System.IO;
using StudentHubMVC.Repositories;
using System.IO.Compression;
using System.Data.Entity.Validation;


namespace StudentHubMVC.Controllers
{
    public class AdminController : Controller
    {
        readonly Liason.Help obj_Help = new Liason.Help();
        readonly Entities db = new Entities();

        readonly RepResults _repResults = new RepResults();
        readonly RepResultsIcet _repResultsIcet = new RepResultsIcet();
        readonly RepResultsPolycet _repResultsPolytec = new RepResultsPolycet();


        // --------------------------------------------------------------------------- ADMIN LOGOUT
        public ActionResult Logout()
        {
            Session.RemoveAll();
            //Fetch the Cookie using its Key.
            HttpCookie loginCookie = Request.Cookies["ShubLogin"];

            //Set the Expiry date to past date.
            loginCookie.Expires = DateTime.Now.AddDays(-1);

            //Update the Cookie in Browser.
            Response.Cookies.Add(loginCookie);

            return Redirect("/admin/login");
            //throw new Exception("Unauthorized Access");

        }


        public ActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAdmin(FormCollection collection)
        {
            Liason.Admin obj_Admin = new Liason.Admin();

            string Response = obj_Admin.AdminLogin(collection);

            // LOGIN SUCCESSFUL
            if (Response == "SUCCESS")
            {
                DataTable dt_LoginDetails = obj_Admin.GetLoginDetails(collection["adminemail"]);

                if (dt_LoginDetails.Rows.Count > 0)
                {
                    Session["FullName"] = dt_LoginDetails.Rows[0]["FullName"];
                    Session["Email"] = dt_LoginDetails.Rows[0]["Email"];
                    Session["ID"] = dt_LoginDetails.Rows[0]["ID"];

                    HttpCookie loginCookie = new HttpCookie("ShubLogin");
                    loginCookie.Values["FullName"] = dt_LoginDetails.Rows[0]["FullName"].ToString();
                    loginCookie.Values["Email"] = dt_LoginDetails.Rows[0]["Email"].ToString();
                    loginCookie.Values["ID"] = dt_LoginDetails.Rows[0]["ID"].ToString();

                    loginCookie.Expires = DateTime.Now.AddDays(30);

                    this.ControllerContext.HttpContext.Response.Cookies.Add(loginCookie);
                }

                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                ViewBag.LoginMessage = Response;
            }

            return View();
        }


        public ActionResult Dashboard()
        {

            return View();
        }

        // --------------------------------------------------------------------- STUDENT

        public ActionResult StudentsManager()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Students obj_Student = new Liason.Students();

            DataTable dt_AllStudentsData = obj_Student.GetAllStudents();

            ViewData.Model = dt_AllStudentsData.AsEnumerable();

            return View();
        }


        public ActionResult AddStudent()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            ViewBag.Title = "Add Student";
            return View("SaveStudent");
        }

        // POST: Admin/SaveStudent
        [HttpPost]
        public ActionResult AddStudent(FormCollection collection)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            try
            {
                HttpPostedFileBase file = Request.Files["ProfilePicUpload"];

                // TODO: Add insert logic here
                Liason.Students obj_Student = new Liason.Students();

                string Response = obj_Student.ProcessData_Student("ADD", "", collection, file);
                int ID;
                if (int.TryParse(Response, out ID))
                {
                    ViewBag.Title = "Edit Student";
                    ViewBag.SaveMessage = "Added New Student";
                    return Redirect("/admin/savestudent/" + ID);
                }

                ViewBag.SaveMessage = Response;
                ViewBag.Title = "Add Student";

                return View("SaveStudent");
            }
            catch (Exception ex)
            {
                return View("SaveStudent");
            }
        }


        // GET: Admin/SaveStudent/5
        public ActionResult SaveStudent(int id)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Students obj_Student = new Liason.Students();

            ViewData.Model = obj_Student.EditStudent(id.ToString());

            ViewBag.Title = "Edit Student";

            return View();
        }


        // POST: Admin/SaveStudent/5
        [HttpPost]
        public ActionResult SaveStudent(int id, FormCollection collection)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            try
            {
                HttpPostedFileBase file = Request.Files["ProfilePicUpload"];

                // TODO: Add update logic here
                Liason.Students obj_Student = new Liason.Students();

                string Response = obj_Student.ProcessData_Student("UPDATE", id.ToString(), collection, file);

                ViewData.Model = obj_Student.EditStudent(id.ToString());
                ViewBag.SaveMessage = Response;

                ViewBag.Title = "Edit Student";

                return View("SaveStudent");
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        // --------------------------------------------------------------------- COURSE
        public ActionResult CoursesManager(VMCoursesManager search, int? page)
        {
            var context = new Entities();
            VMCoursesManager cm = new VMCoursesManager();
            IEnumerable<MainCourses> courses = context.MainCourses.ToList();

            if (search.SearchName != null && search.SearchName != "")
                courses = courses.Where(c => c.Name.ToLower().Contains(search.SearchName.ToLower()));

            if (search.Stream != null && search.Stream != "")
                courses = courses.Where(c => c.Stream.ToString() == search.Stream);

            if (search.Status != null && search.Status != "")
                courses = courses.Where(c => c.Status == search.Status);


            cm.Courses = courses.OrderBy(c => c.Name).ToList().ToPagedList(page ?? 1, int.Parse(ConfigurationManager.AppSettings["Paging50"]));

            //return View("CoursesManagerOld");

            return View(cm);
        }

        // GET: Admin/SaveCourse/4
        public ActionResult SaveCourse(int? id)
        {
            var context = new Entities();

            if(id != null && id.ToString() != "" && id != 0)
            {
                ViewData.Model = context.MainCourses.Single(course => course.MainCourseId == id);
            }

            ViewBag.Title = "Edit Course";

            return View();
        }


        // POST: Admin/SaveCOurse/4
        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult SaveCourse(MainCourses mainCourse, int? id)
        {
            ViewBag.Title = "Edit Course";

            MainCourses tmpCourse = new MainCourses();
            var context = new Entities();

            bool exists = false;

            if (id == null || id == 0)
            {
                tmpCourse = new Entities().MainCourses.Where(c => c.Name == mainCourse.Name).FirstOrDefault();

                if (tmpCourse != null && tmpCourse.Name != "" && tmpCourse.Name != null)
                    exists = true;
            }

            else
            {
                tmpCourse = new Entities().MainCourses.Where(c => c.Name == mainCourse.Name).FirstOrDefault();

                if (tmpCourse != null && tmpCourse.Name != "" && tmpCourse.Name != null && tmpCourse.MainCourseId != id)
                    exists = true;
            }

            if (!exists)
            {

                //if (id != null && id.ToString() != "" && id != 0)
                //{
                //    mainCourse.MainCourseId = (int)id;
                //}

                string response1 = "";

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase logo = Request.Files["uploadlogo"];

                    if (logo.ContentLength > 0)
                    {
                        response1 = new Liason.Help().uploadfile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CourseLogosPath"]), logo);

                        if (response1 != "ERROR")
                            mainCourse.Logo = response1;
                    }
                }

                if (response1 != "ERROR")
                {
                    context.MainCourses.Add(mainCourse);

                    if (mainCourse.MainCourseId > 0)
                    {
                        context.Entry(mainCourse).State = System.Data.Entity.EntityState.Modified;
                    }

                    context.SaveChanges();
                }

                else
                {
                    ViewBag.ErrorMessage = "Upload Failed - Error in Saving Institute";
                    return View(mainCourse);
                }

                ViewBag.SaveMessage = "Course Saved";

                return RedirectToAction("SaveCourse", new { id = mainCourse.MainCourseId, message = "Course Saved" });
            }

            ViewBag.ErrorMessage = "Course Name Already Exists";
            return View(mainCourse);
        }


        [HttpPost]
        public ActionResult DeleteMainCourse(int? courseId)
        {
            if(courseId > 0)
            {
                using (var context = db.Database.BeginTransaction())
                {
                    try
                    {
                        if(db.MainCourses.Any(c => c.MainCourseId == courseId))
                        {
                            if (db.InstitutionCourses.Any(c => c.CourseId == courseId))
                            {
                                foreach (InstitutionCourses inscrs in db.InstitutionCourses.Where(c => c.CourseId == courseId).ToList())
                                {
                                    his_shub_institutioncourses his_inscrs = new his_shub_institutioncourses();
                                    Mapper.Map(inscrs, his_inscrs);
                                    his_inscrs.inscrs_his_TransactionType = "DELETE";
                                    db.his_shub_institutioncourses.Add(his_inscrs);
                                    db.InstitutionCourses.Remove(inscrs);
                                }
                            }

                            if (db.InstitutionCourseTrainingModes.Any(c => c.CourseId == courseId))
                            {
                                foreach (InstitutionCourseTrainingModes inscrs in db.InstitutionCourseTrainingModes.Where(c => c.CourseId == courseId).ToList())
                                {
                                    his_shub_institutioncoursestrainingmodes his_inscrs = new his_shub_institutioncoursestrainingmodes();
                                    Mapper.Map(inscrs, his_inscrs);
                                    his_inscrs.trnmd_his_TransactionType = "DELETE";
                                    db.his_shub_institutioncoursestrainingmodes.Add(his_inscrs);
                                    db.InstitutionCourseTrainingModes.Remove(inscrs);
                                }
                            }

                            MainCourses maincourse = db.MainCourses.FirstOrDefault(c => c.MainCourseId == courseId);
                            his_shub_maincourses his_maincourse = new his_shub_maincourses();
                            his_maincourse.course_his_TransactionType = "DELETE";
                            Mapper.Map(maincourse, his_maincourse);
                            db.his_shub_maincourses.Add(his_maincourse);
                            db.MainCourses.Remove(maincourse);

                            db.SaveChanges();
                            context.Commit();
                        }
                    }
                    catch(Exception ex)
                    {
                        obj_Help.ExcpetionsHandling(ex);
                        context.Rollback();
                        return RedirectToAction("SaveCourse", new { id = courseId, errormessage = "Error occured while Deleting" });
                    }
                }

                return RedirectToAction("CoursesManager", new { errormessage = "Course Deleted!!" });
            }

            return RedirectToAction("CoursesManager");
        }

        // --------------------------------------------------------------------- COURSE SECTION CHAPTER CONTENT - TUTORIALS
        public ActionResult TutorialsManager()
        {
            Entities db = new Entities();
            List<VW_TutorialCoursesList> tutorials = db.VW_TutorialCoursesList1.ToList();

            return View(tutorials);
        }

        public ActionResult SaveCourseContent(int? course)
        {
            ViewBag.CourseID = 0;

            if (course != null && course > 0)
            {
                ViewBag.CourseName = new Liason.Courses().GetCourseName(course.ToString());

                Liason.Sections obj_Section = new Liason.Sections();

                DataSet ds_Result = obj_Section.GetAllSections((int)course);

                ViewBag.Sections = ds_Result.Tables[0];
                ViewBag.Chapters = ds_Result.Tables[1];

                ViewBag.CourseID = course;
            }
            
            return View();
        }


        public ActionResult AddSection(int course)
        {
            ViewBag.CourseName = new Liason.Courses().GetCourseName(course.ToString());

            return View("SaveSection");
        }

        [HttpPost]
        public ActionResult AddSection(int course, FormCollection collection)
        {
            Liason.Sections obj_Section = new Liason.Sections();

            string Response = obj_Section.ProcessData_Section("ADD", course.ToString(), "0", collection);

            ViewBag.SaveMessage = "Section Details Saved!!";

            return RedirectToAction("SaveSection", new { course = course, sectionid = Response, message = "Section Details Saved!!" });
        }


        // GET: Admin/savesection/4
        public ActionResult SaveSection(int course, int sectionid)
        {
            Liason.Sections obj_Section = new Liason.Sections();

            DataSet ds_Result = obj_Section.GetSectionDetails(sectionid);

            ViewBag.CourseName = ds_Result.Tables[0].Rows[0]["CourseName"].ToString();

            Liason.EntityLayer.SectionEN SectionData = new Liason.EntityLayer.SectionEN();

            if (ds_Result.Tables[1].Rows.Count > 0)
            {
                SectionData.Sno = ds_Result.Tables[1].Rows[0]["CourseId"].ToString();
                SectionData.Title = ds_Result.Tables[1].Rows[0]["Title"].ToString();
                SectionData.Description = ds_Result.Tables[1].Rows[0]["Description"].ToString();
                SectionData.SortId = ds_Result.Tables[1].Rows[0]["SortId"].ToString();
            }

            ViewData.Model = SectionData;

            ViewBag.Chapters = ds_Result.Tables[2];
            ViewBag.Tests = ds_Result.Tables[3];
            ViewBag.Materials = ds_Result.Tables[4];

            return View();
        }


        [HttpPost]
        public ActionResult SaveSection(int course, int sectionid, FormCollection collection)
        {
            Liason.Sections obj_Section = new Liason.Sections();

            string Response = obj_Section.ProcessData_Section("UPDATE", course.ToString(), sectionid.ToString(), collection);

            ViewBag.SaveMessage = "Section Details Saved!!";

            //return View();
            return RedirectToAction("SaveSection", new { course = course, sectionid = sectionid, message = "Section Details Saved!!" });
        }


        [HttpPost]
        public ActionResult AddTest(int course, int sectionid, FormCollection collection)
        {
            Liason.SectionTests obj_Test = new Liason.SectionTests();

            obj_Test.ProcessData_Test("ADD", sectionid, "0", collection);

            return RedirectToAction("SaveSection", new { course = course, sectionid = sectionid, message = "New Test Added" });
        }


        // GET: Admin/savetest/4
        [HttpPost]
        public ActionResult SaveTest(int course, int sectionid, int testid, FormCollection collection)
        {
            Liason.SectionTests obj_Test = new Liason.SectionTests();

            obj_Test.ProcessData_Test("UPDATE", sectionid, testid.ToString(), collection);

            return RedirectToAction("SaveSection", new { course = course, sectionid = sectionid, message = "Test Saved" });
        }


        // GET: Admin/deletetest/1/2/1
        public ActionResult DeleteTest(int course, int sectionid, int testid)
        {
            Liason.SectionTests obj_Test = new Liason.SectionTests();

            obj_Test.DeleteTest(sectionid, testid.ToString());

            return RedirectToAction("SaveSection", new { course = course, sectionid = sectionid, message = "Test Deleted" });
        }


        // GET: Admin/uploadmaterial/1/2
        [HttpPost]
        public ActionResult UploadMaterial(int course, int sectionid, FormCollection collection)
        {
            Liason.SectionMaterials obj_Material = new Liason.SectionMaterials();

            obj_Material.ProcessData_Material("ADD", sectionid, "", Request);

            return RedirectToAction("SaveSection", new { course = course, sectionid = sectionid, message = "Material Uploaded" });
        }


        // GET: Admin/delete/1/2
        public ActionResult DeleteMaterial(int course, int sectionid, string fileid)
        {
            Liason.SectionMaterials obj_Material = new Liason.SectionMaterials();

            obj_Material.ProcessData_Material("DELETE", sectionid, fileid, Request);

            return RedirectToAction("SaveSection", new { course = course, sectionid = sectionid, message = "Deleted Material" });
        }


        // GET: Admin/testquestions/4/2/1
        public ActionResult TestQuestions(int course, int sectionid, int testid)
        {
            DataSet ds_Result = new Liason.SectionTestQuestions().GetAllQuestions(testid.ToString());

            if(ds_Result.Tables[0].Rows.Count > 0)
            {
                ViewBag.Difficulty = ds_Result.Tables[0].Rows[0]["Difficulty"].ToString();
            }

            ViewBag.Questions = ds_Result.Tables[1];

            ViewBag.CourseName = new Liason.Courses().GetCourseName(course.ToString());

            return View();
        }



        // POST: Admin/testquestions/4/2/1
        [HttpPost]
        public ActionResult TestQuestions(int course, int sectionid, int testid, FormCollection collection)
        {
            string Response = new Liason.SectionTestQuestions().ProcessData_TestQuestion(course, sectionid, testid, collection);

            return RedirectToAction("TestQuestions", new { course = course, sectionid = sectionid, testid = testid, message = "Question Saved!!" });
        }


        // GET: Admin/deletequesion/4
        public ActionResult DeleteQuestion(int course, int sectionid, int testid, string questionid)
        {
            string Response = new Liason.SectionTestQuestions().DeleteQuestion(questionid);

            return RedirectToAction("TestQuestions", new { course = course, sectionid = sectionid, testid = testid, message = "Question Deleted!!" });
        }


        public ActionResult AddChapter(int course, int sectionid)
        {
            ViewBag.CourseName = new Liason.Courses().GetCourseName(course.ToString());

            return View("SaveChapter");
        }


        [HttpPost]
        public ActionResult AddChapter(int course, int sectionid, FormCollection collection)
        {
            Liason.Chapters obj_Chapter = new Liason.Chapters();

            string Response = obj_Chapter.ProcessData_Chapter("ADD", sectionid, "0", collection, Request);

            return RedirectToAction("SaveChapter", new { course = course, sectionid = sectionid, chapterid = Response, message = "Chapter Details Saved!!" });
        }


        // GET: Admin/savechapter/4
        public ActionResult SaveChapter(int course, int sectionid, int chapterid)
        {
            ViewBag.CourseName = new Liason.Courses().GetCourseName(course.ToString());

            Liason.Chapters obj_Chapter = new Liason.Chapters();

            ViewData.Model = obj_Chapter.EditChapter(chapterid.ToString());

            return View();
        }


        [HttpPost]
        public ActionResult SaveChapter(int course, int sectionid, string chapterid, FormCollection collection)
        {
            Liason.Chapters obj_Chapter = new Liason.Chapters();

            string Response = obj_Chapter.ProcessData_Chapter("UPDATE", sectionid, chapterid, collection, Request);

            return RedirectToAction("SaveChapter", new { course = course, sectionid = sectionid, chapterid = chapterid, message = "Chapter Details Saved!!" });
            //return View();
        }


        // GET: Admin/savechapter/4
        public ActionResult DeleteChapter(int course, int sectionid, int chapterid)
        {
            Liason.Chapters obj_Chapter = new Liason.Chapters();

            string Response = obj_Chapter.DeleteChapter(sectionid, chapterid.ToString());
            
            return RedirectToAction("SaveSection", new { course = course, sectionid = sectionid, message = "Chapter Deleted!!" });
        }


        // --------------------------------------------------------------------- INSTRUCTORS
        public ActionResult InstructorsManager()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Instructors obj_Instructor = new Liason.Instructors();

            DataTable dt_AllInstructorsData = obj_Instructor.GetAllInstructors();

            ViewData.Model = dt_AllInstructorsData.AsEnumerable();

            return View();
        }

        public ActionResult AddInstructor()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            ViewBag.Title = "Add Instructor";
            return View("SaveInstructor");
        }


        [HttpPost]
        public ActionResult AddInstructor(FormCollection collection)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            HttpPostedFileBase file = Request.Files["ProfilePicUpload"];

            Liason.Instructors obj_Instructor = new Liason.Instructors();

            string Response = obj_Instructor.ProcessData_Instructor("ADD", "", collection, file);

            int ID;
            if (int.TryParse(Response, out ID))
            {
                ViewBag.Title = "Edit Instructor";
                ViewBag.SaveMessage = "Added New Instructor";
                return Redirect("/admin/saveinstructor/" + ID);
            }

            ViewBag.SaveMessage = Response;

            ViewBag.Title = "Add Instructor";
            return View("SaveInstructor");
        }

        // GET: Admin/SaveInstructor/4
        public ActionResult SaveInstructor(int ID)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Instructors obj_Instructor = new Liason.Instructors();

            ViewData.Model = obj_Instructor.EditInstructor(ID.ToString());

            ViewBag.Title = "Edit Instructor";
            return View();
        }


        // POST: Admin/SaveInstructor/4
        [HttpPost]
        public ActionResult SaveInstructor(int ID, FormCollection collection)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            try
            {
                HttpPostedFileBase file = Request.Files["ProfilePicUpload"];

                // TODO: Add update logic here
                Liason.Instructors obj_Instructor = new Liason.Instructors();

                string Response = obj_Instructor.ProcessData_Instructor("UPDATE", ID.ToString(), collection, file);

                ViewData.Model = obj_Instructor.EditInstructor(ID.ToString());
                ViewBag.SaveMessage = Response;

                ViewBag.Title = "Edit Instructor";

                return View("SaveInstructor");
            }
            catch (Exception ex)
            {
                return View();
            }
        }



        // --------------------------------------------------------------------- BATCHES
        public ActionResult BatchesManager()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Batches obj_Batch = new Liason.Batches();

            DataTable dt_AllBatchesData = obj_Batch.GetAllBatches();

            ViewData.Model = dt_AllBatchesData.AsEnumerable();

            return View();
        }

        public ActionResult AddBatch()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Batches obj_Batch = new Liason.Batches();

            ViewBag.Courses = obj_Batch.GetAllCourses();
            ViewBag.Instructors = obj_Batch.GetAllInstructors();

            ViewBag.Title = "Add Batch";
            return View("SaveBatch");
        }


        [HttpPost]
        public ActionResult AddBatch(FormCollection collection)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Batches obj_Batch = new Liason.Batches();

            string Response = obj_Batch.ProcessData_Batch("ADD", "", collection);

            int ID;
            if (int.TryParse(Response, out ID))
            {
                ViewBag.Title = "Edit Batch";
                ViewBag.SaveMessage = "Added New Batch";
                ViewBag.Courses = obj_Batch.GetAllCourses();
                ViewBag.Instructors = obj_Batch.GetAllInstructors();
                return Redirect("/admin/savebatch/" + ID);
            }

            ViewBag.Courses = obj_Batch.GetAllCourses();
            ViewBag.Instructors = obj_Batch.GetAllInstructors();
            ViewBag.SaveMessage = Response;

            ViewBag.Title = "Add Batch";
            return View("SaveBatch");
        }

        // GET: Admin/Saveobj_Batch/4
        public ActionResult SaveBatch(int ID)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Batches obj_Batch = new Liason.Batches();

            ViewData.Model = obj_Batch.EditBatch(ID.ToString());

            ViewBag.Courses = obj_Batch.GetAllCourses();
            ViewBag.Instructors = obj_Batch.GetAllInstructors();

            ViewBag.Title = "Edit Batch";
            return View();
        }


        // POST: Admin/Saveobj_Batch/4
        [HttpPost]
        public ActionResult SaveBatch(int ID, FormCollection collection)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            try
            {
                // TODO: Add update logic here
                Liason.Batches obj_Batch = new Liason.Batches();

                string Response = obj_Batch.ProcessData_Batch("UPDATE", ID.ToString(), collection);

                ViewData.Model = obj_Batch.EditBatch(ID.ToString());

                ViewBag.Courses = obj_Batch.GetAllCourses();
                ViewBag.Instructors = obj_Batch.GetAllInstructors();
                ViewBag.SaveMessage = Response;

                ViewBag.Title = "Edit Batch";

                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        // --------------------------------------------------------------------- BATCH STUDENTS
        // GET: Admin/BatchStudents/4
        public ActionResult BatchStudents(int ID)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Batches obj_Batch = new Liason.Batches();

            DataTable dt_BatchStudents = obj_Batch.GetBatchStudents(ID.ToString());

            ViewData.Model = dt_BatchStudents.AsEnumerable();

            return View();
        }


        public ActionResult StudentsBatchActivate(string BatchId, string StudentId, string Mode)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Batches obj_Batch = new Liason.Batches();

            string Response = obj_Batch.StudentsBatchActivate(Mode, BatchId, StudentId);

            ViewBag.Response = Response;

            return Redirect("/admin/batchstudents/" + BatchId);
        }


        // --------------------------------------------------------------------- ENQUIRIES
        public ActionResult Enquiries()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Enquiries obj_Enquiries = new Liason.Enquiries();

            DataTable dt_AllEnquiries = obj_Enquiries.GetAllEnquiries();

            ViewData.Model = dt_AllEnquiries.AsEnumerable();

            return View();
        }


        // --------------------------------------------------------------------- VOUCHERS
        public ActionResult VouchersManager()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Vouchers obj_Vouchers = new Liason.Vouchers();

            DataTable dt_AllVouchers = obj_Vouchers.GetAllVouchers();

            ViewBag.Vouchers = dt_AllVouchers;

            return View();
        }


        [HttpPost]
        public ActionResult VouchersManager(FormCollection collection)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Vouchers obj_Vouchers = new Liason.Vouchers();
            
            string Response = "";

            Response = obj_Vouchers.ProcessData_Voucher("ADD", collection);
            ViewBag.SaveMessage = Response;

            DataTable dt_AllVouchers = obj_Vouchers.GetAllVouchers();
            ViewBag.Vouchers = dt_AllVouchers;

            return View();
        }


        // GET: vouchers/4
        public ActionResult SaveVoucher(string Id)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Vouchers obj_Vouchers = new Liason.Vouchers();

            DataTable dt_AllVouchers = obj_Vouchers.GetAllVouchers();

            ViewBag.ID = Id;
            ViewBag.Vouchers = dt_AllVouchers;
            ViewData.Model = obj_Vouchers.EditVoucher(Id);

            return View("VouchersManager");
        }


        // POST: vouchers/4
        [HttpPost]
        public ActionResult SaveVoucher(string Id, FormCollection collection)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Vouchers obj_Vouchers = new Liason.Vouchers();

            string Sno = collection["Sno"].ToString();
            string Response = "";

            if(Sno != "" && Sno != null)
            {
                Response = obj_Vouchers.ProcessData_Voucher("UPDATE", collection);
            }
            else
            {
                Response = obj_Vouchers.ProcessData_Voucher("ADD", collection);
            }            

            DataTable dt_AllVouchers = obj_Vouchers.GetAllVouchers();

            //ViewBag.ID = Id;
            ViewBag.SaveMessage = Response;
            ViewBag.Vouchers = dt_AllVouchers;
            //ViewData.Model = obj_Vouchers.EditVoucher(Id);

            return View("VouchersManager");
        }


        // --------------------------------------------------------------------- PLACEMENTS
        public ActionResult PlacementsManager()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Placements obj_Placements = new Liason.Placements();

            DataTable dt_AllPlacements = obj_Placements.GetAllPlacements();

            ViewBag.Placements = dt_AllPlacements;

            return View();
        }


        [HttpPost]
        public ActionResult PlacementsManager(FormCollection collection)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            try
            {
                HttpPostedFileBase file = Request.Files["PicUpload"];
                Liason.Placements obj_Placements = new Liason.Placements();

                string Response = "";

                Response = obj_Placements.ProcessData_Placement("ADD", "", collection, file);
                int no;

                if (int.TryParse(Response, out no))
                    Response = "Added New Placement";

                ViewBag.SaveMessage = Response;

                DataTable dt_AllPlacements = obj_Placements.GetAllPlacements();
                ViewBag.Placements = dt_AllPlacements;

                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        // GET: placementsmanager/4
        public ActionResult SavePlacement(string Id)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Placements obj_Placements = new Liason.Placements();

            DataTable dt_AllPlacements = obj_Placements.GetAllPlacements();

            ViewBag.ID = Id;
            ViewBag.Placements = dt_AllPlacements;
            ViewData.Model = obj_Placements.EditPlacement(Id);

            return View("PlacementsManager");
        }


        // POST: placementsmanager/4
        [HttpPost]
        public ActionResult SavePlacement(string Id, FormCollection collection)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            try
            {
                HttpPostedFileBase file = Request.Files["PicUpload"];
                Liason.Placements obj_Placements = new Liason.Placements();

                string Sno = collection["Sno"].ToString();
                string Response = "";

                if (Sno != "" && Sno != null)
                {
                    Response = obj_Placements.ProcessData_Placement("UPDATE", Sno, collection, file);
                }
                else
                {
                    Response = obj_Placements.ProcessData_Placement("ADD", "", collection, file);
                }

                DataTable dt_AllPlacements = obj_Placements.GetAllPlacements();

                //ViewBag.ID = Id;
                ViewBag.SaveMessage = Response;
                ViewBag.Placements = dt_AllPlacements;
                //ViewData.Model = obj_Vouchers.EditVoucher(Id);

                return RedirectToAction("PlacementsManager", new { message = "Placement Saved" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("PlacementsManager", new { errormessage = "Error Occured" });
            }
        }



        // --------------------------------------------------------------------- PAGES
        public ActionResult PagesManager()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            return View();
        }


        // GET: pagesmanager/4
        public ActionResult PageManagerGet(string Id)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Pages obj_Page = new Liason.Pages();

            Liason.EntityLayer.PageEN PageEN = obj_Page.GetPageData(Id);

            ViewBag.ID = Id;
            ViewData.Model = PageEN;

            return View("PagesManager");
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PageManagerGet(FormCollection collection, string Id)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            try
            {
                Liason.Pages obj_Page = new Liason.Pages();

                string Response = "";

                Response = obj_Page.ProcessData_Page( Id, collection);
                ViewBag.SaveMessage = Response;

                Liason.EntityLayer.PageEN PageEN = obj_Page.GetPageData(Id);

                ViewBag.ID = Id;
                ViewData.Model = PageEN;

                return View("PagesManager");
            }
            catch (Exception ex)
            {
                return View("PagesManager");
            }
        }


        // --------------------------------------------------------------------- SETTINGS
        // GET: admin/settings
        public ActionResult Settings()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Settings obj_Settings = new Liason.Settings();

            ViewData.Model = obj_Settings.GetSettingsData();

            return View();
        }


        [HttpPost]
        public ActionResult Settings(FormCollection collection)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                Session.RemoveAll();
                return RedirectToAction("LoginAdmin", "Admin");
            }

            Liason.Settings obj_Settings = new Liason.Settings();

            obj_Settings.SaveSettingsData(collection, Request);

            ViewData.Model = obj_Settings.GetSettingsData();

            return View();
        }


        // --------------------------------------------------------------------- DROPDOWN TYPES

        // GET: admin/grouptype/{id}
        public ActionResult DropdownTypes(int? id)
        {
            if(id != null && id > 0)
            {
                ViewData.Model = new Entities().DropdownTypes.Single(type => type.DropdownTypeId == id);
            }

            return View();
        }


        // POST: admin/grouptype/{id}
        [HttpPost]
        public ActionResult DropdownTypes(int? id, DropdownTypes dropdownType)
        {
            var context = new Entities();
            
            context.DropdownTypes.Add(dropdownType);

            if(dropdownType.DropdownTypeId > 0)
            {
                context.Entry(dropdownType).State = System.Data.Entity.EntityState.Modified;
                dropdownType.UpdatedDate = DateTime.Now;
            }
            else
            {
                if (context.DropdownTypes.Any(d => d.GroupName == dropdownType.GroupName && d.TypeValue == dropdownType.TypeValue))
                {
                    ViewBag.ErrorMessage = "Type Name Already Exists. Please Change";
                    return View(dropdownType);
                }
            }

            context.SaveChanges();

            return RedirectToAction("DropdownTypes", new { message = "Group Type Saved" });
        }


        // POST: admin/deletedropdowntype
        [HttpPost]
        public string DeleteDropdownType(string dropdownType, int id)
        {
            string rsp = "";

            try
            {
                switch (dropdownType)
                {
                    case "COURSESTREAM":
                        if (db.MainCourses.Any(c => c.Stream == id))
                        {
                            rsp = "Course Stream is already linked to some Institution Courses. Cannot Delete.";
                        }
                        break;
                    case "NEWS":
                        if (db.NewsAlerts.Any(c => c.NewsTypeId == id))
                        {
                            rsp = "News Type is already linked to some News Alerts. Cannot Delete.";
                        }
                        break;
                    case "JOBS":
                        if (db.JobAlerts.Any(c => c.JobType == id))
                        {
                            rsp = "Jobs Type is already linked to some Job Alerts. Cannot Delete.";
                        }
                        break;
                    case "TRAININGMODE":
                        if (db.InstitutionCourseTrainingModes.Any(c => c.TrainingModeId == id))
                        {
                            rsp = "Training Mode is already linked to some Institution Courses. Cannot Delete.";
                        }
                        break;
                    case "INSTITUTIONTYPE":
                        if (db.Institutions.Any(c => c.Type == id.ToString()))
                        {
                            rsp = "Institution Type is already linked to some institutions. Cannot Delete.";
                        }
                        break;
                    case "COLLEGETYPE":
                        if (db.Colleges.Any(c => c.coll_Type == id))
                        {
                            rsp = "College Type is already linked to some Colleges. Cannot Delete.";
                        }
                        break;
                    case "COLLEGECOURSETYPE":
                        if (db.CollegeCourses.Any(c => c.colcrs_CourseType == id))
                        {
                            rsp = "College Course Type is already linked to some College Courses. Cannot Delete.";
                        }
                        break;
                    case "COLLEGECOURSELEVEL":
                        if (db.CollegeCourses.Any(c => c.colcrs_CourseLevel == id))
                        {
                            rsp = "College Course Level is already linked to some College Courses. Cannot Delete.";
                        }
                        break;
                    case "DEGREETYPE":
                        if (db.CollegeCourses.Any(c => c.colcrs_DegreeType == id))
                        {
                            rsp = "College Degree Type is already linked to some College Courses. Cannot Delete.";
                        }
                        break;
                    case "FACILITIES":
                        if (db.CollegeFacilities.Any(c => c.collfac_FacilityId == id))
                        {
                            rsp = "Facility is already linked to some Colleges. Cannot Delete.";
                        }
                        break;
                    case "EXAMTYPE":
                        if (db.ExamCourses.Any(c => c.exmcrs_ExamType == id))
                        {
                            rsp = "Exam Type is already linked to some Exam Courses. Cannot Delete.";
                        }
                        break;
                    case "EXAMSTREAM":
                        if (db.ExamCourses.Any(c => c.exmcrs_ExamStream == id))
                        {
                            rsp = "Exam Stream is already linked to some Exam Courses. Cannot Delete.";
                        }
                        break;
                    case "EXAMLEVEL":
                        if (db.ExamCourses.Any(c => c.exmcrs_ExamLevel == id))
                        {
                            rsp = "Exam Level is already linked to some Exam Courses. Cannot Delete.";
                        }
                        break;
                    case "EXAMMODE":
                        if (db.ExamCourses.Any(c => c.exmcrs_ExamMode == id))
                        {
                            rsp = "Exam Mode is already linked to some Exam Courses. Cannot Delete.";
                        }
                        break;
                    case "MOCKTESTTYPE":
                        if (db.MockTests.Any(c => c.Type == id))
                        {
                            rsp = "Mock Test Type is already linked to some Mock Tests. Cannot Delete.";
                        }
                        break;
                }

                if (rsp == "")
                {
                    DropdownTypes drp = db.DropdownTypes.SingleOrDefault(d => d.DropdownTypeId == id);
                    his_shub_dropdowntypes his_Drp = new his_shub_dropdowntypes();

                    //var config = new MapperConfiguration(cfg => cfg.CreateMap<DropdownTypes, his_shub_dropdowntypes>());
                    Mapper.Map(drp, his_Drp);

                    his_Drp.type_his_TransactionType = "DELETE";

                    db.his_shub_dropdowntypes.Add(his_Drp);
                    db.DropdownTypes.Remove(drp);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                obj_Help.ExcpetionsHandling(ex);
                rsp = "ERROR";
            }            
            
            return rsp;
        }


        // --------------------------------------------------------------------- MOCK TEST
        public ActionResult MockTestsManager()
        {
            List<VM_MockTestsList> mockTests = new List<VM_MockTestsList>();

            mockTests = db.VM_MockTestsList.ToList();

            return View(mockTests);
        }


        // GET: admin/savemocktest
        public ActionResult SaveMockTest(string courseid, string id)
        {
            ViewData.Model = new MockTests();

            if (id != null && id != "")
            {
                int testId = int.Parse(id);
                ViewData.Model = new Entities().MockTests.Single(test => test.MockTestId == testId);
            }

            else if (courseid != null && courseid != "")
            {
                MockTests mockTest = new MockTests();
                mockTest.CourseId = int.Parse(courseid);
                ViewData.Model = mockTest;
            }            

            return View();
        }


        [HttpPost]
        public ActionResult SaveMockTest(MockTests MockTest, string courseid, string id)
        {
            try
            {

                var context = new Entities();

                //if (id != null && id != "")
                //{
                //    int testId = int.Parse(id);
                //    MockTest.MockTestId = testId;
                //}

                context.MockTests.Add(MockTest);

                if (MockTest.MockTestId > 0)
                {
                    context.Entry(MockTest).State = System.Data.Entity.EntityState.Modified;
                    MockTest.UpdatedDate = DateTime.Now;
                }

                context.SaveChanges();

                //MockTests mockTest = new MockTests();
                //mockTest.CourseId = int.Parse(courseid);

                //ViewData.Model = mockTest;

                return RedirectToAction("SaveMockTest", new { courseid = courseid, id = string.Empty, message = "Mock Test Saved" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error in Saving Mock Test - " + ex.Message;
                return View(MockTest);
            }
        }


        // --------------------------------------------------------------------- MOCK TEST

        // GET: admin/savemocktestquestion
        public ActionResult SaveMockTestQuestions(string courseid, string testid, string questionid)
        {
            int MockTestId = int.Parse(testid);
            ViewBag.MockTestId = MockTestId;
            ViewBag.CourseId = int.Parse(courseid);

            if (questionid != null && questionid != "")
            {
                int Id = int.Parse(questionid);
                ViewData.Model = new Entities().MockTestQuestions.Single(question => question.QuestionID == Id);
            }

            else
                ViewData.Model = new MockTestQuestions();

            return View();
        }


        [HttpPost]
        public ActionResult SaveMockTestQuestions(MockTestQuestions MockTestQuestion, string courseid, string testid, string questionid)
        //IEnumerable<HttpPostedFileBase> OptionAImages, IEnumerable<HttpPostedFileBase> OptionBImages, IEnumerable<HttpPostedFileBase> OptionCImages, IEnumerable<HttpPostedFileBase> OptionDImages, IEnumerable<HttpPostedFileBase> OptionEImages
        {
            using (Entities context = new Entities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    int MockTestId = int.Parse(testid);
                    ViewBag.MockTestId = MockTestId;

                    if (testid != null && testid != "")
                    {
                        MockTestQuestion.MockTestId = MockTestId;
                    }

                    //if (questionid != null && questionid != "")
                    //{
                    //    MockTestQuestion.QuestionID = int.Parse(questionid);
                    //}

                    Liason.Help obj_Help = new Liason.Help();

                    HttpPostedFileBase QuestionImages = Request.Files["QuestionImages"];
                    HttpPostedFileBase OptionAImages = Request.Files["OptionAImages"];
                    HttpPostedFileBase OptionBImages = Request.Files["OptionBImages"];
                    HttpPostedFileBase OptionCImages = Request.Files["OptionCImages"];
                    HttpPostedFileBase OptionDImages = Request.Files["OptionDImages"];
                    HttpPostedFileBase OptionEImages = Request.Files["OptionEImages"];
                    HttpPostedFileBase ExplanationImages = Request.Files["ExplanationImages"];
                    
                    bool saveImages = false;
                    string imagesSavePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("MockTestQuestionImages"));
                    
                    if(QuestionImages != null && QuestionImages.ContentLength > 0)
                    {
                        MockTestQuestion.QuestionImage = obj_Help.uploadfile(imagesSavePath, QuestionImages); //QuestionImages.FileName;                        
                    }

                    if (OptionAImages != null && OptionAImages.ContentLength > 0)
                    {
                        MockTestQuestion.OptionAImage = obj_Help.uploadfile(imagesSavePath, OptionAImages); //OptionAImages.FileName;                        
                    }

                    if (OptionBImages != null && OptionBImages.ContentLength > 0)
                    {
                        MockTestQuestion.OptionBImage = obj_Help.uploadfile(imagesSavePath, OptionBImages); //OptionBImages.FileName;                        
                    }

                    if (OptionCImages != null && OptionCImages.ContentLength > 0)
                    {
                        MockTestQuestion.OptionCImage = obj_Help.uploadfile(imagesSavePath, OptionCImages); //OptionCImages.FileName;                        
                    }

                    if (OptionDImages != null && OptionDImages.ContentLength > 0)
                    {
                        MockTestQuestion.OptionDImage = obj_Help.uploadfile(imagesSavePath, OptionDImages); //OptionDImages.FileName;                        
                    }

                    if (OptionEImages != null && OptionEImages.ContentLength > 0)
                    {
                        MockTestQuestion.OptionEImage = obj_Help.uploadfile(imagesSavePath, OptionEImages); //OptionEImages.FileName;                        
                    }

                    if (ExplanationImages != null && ExplanationImages.ContentLength > 0)
                    {
                        MockTestQuestion.ExplanationImage = obj_Help.uploadfile(imagesSavePath, ExplanationImages); //ExplanationImages.FileName;                        
                    }

                    context.MockTestQuestions.Add(MockTestQuestion);

                    if (MockTestQuestion.QuestionID > 0)
                    {
                        context.Entry(MockTestQuestion).State = System.Data.Entity.EntityState.Modified;
                    }

                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            return RedirectToAction("SaveMockTestQuestions", new { questionid = MockTestQuestion.QuestionID, message = "Question Saved" });
        }


        public ActionResult SaveMockTestQuestions2(string courseid, string testid)
        {
            int MockTestId = int.Parse(testid);
            ViewBag.MockTestId = MockTestId;
            ViewBag.CourseId = int.Parse(courseid);

            return View(new MockTestQuestions());
        }


        [HttpPost]
        public ActionResult SaveMockTestQuestions2(MockTestQuestions MockTestQuestion)
        {
            //int MockTestId = int.Parse(testid);
            //ViewBag.MockTestId = MockTestId;
            //ViewBag.CourseId = int.Parse(courseid);

            if (MockTestQuestion.MockTestId > 0)
            {
                using (Entities context = new Entities())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        long MockTestId = MockTestQuestion.MockTestId;
                        ViewBag.MockTestId = MockTestId;

                       // if (testid != null && testid != "")
                        //{
                        //    MockTestQuestion.MockTestId = MockTestId;
                        //}

                        //if (questionid != null && questionid != "")
                        //{
                        //    MockTestQuestion.QuestionID = int.Parse(questionid);
                        //}

                        Liason.Help obj_Help = new Liason.Help();

                        HttpPostedFileBase QuestionImages = Request.Files["QuestionImages"];
                        HttpPostedFileBase OptionAImages = Request.Files["OptionAImages"];
                        HttpPostedFileBase OptionBImages = Request.Files["OptionBImages"];
                        HttpPostedFileBase OptionCImages = Request.Files["OptionCImages"];
                        HttpPostedFileBase OptionDImages = Request.Files["OptionDImages"];
                        HttpPostedFileBase OptionEImages = Request.Files["OptionEImages"];
                        HttpPostedFileBase ExplanationImages = Request.Files["ExplanationImages"];

                        bool saveImages = false;
                        string imagesSavePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("MockTestQuestionImages"));

                        if (QuestionImages != null && QuestionImages.ContentLength > 0)
                        {
                            MockTestQuestion.QuestionImage = obj_Help.uploadfile(imagesSavePath, QuestionImages); //QuestionImages.FileName;                        
                        }

                        if (OptionAImages != null && OptionAImages.ContentLength > 0)
                        {
                            MockTestQuestion.OptionAImage = obj_Help.uploadfile(imagesSavePath, OptionAImages); //OptionAImages.FileName;                        
                        }

                        if (OptionBImages != null && OptionBImages.ContentLength > 0)
                        {
                            MockTestQuestion.OptionBImage = obj_Help.uploadfile(imagesSavePath, OptionBImages); //OptionBImages.FileName;                        
                        }

                        if (OptionCImages != null && OptionCImages.ContentLength > 0)
                        {
                            MockTestQuestion.OptionCImage = obj_Help.uploadfile(imagesSavePath, OptionCImages); //OptionCImages.FileName;                        
                        }

                        if (OptionDImages != null && OptionDImages.ContentLength > 0)
                        {
                            MockTestQuestion.OptionDImage = obj_Help.uploadfile(imagesSavePath, OptionDImages); //OptionDImages.FileName;                        
                        }

                        if (OptionEImages != null && OptionEImages.ContentLength > 0)
                        {
                            MockTestQuestion.OptionEImage = obj_Help.uploadfile(imagesSavePath, OptionEImages); //OptionEImages.FileName;                        
                        }

                        if (ExplanationImages != null && ExplanationImages.ContentLength > 0)
                        {
                            MockTestQuestion.ExplanationImage = obj_Help.uploadfile(imagesSavePath, ExplanationImages); //ExplanationImages.FileName;                        
                        }

                        context.MockTestQuestions.Add(MockTestQuestion);

                        if (MockTestQuestion.QuestionID > 0)
                        {
                            context.Entry(MockTestQuestion).State = System.Data.Entity.EntityState.Modified;
                        }

                        context.SaveChanges();
                        transaction.Commit();
                    }
                }
            }

            return Json("SUCCESS");
            //return _mockTestQuestionsList((int)MockTestQuestion.MockTestId);
            //return GetQuestionData(0);
            //return View(new MockTestQuestions());
        }


        public ActionResult _mockTestQuestionsList(int MockTestId)
        {
            var Questions = new Entities().MockTestQuestions.Where(cond => cond.MockTestId == MockTestId).AsEnumerable<MockTestQuestions>() ?? new List<MockTestQuestions>();

            return PartialView(Questions);
        }


        //[HttpPost]
        public JsonResult GetQuestionData(int MockTestQuestionId = 0)
        {
            MockTestQuestions question = new Entities().MockTestQuestions.FirstOrDefault(q => q.QuestionID == MockTestQuestionId) ?? new MockTestQuestions();

            return Json(
                new {
                    QuestionID = question.QuestionID,
                    MockTestId = question.MockTestId,
                    QuestionText = question.QuestionText,
                    QuestionImage = question.QuestionImage,
                    AnswerOption = question.AnswerOption,
                    OptionA = question.OptionA,
                    OptionAImage = question.OptionAImage,
                    OptionB = question.OptionB,
                    OptionBImage = question.OptionBImage,
                    OptionC = question.OptionC,
                    OptionCImage = question.OptionCImage,
                    OptionD = question.OptionD,
                    OptionDImage = question.OptionDImage,
                    OptionE = question.OptionE,
                    OptionEImage = question.OptionEImage,
                    Explanation = question.Explanation,
                    ExplanationImage = question.ExplanationImage,
                    AnswerOption2 = question.AnswerOption2,
                    AnswerOption3 = question.AnswerOption3
                }
                , JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public string DeleteMocktestQuestion(int testId, int questionId)
        {
            string res = "";

            try
            {
                if (db.MockTestQuestions.Any(q => q.MockTestId == testId && q.QuestionID == questionId))
                {
                    MockTestQuestions q = db.MockTestQuestions.SingleOrDefault(qu => qu.MockTestId == testId && qu.QuestionID == questionId);

                    his_shub_mocktestquestions mockTestQuestion = new his_shub_mocktestquestions();
                    Mapper.Map(q, mockTestQuestion);

                    mockTestQuestion.ques_his_TransactionType = "DELETE";
                    mockTestQuestion.QuestionID = q.QuestionID;

                    db.his_shub_mocktestquestions.Add(mockTestQuestion);
                    db.MockTestQuestions.Remove(q);
                    db.SaveChanges();
                }
                else
                {
                    res = "NOT FOUND";
                }
            }
            catch(Exception ex)
            {
                res = "ERROR";
                obj_Help.ExcpetionsHandling(ex);
            }

            return res;
        }


        [HttpPost]
        public string DeleteMocktest(int testId)
        {
            string res = "";

            try
            {
                if (db.MockTests.Any(q => q.MockTestId == testId))
                {
                    List<MockTestQuestions> q = db.MockTestQuestions.Where(qu => qu.MockTestId == testId).ToList();

                    using(var dbContext = db.Database.BeginTransaction())
                    {
                        try
                        {
                            if(q != null && q.Any())
                            {
                                foreach(MockTestQuestions tq in q)
                                {
                                    his_shub_mocktestquestions mockTestQuestion = new his_shub_mocktestquestions();
                                    Mapper.Map(tq, mockTestQuestion);

                                    mockTestQuestion.ques_his_TransactionType = "DELETE";
                                    mockTestQuestion.QuestionID = tq.QuestionID;

                                    db.his_shub_mocktestquestions.Add(mockTestQuestion);
                                    db.MockTestQuestions.Remove(tq);
                                }
                            }

                            MockTests tes = db.MockTests.SingleOrDefault(t => t.MockTestId == testId);

                            his_shub_mocktests mockTest = new his_shub_mocktests();
                            Mapper.Map(tes, mockTest);

                            mockTest.mocktest_his_TransactionType = "DELETE";
                            mockTest.MockTestId = tes.MockTestId;

                            db.his_shub_mocktests.Add(mockTest);
                            db.MockTests.Remove(tes);
                        }
                        catch(Exception ex)
                        {
                            dbContext.Rollback();
                            obj_Help.ExcpetionsHandling(ex);
                            res = "ERROR";
                        }

                        db.SaveChanges();
                        dbContext.Commit();
                    }
                }
                else
                {
                    res = "NOT FOUND";
                }
            }
            catch (Exception ex)
            {
                res = "ERROR";
                obj_Help.ExcpetionsHandling(ex);
            }

            return res;
        }



        // --------------------------------------------------------------------- NEWS ALERTS

        // GET: admin/newsalerts
        public ActionResult NewsAlertsManager()
        {
            ViewData.Model = new Entities().NewsAlerts.Include("DropdownTypes").OrderByDescending(news => news.NewsId).AsEnumerable();

            return View();
        }

        // GET: admin/savenewsalert/{newsid}
        public ActionResult SaveNewsAlert(int? newsid)
        {
            var context = new Entities();
            NewsAlerts news = new NewsAlerts();

            if (newsid != null && newsid > 0)
            {
                news = context.NewsAlerts.Single(n => n.NewsId == newsid);
            }

            return View(news);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveNewsAlert(NewsAlerts newsAlert, int? newsid)
        {
            var context = new Entities();

            context.NewsAlerts.Add(newsAlert);

            HttpPostedFileBase file = Request.Files["UploadCoverImage"];
            string response = "";

            if (file.ContentLength > 0)
            {
                response = new Liason.Help().uploadfile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["NewsPath"]), file);
                newsAlert.CoverImage = response;
            }

            if(response != "ERROR")
            {
                if (newsAlert.NewsId > 0)
                {
                    context.Entry(newsAlert).State = System.Data.Entity.EntityState.Modified;
                    newsAlert.UpdatedDate = DateTime.Now;
                }

                context.SaveChanges();
            }
            
            else
            {
                ViewBag.ErrorMessage = "Image upload Failed. News Alert Not Saved.";
                return View();
            }

            return RedirectToAction("SaveNewsAlert", new { newsid = newsAlert.NewsId, message = "News Alert Saved" });
        }

        [HttpPost]
        public ActionResult DeleteNewsAlert(int? newsId)
        {
            if(newsId != null && newsId > 0)
            {
                try
                {
                    if(db.NewsAlerts.Any(n => n.NewsId == newsId))
                    {
                        NewsAlerts news = db.NewsAlerts.Where(n => n.NewsId == newsId).FirstOrDefault();
                        db.NewsAlerts.Remove(news);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    obj_Help.ExcpetionsHandling(ex);
                    return RedirectToAction("SaveNewsAlert", new { newsid = newsId, errormessage = "Error occured while Deleting" });
                }
            }

            return RedirectToAction("NewsAlertsManager", new { errormessage = "News Alert Deleted" });
        }



        // --------------------------------------------------------------------- JOB ALERTS

        // GET: admin/jobalerts
        public ActionResult JobAlertsManager()
        {
            ViewData.Model = new Entities().JobAlerts.OrderByDescending(job => job.JobId).AsEnumerable();

            return View();
        }

        // GET: admin/savejobalert/{jobid}
        public ActionResult SaveJobAlert(int? jobid)
        {
            var context = new Entities();
            JobAlerts job = new JobAlerts();

            if (jobid != null && jobid > 0)
            {
                job = context.JobAlerts.Single(j => j.JobId == jobid);
            }

            return View(job);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveJobAlert(JobAlerts jobAlert, int? jobid)
        {
            var context = new Entities();

            string response1 = "";

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase logo = Request.Files["uploadlogo"];

                if (logo.ContentLength > 0)
                {
                    response1 = new Liason.Help().uploadfile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["joblogos"]), logo);

                    if (response1 != "ERROR")
                        jobAlert.CompanyLogo = response1;
                }
            }

            if (response1 != "ERROR")
            {
                context.JobAlerts.Add(jobAlert);

                if (jobAlert.JobId > 0)
                {
                    context.Entry(jobAlert).State = System.Data.Entity.EntityState.Modified;
                    jobAlert.UpdatedDate = DateTime.Now;
                }

                context.SaveChanges();
            }
            else
            {
                ViewBag.ErrorMessage = "Upload Error - Try again";
                return View(jobAlert);
            }

            ViewBag.SaveMessage = "Job Alert Saved";
            //return View(jobAlert);
            return RedirectToAction("JobAlertsManager");
        }

        [HttpPost]
        public ActionResult DeleteJobAlert(int? jobId)
        {
            if (jobId != null && jobId > 0)
            {
                try
                {
                    if (db.JobAlerts.Any(n => n.JobId == jobId))
                    {
                        JobAlerts job = db.JobAlerts.Where(n => n.JobId == jobId).FirstOrDefault();
                        db.JobAlerts.Remove(job);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    obj_Help.ExcpetionsHandling(ex);
                    return RedirectToAction("SaveJobAlert", new { jobid= jobId, errormessage = "Error occured while Deleting" });
                }
            }

            return RedirectToAction("JobAlertsManager", new { errormessage = "Job Alert Deleted" });
        }


        // ---------------------------------------------------------------------- QUESTION PAPERS
        // GET: admin/questionpapersmanager
        public ActionResult QuestionPapersManager(int? courseid)
        {           
            if(courseid != null && courseid > 0)
            {
                ViewData.Model = new Entities().QuestionPapers.Include("MainCourses").Where(paper => paper.CourseId == courseid);
            }

            return View();
        }


        // GET: admin/savequestionpaper/{questionpaperid}
        public ActionResult SaveQuestionPaper(int? questionpaperid)
        {            
            if(questionpaperid != null && questionpaperid != 0)
            {
                ViewData.Model = new Entities().QuestionPapers.Single(qpaper => qpaper.QuestionPaperId == (int)questionpaperid);
            }

            return View();
        }


        // POST: admin/savequestionpaper
        [HttpPost]
        public ActionResult SaveQuestionPaper(int? questionpaperid, QuestionPapers questionPaper)
        {
            string response = "";

            HttpPostedFileBase file = Request.Files["UploadFile"];

            if (file.ContentLength > 0)
            {
                response = new Liason.Help().uploadfile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["QuestionPapersPath"]), Request.Files[0]);
            }

            if (response == "" || response != "ERROR")
            {
                if (response != "")
                    questionPaper.Filename = response;

                var context = new Entities();

                QuestionPapers Qp = context.QuestionPapers.Add(questionPaper);

                if (questionPaper.QuestionPaperId > 0)
                    context.Entry(questionPaper).State = System.Data.Entity.EntityState.Modified;

                int effected = context.SaveChanges();
            }

            else
            {
                ViewBag.Message = "UPLOAD FAILED - SAVE FAILED!!";

                return RedirectToAction("SaveQuestionPaper", new { errormessage = "UPLOAD FAILED - SAVE FAILED" });
            }
                

            return RedirectToAction("SaveQuestionPaper", new { questionpaperid = questionPaper.QuestionPaperId, message = "Question Paper Saved" });
        }


        // ---------------------------------------------------------------------- INSTITUTIONS
        // GET: admin/institutionsmanager
        public ActionResult InstitutionsManager(VMInstitutionsManager vmInstitutionsManager, int? page)
        {
            VMInstitutionsManager IM = new VMInstitutionsManager();
            IEnumerable<Institutions> institutions = db.Institutions.ToList();

            if (vmInstitutionsManager.SearchName != null && vmInstitutionsManager.SearchName != "")
                institutions = institutions.Where(i => i.Name.ToLower().Contains(vmInstitutionsManager.SearchName.ToLower()));

            if (vmInstitutionsManager.Location != null && vmInstitutionsManager.Location != "")
                institutions = institutions.Where(i => i.Area.ToLower().Contains(vmInstitutionsManager.Location.ToLower()));

            if (vmInstitutionsManager.City != null && vmInstitutionsManager.City != "")
                institutions = institutions.Where(i => i.City.ToLower().Contains(vmInstitutionsManager.City.ToLower()));

            if (vmInstitutionsManager.Type != null && vmInstitutionsManager.Type != "")
                institutions = institutions.Where(i => i.Type.ToLower() == vmInstitutionsManager.Type.ToLower());

            if (vmInstitutionsManager.Status != null && vmInstitutionsManager.Status != "")
                institutions = institutions.Where(i => i.Status == vmInstitutionsManager.Status);

            IM.Institutions = institutions.OrderBy(i => i.Name).ToPagedList(page ?? 1, int.Parse(ConfigurationManager.AppSettings["Paging50"]));

            return View(IM);
        }


        // GET: admin/saveinstitution/{institutionid}
        public ActionResult SaveInstitution(int? institutionid)
        {
            if(institutionid != null && institutionid != 0)
            {
                ViewData.Model = new Entities().Institutions.Include("MainCourses").Single(institution => institution.InstitutionId == institutionid);
            }

            return View();
        }


        // POST: admin/saveinstitution/{institutionid}
        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult SaveInstitution(int? institutionid, Institutions institution)
        {
            //if (institutionid != null && institutionid != 0)
            //{
            //    ViewData.Model = new Entities().Institutions.Include("MainCourses").Single(institution => institution.InstitutionId == institutionid);
            //}

            Institutions tmpInstitute = new Institutions();

            bool exists = false;

            if (institutionid == 0 || institutionid == null)
            {
                tmpInstitute = new Entities().Institutions.Where(i => i.Name == institution.Name).FirstOrDefault();

                if (tmpInstitute != null && tmpInstitute.Name != "" && tmpInstitute.Name != null)
                    exists = true;
            }

            else
            {
                tmpInstitute = new Entities().Institutions.Where(c => c.Name == institution.Name).FirstOrDefault();

                if (tmpInstitute != null && tmpInstitute.Name != "" && tmpInstitute.Name != null && tmpInstitute.InstitutionId != institutionid)
                    exists = true;
            }

            if (!exists)
            {
                string response1 = "";
                string response2 = "";

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase logo = Request.Files["uploadlogo"];
                    HttpPostedFileBase broucher = Request.Files["uploadbroucher"];

                    if (logo.ContentLength > 0)
                    {
                        response1 = new Liason.Help().uploadfile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["InstitutionLogosPath"]), logo);
                        response1 = new Liason.Help().uploadfile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["Colleges"]), logo);

                        if (response1 != "ERROR")
                            institution.Logo = response1;
                    }

                    if (broucher.ContentLength > 0)
                    {
                        response2 = new Liason.Help().uploadfile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["BrouchersPath"]), broucher);

                        if (response2 != "ERROR")
                            institution.Broucher = response2;
                    }
                }

                if (response1 != "ERROR" && response2 != "ERROR")
                {
                    var context = new Entities();

                    context.Institutions.Add(institution);

                    if (institution.InstitutionId > 0)
                    {
                        context.Entry(institution).State = System.Data.Entity.EntityState.Modified;
                        institution.UpdatedDate = DateTime.Now;
                    }

                    context.SaveChanges();
                }

                else
                    return RedirectToAction("SaveInstitution", new { institutionid = institution.InstitutionId, errormessage = "Upload Failed - Error in Saving Institute" });


                return RedirectToAction("SaveInstitution", new { institutionid = institution.InstitutionId, message = "Institute Saved" });
            }

            ViewBag.ErrorMessage = "Institution Name Already Exists";
            if (institutionid != null && institutionid != 0)
            {
                institution.MainCourses = new Entities().Institutions.Single(i => i.InstitutionId == institutionid).MainCourses;
            }
            return View(institution);
        }


        [HttpPost]
        public ActionResult DeleteInstitution(int? institutionId)
        {
            if(institutionId > 0)
            {
                using (var context = db.Database.BeginTransaction())
                {
                    try
                    {
                        if(db.Institutions.Any(c => c.InstitutionId == institutionId))
                        {
                            List<InstitutionCourses> inscrsList = db.InstitutionCourses.Where(c => c.InstitutionId == institutionId).ToList();

                            if(inscrsList != null && inscrsList.Any())
                            {
                                foreach (InstitutionCourses inscrs in inscrsList)
                                {
                                    his_shub_institutioncourses his_inscrs = new his_shub_institutioncourses();
                                    Mapper.Map(inscrs, his_inscrs);
                                    his_inscrs.inscrs_his_TransactionType = "DELETE";
                                    db.his_shub_institutioncourses.Add(his_inscrs);

                                    List<InstitutionCourseTrainingModes> inscrstrnmodeList = db.InstitutionCourseTrainingModes.Where(c => c.CourseId == inscrs.InstitutionCourseId).ToList();

                                    if(inscrstrnmodeList != null && inscrstrnmodeList.Any())
                                    {
                                        foreach (InstitutionCourseTrainingModes inscrstrains in inscrstrnmodeList)
                                        {
                                            his_shub_institutioncoursestrainingmodes his_inscrstrains = new his_shub_institutioncoursestrainingmodes();
                                            Mapper.Map(inscrstrains, his_inscrstrains);
                                            his_inscrstrains.trnmd_his_TransactionType = "DELETE";
                                            db.his_shub_institutioncoursestrainingmodes.Add(his_inscrstrains);
                                            db.InstitutionCourseTrainingModes.Remove(inscrstrains);
                                        }
                                    }
                                    db.InstitutionCourses.Remove(inscrs);
                                }
                            }

                            foreach(Addresses address in db.Addresses.Where(a => a.InstitutionId == institutionId))
                            {
                                his_shub_addresses his_addresses = new his_shub_addresses();
                                Mapper.Map(address, his_addresses);
                                his_addresses.addrs_his_TransactionType = "DELETE";
                                db.his_shub_addresses.Add(his_addresses);
                                db.Addresses.Remove(address);
                            }

                            Institutions ins = db.Institutions.FirstOrDefault(c => c.InstitutionId == institutionId);
                            his_shub_institutions his_ins = new his_shub_institutions();
                            Mapper.Map(ins, his_ins);
                            his_ins.ins_his_TransactionType = "DELETE";
                            db.his_shub_institutions.Add(his_ins);
                            db.Institutions.Remove(ins);

                            db.SaveChanges();
                            context.Commit();
                        }
                    }
                    catch(Exception ex)
                    {
                        obj_Help.ExcpetionsHandling(ex);
                        context.Rollback();
                        return RedirectToAction("SaveInstitution", new { institutionid = institutionId, errormessage = "Error occured while Deleting" });
                    }
                }
            }

            return RedirectToAction("InstitutionsManager");
        }


        [HttpPost]
        [Route("GetInstituteAddress")]
        public ActionResult GetInstituteAddress(int? id)
        {
            Addresses addressInstitution = new Addresses();

            if (id > 0)
            {
                addressInstitution = new Entities().Addresses.Where(a => a.AddressId == id).FirstOrDefault();
            }

            return PartialView("_instituteAddress", addressInstitution);
        }


        // POST: admin -> institution -> address save
        [HttpPost]
        public ActionResult SaveInstitutionAddress(Addresses addressInstitution)
        {
            Entities db = new Entities();

            db.Addresses.Add(addressInstitution);

            if(addressInstitution.AddressId > 0 && addressInstitution.InstitutionId > 0)
            {
                db.Entry(addressInstitution).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();

            return RedirectToAction("SaveInstitution", new { institutionid = addressInstitution.InstitutionId, message = "Branch Saved" });
        }


        // ---------------------------------------------------------------------- INSTITUTION COURSES
        // GET: admin/saveinstitutioncourse/{instituteid}/{courseid}
        public ActionResult SaveInstitutionCourse(int? institutionid, int? InstitutionCourseId)
        {
            VMInstitutionCourse _vmInstitutionCourse = new VMInstitutionCourse();

            InstitutionCourses institutionCourse = new InstitutionCourses();
            //ViewData.Model = _vmInstitutionCourse;

            if (InstitutionCourseId != null && InstitutionCourseId != 0)
            {
                institutionCourse = new Entities().InstitutionCourses.SingleOrDefault(course => course.InstitutionCourseId == InstitutionCourseId);
                _vmInstitutionCourse.InstitutionId = institutionCourse.InstitutionId;
                _vmInstitutionCourse.CourseId = institutionCourse.CourseId;
                _vmInstitutionCourse.CourseFee = institutionCourse.CourseFee;
                _vmInstitutionCourse.Duration = institutionCourse.Duration;
                _vmInstitutionCourse.SavedTrainingModes = institutionCourse.InstitutionCourseTrainingModes.ToList();
            }

            else if (institutionid != null && institutionid != 0)
            {                
                institutionCourse.InstitutionId = (int)institutionid;
                _vmInstitutionCourse.InstitutionId = institutionCourse.InstitutionId;
                //ViewData.Model = institutionCourse;
            }
            
            return View(_vmInstitutionCourse);
        }


        // POST: admin/saveinstitutioncourse/{instituteid}/{courseid}
        [HttpPost]
        public ActionResult SaveInstitutionCourse(int? institutionid, int? courseid, VMInstitutionCourse _vmInstitutionCourse)
        {
            var context = new Entities();

            using (var transaction = context.Database.BeginTransaction())
            {
                InstitutionCourses institutionCourse = new InstitutionCourses();
                institutionCourse.InstitutionCourseId = _vmInstitutionCourse.InstitutionCourseId;
                institutionCourse.CourseId = _vmInstitutionCourse.CourseId;
                institutionCourse.InstitutionId = _vmInstitutionCourse.InstitutionId;
                institutionCourse.Duration = _vmInstitutionCourse.Duration;
                institutionCourse.CourseFee = _vmInstitutionCourse.CourseFee;

                InstitutionCourses tmpInsCourse = new Entities().InstitutionCourses.Where(c => c.InstitutionId == institutionid && c.CourseId == institutionCourse.CourseId).FirstOrDefault();

                bool exists = false;

                if (institutionCourse.InstitutionCourseId == 0 || institutionCourse.InstitutionCourseId == null)
                {
                    if (tmpInsCourse != null && tmpInsCourse.CourseId > 0)
                        exists = true;
                }
                else
                {
                    if (tmpInsCourse != null && tmpInsCourse.CourseId > 0 && tmpInsCourse.InstitutionCourseId != institutionCourse.InstitutionCourseId)
                        exists = true;
                }

                if (!exists)
                {

                    context.InstitutionCourses.Add(institutionCourse);

                    if (institutionCourse.InstitutionCourseId > 0)
                    {
                        context.Entry(institutionCourse).State = System.Data.Entity.EntityState.Modified;
                        institutionCourse.UpdatedDate = DateTime.Now;
                    }

                    context.SaveChanges();
                    var SavedTrainingModes = new Entities().InstitutionCourseTrainingModes.Where(t => t.CourseId == _vmInstitutionCourse.InstitutionCourseId).ToList();
                    _vmInstitutionCourse.InstitutionCourseId = institutionCourse.InstitutionCourseId;

                    if (_vmInstitutionCourse.TrainingModes != null)
                    {
                        // adding new training modes
                        foreach (string TM in _vmInstitutionCourse.TrainingModes)
                        {
                            var mode = SavedTrainingModes.Where(s => s.TrainingModeId.ToString() == TM).SingleOrDefault();

                            if (mode == null)
                            {
                                InstitutionCourseTrainingModes m = new InstitutionCourseTrainingModes();
                                m.TrainingModeId = int.Parse(TM);
                                m.CourseId = _vmInstitutionCourse.InstitutionCourseId;

                                context.InstitutionCourseTrainingModes.Add(m);
                            }
                        }
                    }

                    // deleting removed training modes
                    foreach (InstitutionCourseTrainingModes t in SavedTrainingModes)
                    {
                        if (_vmInstitutionCourse.TrainingModes == null || _vmInstitutionCourse.TrainingModes.Contains(t.TrainingModeId.ToString()) == false)
                        {
                            context.Entry(context.InstitutionCourseTrainingModes.Where(s => s.CourseId == t.CourseId && s.TrainingModeId == t.TrainingModeId).SingleOrDefault()).State = System.Data.Entity.EntityState.Deleted;
                        }
                    }

                    context.SaveChanges();
                    transaction.Commit();

                    return RedirectToAction("SaveInstitutionCourse", new { institutionid = institutionCourse.InstitutionId, InstitutionCourseId = institutionCourse.InstitutionCourseId, message = "Course Saved for Institution" });
                }

                ViewBag.ErrorMessage = "Course Already Exists for the Institute";
                return View(_vmInstitutionCourse);

                // return RedirectToAction("SaveInstitution");

                // return RedirectToAction("SaveInstitutionCourse");
            }


        }

        
        // GET: admin/delinstitutioncourse/{instituteid}/{courseid}
        public ActionResult DeleteInstitutionCourse(int? institutionid, int? InstitutionCourseId)
        {
            Entities db = new Entities();

            InstitutionCourses insCourse = new InstitutionCourses();

            if (institutionid != null && institutionid > 0 && InstitutionCourseId != null && InstitutionCourseId > 0)
                insCourse = db.InstitutionCourses.Where(i => i.InstitutionId == institutionid && i.InstitutionCourseId == InstitutionCourseId).SingleOrDefault();

            return View(insCourse);
        }


        // GET: admin/delinstitutioncourse/{instituteid}/{courseid}
        [HttpPost]
        public ActionResult DeleteInstitutionCourse(InstitutionCourses postInsCourse)
        {
            Entities db = new Entities();

            InstitutionCourses insCourse = new InstitutionCourses();

            if (postInsCourse.InstitutionId != null && postInsCourse.InstitutionId > 0 && postInsCourse.InstitutionCourseId != null && postInsCourse.InstitutionCourseId > 0)
            {
                insCourse = db.InstitutionCourses.Where(i => i.InstitutionId == postInsCourse.InstitutionId && i.InstitutionCourseId == postInsCourse.InstitutionCourseId).SingleOrDefault();

                string msg = insCourse.MainCourses.Name;

                db.Entry(insCourse).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();

                return RedirectToAction("SaveInstitution", new { institutionid = insCourse.InstitutionId, message = "Course - " + msg + " - Deleted for Institution" });
            }

            return View(insCourse);
        }


        // ---------------------------------------------------------------------- COLLEGE COURSES

        // GET: admin/CollegeCoursesManager/
        public ActionResult CollegeCoursesManager(VMCollegeCoursesManager vmCollegeCoursesManager, int? page)
        {
            VMCollegeCoursesManager IM = new VMCollegeCoursesManager();
            IEnumerable<CollegeCourses> collegeCourses = db.CollegeCourses.ToList();

            if (vmCollegeCoursesManager.SearchName != null && vmCollegeCoursesManager.SearchName != "")
                collegeCourses = collegeCourses.Where(i => i.colcrs_Name.ToLower().Contains(vmCollegeCoursesManager.SearchName.ToLower()));

            //if (vmCollegeCoursesManager.Location != null && vmCollegeCoursesManager.Location != "")
            //    collegeCourses = collegeCourses.Where(i => i.Area.ToLower().Contains(vmCollegeCoursesManager.Location.ToLower()));

            //if (vmCollegeCoursesManager.City != null && vmCollegeCoursesManager.City != "")
            //    collegeCourses = collegeCourses.Where(i => i.City.ToLower().Contains(vmCollegeCoursesManager.City.ToLower()));

            //if (vmCollegeCoursesManager.Type != null && vmCollegeCoursesManager.Type != "")
            //    collegeCourses = collegeCourses.Where(i => i.Type.ToLower() == vmCollegeCoursesManager.Type.ToLower());

            //if (vmCollegeCoursesManager.Status != null && vmCollegeCoursesManager.Status != "")
            //    collegeCourses = collegeCourses.Where(i => i.Status == vmCollegeCoursesManager.Status);

            IM.CollegeCourses = collegeCourses.OrderBy(i => i.colcrs_Name).ToPagedList(page ?? 1, int.Parse(ConfigurationManager.AppSettings["Paging50"]));

            return View(IM);
        }


        // GET: admin/SaveCollegeCourse/{collcourseid}
        public ActionResult SaveCollegeCourse(int? collegeCourseId)
        {
            CollegeCourses collegeCourse = new CollegeCourses();

            if (collegeCourseId != null && collegeCourseId > 0)
            {
                collegeCourse = db.CollegeCourses.Where(c => c.colcrs_Id == collegeCourseId).SingleOrDefault();
            }

            return View(collegeCourse);
        }


        // POST: admin/SaveCollegeCourse/{collegeCourseId}
        [HttpPost]
        public ActionResult SaveCollegeCourse(int? collegeCourseId, CollegeCourses collegeCourse)
        {
            CollegeCourses tmpCollegeCourse = new CollegeCourses();

            bool exists = false;

            if (collegeCourse.colcrs_Id > 0)
                collegeCourseId = (int)collegeCourse.colcrs_Id;

            if (collegeCourseId == 0 || collegeCourseId == null)
            {
                tmpCollegeCourse = db.CollegeCourses.Where(i => i.colcrs_Name == collegeCourse.colcrs_Name).FirstOrDefault();

                if (tmpCollegeCourse != null && tmpCollegeCourse.colcrs_Name != "" && tmpCollegeCourse.colcrs_Name != null)
                    exists = true;
            }

            else
            {
                tmpCollegeCourse = db.CollegeCourses.Where(c => c.colcrs_Name == collegeCourse.colcrs_Name).FirstOrDefault();

                if (tmpCollegeCourse != null && tmpCollegeCourse.colcrs_Name != "" && tmpCollegeCourse.colcrs_Name != null && tmpCollegeCourse.colcrs_Id != collegeCourseId)
                    exists = true;
            }

            if (!exists)
            {
                string response1 = "";
                string response2 = "";

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase logo = Request.Files["uploadlogo"];
                    HttpPostedFileBase coverpic = Request.Files["uploadcoverpic"];

                    if (logo.ContentLength > 0)
                    {
                        response1 = new Liason.Help().uploadfile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollegeCourse"]), logo);

                        if (response1 != "ERROR")
                            collegeCourse.colcrs_Logo = response1;
                    }

                    if (coverpic.ContentLength > 0)
                    {
                        response2 = new Liason.Help().uploadfile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollegeCourse"]), coverpic);

                        if (response2 != "ERROR")
                            collegeCourse.colcrs_CoverPic = response2;
                    }
                }

                if (response1 != "ERROR" && response2 != "ERROR")
                {
                    var context = new Entities();

                    context.CollegeCourses.Add(collegeCourse);

                    if (collegeCourse.colcrs_Id > 0)
                    {
                        context.Entry(collegeCourse).State = System.Data.Entity.EntityState.Modified;
                        collegeCourse.colcrs_ModifiedDate = DateTime.Now;
                    }

                    context.SaveChanges();
                }

                else
                    return RedirectToAction("SaveCollegeCourse", new { collegeCourseId = collegeCourse.colcrs_Id, errormessage = "Upload Failed - Error in Saving Institute" });


                return RedirectToAction("SaveCollegeCourse", new { collegeCourseId = collegeCourse.colcrs_Id, message = "College Course Saved" });
            }

            ViewBag.ErrorMessage = "Course Name Already Exists";
            //if (institutionid != null && institutionid != 0)
            //{
            //    institution.MainCourses = new Entities().Institutions.Single(i => i.InstitutionId == institutionid).MainCourses;
            //}
            return View(collegeCourse);
        }


        [HttpPost]
        public ActionResult DeleteCollegeCourse(int? collegeCourseId)
        {
            if (db.CollegeCourses.Any(c => c.colcrs_Id == collegeCourseId))
            {
                using (var context = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (db.CollegeCoursesRelations.Any(c => c.collcrsrel_CollegeCourseId == collegeCourseId))
                        {
                            foreach (CollegeCoursesRelation collCr in db.CollegeCoursesRelations.Where(c => c.collcrsrel_CollegeCourseId == collegeCourseId).ToList())
                            {
                                his_shub_collegecoursesrelation ccr = new his_shub_collegecoursesrelation();
                                Mapper.Map(collCr, ccr);
                                ccr.coll_his_TransactionType = "DELETE";

                                db.his_shub_collegecoursesrelation.Add(ccr);
                                db.CollegeCoursesRelations.Remove(collCr);
                            }
                        }

                        CollegeCourses collegeCourse = db.CollegeCourses.FirstOrDefault(c => c.colcrs_Id == collegeCourseId);
                        his_shub_collegecourse hisCollCrs = new his_shub_collegecourse();
                        Mapper.Map(collegeCourse, hisCollCrs);
                        hisCollCrs.coll_his_TransactionType = "DELETE";
                        db.his_shub_collegecourse.Add(hisCollCrs);
                        db.CollegeCourses.Remove(collegeCourse);

                        db.SaveChanges();
                        context.Commit();
                    }
                    catch (Exception ex)
                    {
                        obj_Help.ExcpetionsHandling(ex);
                        context.Rollback();
                        return RedirectToAction("SaveCollegeCourse", new { collegeCourseId = collegeCourseId, errormessage = "Error occured while Deleting" });
                    }
                }
            }

            return RedirectToAction("CollegeCoursesManager", new { errormessage = "College Course Deleted" });
        }


        // ----------------------------------------------------------------------- COLLEGES

        // GET: admin/CollegesManager/
        public ActionResult CollegesManager(VMCollegesManager vmCollegesManager, int? page)
        {
            VMCollegesManager IM = new VMCollegesManager();
            IEnumerable<Colleges> colleges = new Entities().Colleges.ToList();

            if (vmCollegesManager.SearchName != null && vmCollegesManager.SearchName != "")
                colleges = colleges.Where(i => i.coll_Name.ToLower().Contains(vmCollegesManager.SearchName.ToLower()));

            //if (vmCollegeCoursesManager.Location != null && vmCollegeCoursesManager.Location != "")
            //    collegeCourses = collegeCourses.Where(i => i.Area.ToLower().Contains(vmCollegeCoursesManager.Location.ToLower()));

            if (vmCollegesManager.City != null && vmCollegesManager.City != "")
                colleges = colleges.Where(i => i.coll_City.ToLower().Contains(vmCollegesManager.City.ToLower()));

            //if (vmCollegeCoursesManager.Type != null && vmCollegeCoursesManager.Type != "")
            //    collegeCourses = collegeCourses.Where(i => i.Type.ToLower() == vmCollegeCoursesManager.Type.ToLower());

            //if (vmCollegeCoursesManager.Status != null && vmCollegeCoursesManager.Status != "")
            //    collegeCourses = collegeCourses.Where(i => i.Status == vmCollegeCoursesManager.Status);

            IM.Colleges = colleges.OrderBy(i => i.coll_Name).ToPagedList(page ?? 1, int.Parse(ConfigurationManager.AppSettings["Paging50"]));

            return View(IM);
        }


        // GET: admin/SaveCollegeCourse/{collcourseid}
        public ActionResult SaveCollege(int? collegeId)
        {
            Colleges college = new Colleges();

            if (collegeId != null && collegeId > 0)
            {
                college = db.Colleges.Where(c => c.coll_ID == collegeId).SingleOrDefault();
                //college.SelectedCourses = db.CollegeCoursesRelations.Where(c => c.collcrsrel_CollegeId == collegeId).Any() ?
                //    db.CollegeCoursesRelations.Where(c => c.collcrsrel_CollegeId == collegeId).Select(c => (int)c.collcrsrel_CollegeCourseId).ToArray() : new int[] { };

                //college.CoursesIds = db.CollegeCoursesRelations.Where(c => c.collcrsrel_CollegeId == collegeId).Any() ?
                //    string.Join(",", db.CollegeCoursesRelations.Where(c => c.collcrsrel_CollegeId == collegeId).Select(c => (int)c.collcrsrel_CollegeCourseId).ToArray()) : "";

                //college.SelectedFacilites = db.CollegeFacilities.Where(c => c.collfac_CollegeId == collegeId).Any() ?
                //    db.CollegeFacilities.Where(c => c.collfac_CollegeId == collegeId).Select(c => (int)c.collfac_FacilityId).ToArray() : new int[] { };

                //college.FacilitiesIds = db.CollegeFacilities.Where(c => c.collfac_CollegeId == collegeId).Any() ?
                //    string.Join(",", db.CollegeFacilities.Where(c => c.collfac_CollegeId == collegeId).Select(c => (int)c.collfac_FacilityId).ToArray()) : "";
            }

            college.coll_FeesStructure = 
                string.IsNullOrEmpty(college.coll_FeesStructure) 
                ? "<table style=\"width: 100%;\"> <tbody> <tr> <td><strong>Particulars</strong></td> <td><strong>Amount</strong></td> </tr> <tr> <td>Caution Money (One Time, Refundable)</td> <td><br></td> </tr> <tr> <td>One Time Fees</td> <td><br></td> </tr> <tr> <td>Tuition Fee (per Semester)</td> <td><br></td> </tr> <tr> <td>Other fees (per Semester)</td> <td><br></td> </tr> <tr> <td><b>Total</b></td> <td><br></td> </tr> <tr> <td colspan=\"2\" > Note:&nbsp;</td> </tr> </tbody></table>"
                : college.coll_FeesStructure;

            college.coll_HostelFee =
                string.IsNullOrEmpty(college.coll_HostelFee) 
                ? "<table style=\"width: 100%; \"> <tbody> <tr> <td><strong>Particulars</strong></td> <td><strong>Amount</strong></td> </tr> <tr> <td>Mess Caution Money (One Time, Refundable)</td> <td><br></td> </tr> <tr> <td>Hostel Seat Rent (per Semester)</td> <td><br></td> </tr> <tr> <td>Electricity & Water charges (per Semester)</td> <td><br></td> </tr> <tr> <td>Other fees (per Semester)</td> <td><br></td> </tr> <tr> <td>Mess Advance (per Semester)</td> <td></td> </tr> <tr> <td><b>Total</b></td> <td><br></td> </tr> </tbody></table>"
                : college.coll_HostelFee;

            college.coll_Scholarships =
                string.IsNullOrEmpty(college.coll_Scholarships) 
                ? "<table style=\"width: 100%;\"> <tbody> <tr> <td><strong>Peformance</strong></td> <td><strong>Scholarships</strong></td> </tr> <tr> <td>Topper of each State and Central Board</td> <td>100% Tuition fee waiver for all 4 years</td> </tr><tr> <td></td> <td><br></td> </tr></tbody></table>"
                : college.coll_Scholarships;

            college.coll_Rankings =
                string.IsNullOrEmpty(college.coll_Rankings)
                ? "<table style=\"width: 100%; text-align: center; \"> <tbody> <tr> <td style=\"text-align: center; \" rowspan=\"2\"><strong>Category</strong></td> <td colspan=\"2\" style=\"text-align: center; \"><strong>Rank (Year)</strong></td> </tr> <tr> <td style=\"text-align: center; \"><strong>Latest</strong></td> <td style=\"text-align: center; \"><strong>Previous</strong></td> </tr> <tr> <td colspan=\"3\" style=\"text-align: center; \"><strong>International Ranking</strong></td> </tr> <tr> <td style=\"text-align: center; \"></td> <td style=\"text-align: center; \"></td> <td style=\"text-align: center; \"></td> </tr> <tr> <td><br></td> <td><br></td> <td><br></td> </tr> <tr> <td colspan=\"3\" style=\"text-align: center; \"><strong>National Ranking</strong></td> </tr> <tr> <td style=\"text-align: center; \"></td> <td style=\"text-align: center; \"></td> <td style=\"text-align: center; \"></td> </tr> <tr> <td><br></td> <td></td> <td></td> </tr> </tbody></table>"
                : college.coll_Rankings;

            return View(college);
        }


        // POST: admin/SaveCollegeCourse/{collcourseid}
        [HttpPost]
        public ActionResult SaveCollege(int? collegeId, Colleges college)
        {
            Colleges tmpCollege = new Colleges();

            bool exists = false;

            if (college.coll_ID > 0)
                collegeId = (int)college.coll_ID;

            if (collegeId == 0 || collegeId == null)
            {
                tmpCollege = db.Colleges.Where(i => i.coll_Name == college.coll_Name).FirstOrDefault();

                if (tmpCollege != null && tmpCollege.coll_Name != "" && tmpCollege.coll_Name != null)
                    exists = true;
            }

            else
            {
                tmpCollege = db.Colleges.Where(c => c.coll_Name == college.coll_Name).FirstOrDefault();

                if (tmpCollege != null && tmpCollege.coll_Name != "" && tmpCollege.coll_Name != null && tmpCollege.coll_ID != collegeId)
                    exists = true;
            }

            if (!exists)
            {
                string response1 = "";
                string response2 = "";

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase logo = Request.Files["uploadlogo"];
                    HttpPostedFileBase coverpic = Request.Files["uploadcoverpic"];

                    if (logo.ContentLength > 0)
                    {
                        response1 = new Liason.Help().uploadfile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["Colleges"]), logo);

                        if (response1 != "ERROR")
                            college.coll_Logo = response1;
                    }

                    if (coverpic.ContentLength > 0)
                    {
                        response1 = new Liason.Help().uploadfile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["Colleges"]), coverpic);

                        if (response1 != "ERROR")
                            college.coll_CoverPic = response1;
                    }

                    //if (coverpic.ContentLength > 0)
                    //{
                    //    response2 = new Liason.Help().uploadfile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["Colleges"]), coverpic);

                    //    if (response2 != "ERROR")
                    //        college.coll_CoverPic = response2;
                    //}
                }

                if (response1 != "ERROR" && response2 != "ERROR")
                {
                    var context = new Entities();

                    using (var dbTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Colleges.Add(college);

                            if (college.coll_ID > 0)
                            {
                                context.Entry(college).State = System.Data.Entity.EntityState.Modified;
                                college.coll_UpdatedDate = DateTime.Now;
                            }

                            context.SaveChanges();

                            //List<CollegeCoursesRelation> collegeCoursesRelation = new List<CollegeCoursesRelation>();
                            //collegeCoursesRelation = context.CollegeCoursesRelations.Where(c => c.collcrsrel_CollegeId == college.coll_ID).ToList();

                            //int[] savedCourses;

                            ////if(collegeCoursesRelation.Count > 0)
                            //{
                            //    savedCourses = collegeCoursesRelation.Select(c => (int)c.collcrsrel_CollegeCourseId).ToArray() ?? new int[] { };

                            //    if(college.SelectedCourses == null)
                            //    {
                            //        college.SelectedCourses = new int[] { };
                            //    }

                            //    // saving new courses
                            //    foreach (int course in college.SelectedCourses)
                            //    {
                            //        if (savedCourses.Contains(course) == false)
                            //        {
                            //            CollegeCoursesRelation rel = new CollegeCoursesRelation();
                            //            rel.collcrsrel_CollegeId = college.coll_ID;
                            //            rel.collcrsrel_CollegeCourseId = course;

                            //            context.CollegeCoursesRelations.Add(rel);
                            //        }
                            //    }

                            //    // deleting removed courses
                            //    foreach (int course in savedCourses)
                            //    {
                            //        if(college.SelectedCourses.Contains(course) == false)
                            //        {
                            //            CollegeCoursesRelation rel = new CollegeCoursesRelation();
                            //            rel = context.CollegeCoursesRelations.Where(c => c.collcrsrel_CollegeId == college.coll_ID && c.collcrsrel_CollegeCourseId == course).SingleOrDefault();

                            //            if(rel != null)
                            //            {
                            //                context.CollegeCoursesRelations.Add(rel);
                            //                context.Entry(rel).State = System.Data.Entity.EntityState.Deleted;
                            //            }
                            //        }
                            //    }

                            //    context.SaveChanges();
                            //}

                            //List<CollegeFacilities> collegeFacilities = new List<CollegeFacilities>();
                            //collegeFacilities = context.CollegeFacilities.Where(c => c.collfac_CollegeId == college.coll_ID).ToList();

                            //int[] savedFacilities;
                            //savedFacilities = collegeFacilities.Select(c => (int)c.collfac_FacilityId).ToArray() ?? new int[] { };

                            //if (college.SelectedFacilites == null)
                            //{
                            //    college.SelectedFacilites = new int[] { };
                            //}

                            //// saving new facilities
                            //foreach (int facility in college.SelectedFacilites)
                            //{
                            //    if (savedFacilities.Contains(facility) == false)
                            //    {
                            //        CollegeFacilities fac = new CollegeFacilities();
                            //        fac.collfac_CollegeId = college.coll_ID;
                            //        fac.collfac_FacilityId = facility;

                            //        context.CollegeFacilities.Add(fac);
                            //    }
                            //}

                            //// deleting removed facilities
                            //foreach (int facility in savedFacilities)
                            //{
                            //    if (college.SelectedFacilites.Contains(facility) == false)
                            //    {
                            //        CollegeFacilities fac = new CollegeFacilities();
                            //        fac = context.CollegeFacilities.Where(c => c.collfac_CollegeId == college.coll_ID && c.collfac_FacilityId == facility).SingleOrDefault();

                            //        if (fac != null)
                            //        {
                            //            context.CollegeFacilities.Add(fac);
                            //            context.Entry(fac).State = System.Data.Entity.EntityState.Deleted;
                            //        }
                            //    }
                            //}

                            //context.SaveChanges();

                            dbTransaction.Commit();
                        }
                        catch (DbEntityValidationException e)
                        {
                            foreach (var eve in e.EntityValidationErrors)
                            {                             
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    ViewBag.Errors.Add(ve);
                                        
                                }
                            }
                            throw;
                        }
                        catch (Exception ex)
                        {
                            dbTransaction.Rollback();
                            ViewBag.ErrorMessage = "Error in Saving College - " + ex.Message;
                            return View(college);
                        }
                    }               
                    
                }

                else
                {
                    ViewBag.ErrorMessage = "Upload Failed - Error in Saving College";
                    return View(college);
                }
                    //return RedirectToAction("SaveCollege", new { collegeId = college.coll_ID, errormessage = "Upload Failed - Error in Saving College" });
                
                return RedirectToAction("SaveCollege", new { collegeId = college.coll_ID, message = "College Saved" });  
            }

            ViewBag.ErrorMessage = "College Name Already Exists";
            //if (institutionid != null && institutionid != 0)
            //{
            //    institution.MainCourses = new Entities().Institutions.Single(i => i.InstitutionId == institutionid).MainCourses;
            //}
            return View(college);
        }


        [HttpPost]
        public ActionResult DeleteCollege(int? collegeId)
        {
            if (db.Colleges.Any(c => c.coll_ID == collegeId))
            {
                using (var context = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (db.CollegeCoursesRelations.Any(c => c.collcrsrel_CollegeId == collegeId))
                        {
                            foreach (CollegeCoursesRelation collCr in db.CollegeCoursesRelations.Where(c => c.collcrsrel_CollegeId == collegeId).ToList())
                            {
                                his_shub_collegecoursesrelation ccr = new his_shub_collegecoursesrelation();
                                Mapper.Map(collCr, ccr);
                                ccr.coll_his_TransactionType = "DELETE";

                                db.his_shub_collegecoursesrelation.Add(ccr);
                                db.CollegeCoursesRelations.Remove(collCr);
                            }
                        }

                        if (db.CollegeFacilities.Any(c => c.collfac_CollegeId == collegeId))
                        {
                            foreach (CollegeFacilities collF in db.CollegeFacilities.Where(c => c.collfac_CollegeId == collegeId).ToList())
                            {
                                his_shub_collegefacilities ccr = new his_shub_collegefacilities();
                                Mapper.Map(collF, ccr);
                                ccr.coll_his_TransactionType = "DELETE";

                                db.his_shub_collegefacilities.Add(ccr);
                                db.CollegeFacilities.Remove(collF);
                            }
                        }

                        if (db.CollegeImages.Any(c => c.collimg_CollegeId == collegeId))
                        {
                            foreach (CollegeImages coll in db.CollegeImages.Where(c => c.collimg_CollegeId == collegeId).ToList())
                            {
                                his_shub_collegeimages ccr = new his_shub_collegeimages();
                                Mapper.Map(coll, ccr);
                                ccr.coll_his_TransactionType = "DELETE";

                                db.his_shub_collegeimages.Add(ccr);
                                db.CollegeImages.Remove(coll);
                            }
                        }

                        Colleges mainColl = db.Colleges.FirstOrDefault(c => c.coll_ID == collegeId);
                        his_shub_colleges his_coll = new his_shub_colleges();
                        Mapper.Map(mainColl, his_coll);
                        his_coll.coll_his_TransactionType = "DELETE";
                        db.his_shub_colleges.Add(his_coll);
                        db.Colleges.Remove(mainColl);

                        db.SaveChanges();
                        context.Commit();
                    }
                    catch (Exception ex)
                    {
                        obj_Help.ExcpetionsHandling(ex);
                        context.Rollback();
                        return RedirectToAction("SaveCollege", new { collegeId = collegeId, errormessage = "Error occured while Deleting" });
                    }
                }
            }

            ViewBag.ErrorMessage = "College Deleted";
            return RedirectToAction("CollegesManager", new { errormessage = "College Deleted" });
            
        }


        [HttpPost]
        public string SaveCollegeCourseDetails(int CollegeId, int CollegeCourseId, string CollegeCourseSeats, string CollegeCourseMode, string CollegeCourseFees, string CollegeCourseDuration)
        {
            string Response = "";

            try
            {
                if (CollegeId > 0 && CollegeCourseId > 0)
                {
                    CollegeCoursesRelation collegeCourseRel = db.CollegeCoursesRelations
                        .Where(c => c.collcrsrel_CollegeId == CollegeId && c.collcrsrel_CollegeCourseId == CollegeCourseId).SingleOrDefault();

                    if (collegeCourseRel == null)
                    {
                        collegeCourseRel = new CollegeCoursesRelation();
                    }

                    collegeCourseRel.collcrsrel_CollegeId = CollegeId;
                    collegeCourseRel.collcrsrel_CollegeCourseId = CollegeCourseId;
                    collegeCourseRel.collcrsrel_Seats = CollegeCourseSeats;
                    collegeCourseRel.collcrsrel_Mode = CollegeCourseMode;
                    collegeCourseRel.collcrsrel_FeeDetails = CollegeCourseFees;
                    collegeCourseRel.collcrsrel_Duration = CollegeCourseDuration;

                    db.CollegeCoursesRelations.Add(collegeCourseRel);

                    if (collegeCourseRel.collcrsrel_Id > 0)
                    {
                        db.Entry(collegeCourseRel).State = System.Data.Entity.EntityState.Modified;
                    }

                    db.SaveChanges();

                    Response = "SAVED";
                }
            }
            catch(Exception ex)
            {
                Response = "ERROR";
            }

            return Response;
        }
        
        [HttpPost]
        public ActionResult GetCollegeCourseDetails(int CollegeId, int CollegeCourseId)
        {
            CollegeCoursesRelation collegeCourseRel = db.CollegeCoursesRelations
                        .Where(c => c.collcrsrel_CollegeId == CollegeId && c.collcrsrel_CollegeCourseId == CollegeCourseId).SingleOrDefault();

            if(collegeCourseRel != null)
            {
                return Json(new string[] { collegeCourseRel.collcrsrel_Duration, collegeCourseRel.collcrsrel_Mode,
                    collegeCourseRel.collcrsrel_Seats, collegeCourseRel.collcrsrel_FeeDetails });
            }

            return Json("NODATA");
        }


        // --------------------------------------------------------------------------- EXAMS
        // GET: admin/SaveCollegeCourse/{collcourseid}
        public ActionResult ExamCoursesManager()
        {
            IEnumerable<ExamCourses> examCourses;

            examCourses = db.ExamCourses.OrderBy(e => e.exmcrs_Name).ToList() ?? new List<ExamCourses>();

            return View(examCourses);
        }

        public ActionResult SaveExamCourse(int? examcourseid)
        {
            ExamCourses examCourse = new ExamCourses();

            if (examcourseid != null && examcourseid > 0)
            {
                examCourse = db.ExamCourses.Where(c => c.exmcrs_Id == examcourseid).SingleOrDefault() ?? new ExamCourses();
            }

            return View(examCourse);
        }


        // POST: admin/SaveExamCourse/{examcourseid}
        [HttpPost]
        public ActionResult SaveExamCourse(int? examcourseid, ExamCourses examCourse)
        {
            ExamCourses tmpExamCourse = new ExamCourses();

            bool exists = false;

            if (examCourse.exmcrs_Id > 0)
                examcourseid = (int)examCourse.exmcrs_Id;

            if (examcourseid == 0 || examcourseid == null)
            {
                tmpExamCourse = db.ExamCourses.Where(i => i.exmcrs_Name == examCourse.exmcrs_Name).FirstOrDefault();

                if (tmpExamCourse != null && tmpExamCourse.exmcrs_Name != "" && tmpExamCourse.exmcrs_Name != null)
                    exists = true;
            }

            else
            {
                tmpExamCourse = db.ExamCourses.Where(c => c.exmcrs_Name == examCourse.exmcrs_Name).FirstOrDefault();

                if (tmpExamCourse != null && tmpExamCourse.exmcrs_Name != "" && tmpExamCourse.exmcrs_Name != null && tmpExamCourse.exmcrs_Id != examcourseid)
                    exists = true;
            }

            if (!exists)
            {
                string response1 = "";
                string response2 = "";

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase logo = Request.Files["uploadlogo"];

                    if (logo.ContentLength > 0)
                    {
                        response1 = new Liason.Help().uploadfile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ExamCourseLogosPath"]), logo);

                        if (response1 != "ERROR")
                            examCourse.exmcrs_Logo = response1;
                    }
                }

                if (response1 != "ERROR" && response2 != "ERROR")
                {
                    var context = new Entities();

                    using (var dbTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.ExamCourses.Add(examCourse);

                            if (examCourse.exmcrs_Id > 0)
                            {
                                examCourse.exmcrs_UpdatedDate = DateTime.Now;
                                context.Entry(examCourse).State = System.Data.Entity.EntityState.Modified;
                            }

                            context.SaveChanges();

                            dbTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbTransaction.Rollback();
                            ViewBag.ErrorMessage = "Error in Saving Exam Course - " + ex.Message;
                            return View(examCourse);
                        }
                    }

                }

                else
                    return RedirectToAction("SaveExamCourse", new { examcourseid = examCourse.exmcrs_Id, errormessage = "Upload Failed - Error in Saving Exam Course" });

                return RedirectToAction("SaveExamCourse", new { examcourseid = examCourse.exmcrs_Id, message = "Exam Course Saved" });
            }

            ViewBag.ErrorMessage = "College Name Already Exists";
            return View(examCourse);
        }


        public ActionResult MockTestUsers()
        {
            var users = db.ad_mock_register.ToList() ?? new List<ad_mock_register>();

            return View(users);
        }


        // --------------------------------------------------------------------------- RESULTS
        #region ----------------- DISTRICTS
        public ActionResult ResultsDistrictsMaster()
        {
            VMResultsDistrictsMaster vMResultsDistrictsMaster = new VMResultsDistrictsMaster();
            vMResultsDistrictsMaster.ExamTypes = _repResults.GetResultsExamTypes();
            vMResultsDistrictsMaster.DistrictsList = _repResults.GetResultsDistricts();

            return View(vMResultsDistrictsMaster);
        }

        [HttpPost]
        public ActionResult ResultsDistrictsMaster(VMResultsDistrictsMaster vMResultsDistrictsMaster)
        {
            string response = _repResults.UpdateResultDistrict(vMResultsDistrictsMaster);

            return RedirectToAction("ResultsDistrictsMaster", new { message = response });
        }

        [HttpPost]
        public ActionResult UploadDistricts(int UploadExamTypeID)
        {
            string message = "Districts Uploaded";
            string response = "";

            if (UploadExamTypeID > 0)
            {
                HttpPostedFileBase file = Request.Files["DistrictsUploadFile"];
                string fileExtension = Path.GetExtension(file.FileName);

                if (file != null && file.ContentLength > 0 && fileExtension.ToLower() == ".txt")
                {
                    response = new Liason.Help().uploadfile(Server.MapPath(ConfigurationManager.AppSettings["ResultsPath"]), file);

                    if (response == "" || response != "ERROR")
                    {
                        message = _repResults.UpdateDistrictsFromUpload(UploadExamTypeID, file);
                    }
                    else
                    {
                        ViewBag.Message = "UPLOAD FAILED - SAVE FAILED!!";
                        message = "UPLOAD FAILED - SAVE FAILED";
                    }
                }
                else
                {
                    message = "Invalid File";
                }
            }
            else
            {
                message = "Invalid Exam Type";
            }

            return RedirectToAction("ResultsDistrictsMaster", new { message = message });
        }

        #endregion

        #region -------------------- COURSES

        public ActionResult ResultsCoursesMaster()
        {
            VMResultsCoursesMaster vMResultsCoursesMaster = new VMResultsCoursesMaster();
            vMResultsCoursesMaster.ResultsExamTypesList = _repResults.GetResultsExamTypes();
            vMResultsCoursesMaster.ResultsCoursesMasterList = _repResults.GetResultsCourses();

            return View(vMResultsCoursesMaster);
        }

        [HttpPost]
        public ActionResult ResultsCoursesMaster(VMResultsCoursesMaster vMResultsCoursesMaster)
        {
            string response = _repResults.UpdateResultsCourse(vMResultsCoursesMaster);
            return RedirectToAction("ResultsCoursesMaster", new { message = response });
        }

        [HttpPost]
        public ActionResult UploadResultsCourses(int UploadExamTypeID)
        {
            string message = "Courses Uploaded";
            string response = "";

            if (UploadExamTypeID > 0)
            {
                HttpPostedFileBase file = Request.Files["CoursesUploadFile"];
                string fileExtension = Path.GetExtension(file.FileName);

                if (file != null && file.ContentLength > 0 && fileExtension.ToLower() == ".txt")
                {
                    response = new Liason.Help().uploadfile(Server.MapPath(ConfigurationManager.AppSettings["ResultsPath"]), file);

                    if (response == "" || response != "ERROR")
                    {
                        message = _repResults.UpdateResultsCoursesFromUpload(UploadExamTypeID, file);
                    }
                    else
                    {
                        ViewBag.Message = "UPLOAD FAILED - SAVE FAILED!!";
                        message = "UPLOAD FAILED - SAVE FAILED";
                    }
                }
                else
                {
                    message = "Invalid File";
                }
            }
            else
            {
                message = "Invalid Exam Type";
            }

            return RedirectToAction("ResultsCoursesMaster", new { message = message });
        }

        #endregion

        #region --------------------- SUBJECTS

        public ActionResult ResultsSubjectsMaster()
        {
            VMResultsSubjectsMaster vMResultsSubjectsMaster = new VMResultsSubjectsMaster();
            vMResultsSubjectsMaster.ResultsExamTypesList = _repResults.GetResultsExamTypes();
            vMResultsSubjectsMaster.ResultsSubjectsList = _repResults.GetResultsSubjects();

            return View(vMResultsSubjectsMaster);
        }

        [HttpPost]
        public ActionResult ResultsSubjectsMaster(VMResultsSubjectsMaster vMResultsSubjectsMaster)
        {
            string response = _repResults.UpdateResultsSubject(vMResultsSubjectsMaster);
            return RedirectToAction("ResultsSubjectsMaster", new { message = response });
        }

        [HttpPost]
        public ActionResult UploadResultsSubjects(int UploadExamTypeID)
        {
            string message = "Subjects Uploaded";
            string response = "";

            if (UploadExamTypeID > 0)
            {
                HttpPostedFileBase file = Request.Files["SubjectsUploadFile"];
                string fileExtension = Path.GetExtension(file.FileName);

                if (file != null && file.ContentLength > 0 && fileExtension.ToLower() == ".txt")
                {
                    response = new Liason.Help().uploadfile(Server.MapPath(ConfigurationManager.AppSettings["ResultsPath"]), file);

                    if (response == "" || response != "ERROR")
                    {
                        message = _repResults.UpdateResultsSubjectsFromUpload(UploadExamTypeID, file);
                    }
                    else
                    {
                        ViewBag.Message = "UPLOAD FAILED - SAVE FAILED!!";
                        message = "UPLOAD FAILED - SAVE FAILED";
                    }
                }
                else
                {
                    message = "Invalid File";
                }
            }
            else
            {
                message = "Invalid Exam Type";
            }

            return RedirectToAction("ResultsSubjectsMaster", new { message = message });
        }

        #endregion

        #region --------------------- TS INTERMEDIATE RESULTS

        public ActionResult TsIntermediateResults()
        {
            VMResultsTsInterMaster vMResultsTsInterMaster = new VMResultsTsInterMaster();
            vMResultsTsInterMaster.ResultsExamTypesList = _repResults.GetResultsExamTypesByState(Constants_States.Telangana);

            return View(vMResultsTsInterMaster);
        }

        [HttpPost]
        public ActionResult TsIntermediateResults(string HallTicketNumber, int ExamType, string ExamYear)
        {
            VMResultsTsInterMaster vMResultsTsInterMaster = new VMResultsTsInterMaster
            {
                ResultsExamTypesList = _repResults.GetResultsExamTypesByState(Constants_States.Telangana),
                IsPostRequest = true
            };

            if (ExamType == (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Regular ||
                    ExamType == (int)Enums_ExamTypes.Telangana_Intermediate_Year_1_Regular)
            {
                vMResultsTsInterMaster.TSYr2RegResult = _repResults.GetTSYr2RegResult(HallTicketNumber, ExamType, ExamYear);
            }
            else if (ExamType == (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Vocational ||
                ExamType == (int)Enums_ExamTypes.Telangana_Intermediate_Year_1_Vocational)
            {
                vMResultsTsInterMaster.TSYr2VocResult = _repResults.GetTSYr2VocResult(HallTicketNumber, ExamType, ExamYear);
            }
            else if (ExamType == (int)Enums_ExamTypes.Telangana_SSC)
            {
                vMResultsTsInterMaster.TSSSCResults = _repResults.GetTSSSCResult(HallTicketNumber, ExamYear);
            }
            else if (ExamType == (int)Enums_ExamTypes.Telangana_EAMCET_Engineering_13)
            {
                vMResultsTsInterMaster.TSEamcetEngResult = _repResults.GetTSEamcetEngResult(HallTicketNumber, ExamYear);
            }
            else if (ExamType == (int)Enums_ExamTypes.Telangana_EAMCET_AgricultureMedical_14)
            {
                vMResultsTsInterMaster.TSEamcetAMResult = _repResults.GetTSEamcetAMResult(HallTicketNumber, ExamYear);
            }
            else if (ExamType == (int)Enums_ExamTypes.TS_POLYCET_16)
            {
                vMResultsTsInterMaster.TSPolycetResult = _repResultsPolytec.GetTSPolycetResult(HallTicketNumber, ExamYear);
            }
            else if (ExamType == (int)Enums_ExamTypes.TS_ICET_18)
            {
                vMResultsTsInterMaster.TSIcetResult = _repResultsIcet.GetTSIcetResult(HallTicketNumber, ExamYear);
            }

            return View(vMResultsTsInterMaster);
        }

        [HttpPost]
        public ActionResult UploadTsInterResults(int UploadExamTypeID, string ExamYear, string ExamMonth, string ResultsReleaseDate)
        {
            string message = "Results Uploaded";
            string responseFileName = "";

            if (UploadExamTypeID > 0)
            {
                HttpPostedFileBase file = Request.Files["ResultsUploadFile"];
                string fileExtension = Path.GetExtension(file.FileName);
                string path = Server.MapPath(ConfigurationManager.AppSettings["ResultsPath"]);

                if (arrTSExamTypesForFileUploadNoTxt.Contains(UploadExamTypeID))
                {
                    path = Server.MapPath(ConfigurationManager.AppSettings["ResultsDoNotDeletePath"]);
                }

                if (file != null && file.ContentLength > 0 && Constants_General.listUploadAllowedFileTypes.Contains(fileExtension.ToLower()))
                {
                    var _help = new Liason.Help();
                    
                    if (fileExtension.ToLower() == ".zip")
                    {
                        responseFileName = _help.extractUploadZipTxtFile(path, file);
                    }
                    else if (Constants_General.listUploadAllowedFileTypes.Contains(fileExtension.ToLower()))
                    {
                        responseFileName = _help.uploadfile(path, file);
                    }

                    if (responseFileName != "" || responseFileName != "ERROR")
                    {
                        if (arrTSExamTypesForFileUploadNoTxt.Contains(UploadExamTypeID))
                        {
                            var uploadResult = _repResults.AddFileUploadLog(UploadExamTypeID, ExamYear, responseFileName, ExamMonth, ResultsReleaseDate);
                            uploadResult.upld_Status = Constants_FileUploadStatus.Done;
                            _repResults.UpdateFileUploadStatus(uploadResult);
                        }
                        else if (UploadExamTypeID != (int)Enums_ExamTypes.Telangana_SSC)
                            message = _repResults.UpdateInterResultsFromUpload(UploadExamTypeID, ExamYear, Path.Combine(path, responseFileName), ExamMonth, ResultsReleaseDate);
                    }
                    else
                    {
                        ViewBag.Message = "UPLOAD FAILED - SAVE FAILED!!";
                        message = "UPLOAD FAILED - SAVE FAILED";
                    }
                }
                else
                {
                    message = "Invalid File";
                }
            }
            else
            {
                message = "Invalid Exam Type";
            }

            return RedirectToAction("TsIntermediateResults", new { message = message });
        }

        #endregion

        #region --------------------- AP INTERMEDIATE RESULTS

        public ActionResult ApIntermediateResults()
        {
            VMResultsApInterMaster vMResultsApInterMaster = new VMResultsApInterMaster();
            vMResultsApInterMaster.ResultsExamTypesList = _repResults.GetResultsExamTypesByState(Constants_States.AndhraPradesh);

            return View(vMResultsApInterMaster);
        }

        [HttpPost]
        public ActionResult ApIntermediateResults(string HallTicketNumber, int ExamType, string ExamYear)
        {
            VMResultsApInterMaster vMResultsApInterMaster = new VMResultsApInterMaster
            {
                ResultsExamTypesList = _repResults.GetResultsExamTypesByState(Constants_States.AndhraPradesh),
                IsPostRequest = true
            };

            if (ExamType == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_1_Regular ||
                    ExamType == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_2_Regular)
            {
                vMResultsApInterMaster.APRegResult = _repResults.GetAPRegResult(HallTicketNumber, ExamType, ExamYear);
            }
            else if (ExamType == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_1_Vocational ||
                ExamType == (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_2_Vocational)
            {
                vMResultsApInterMaster.APVocResult = _repResults.GetAPVocResult(HallTicketNumber, ExamType, ExamYear);
            }
            else if(ExamType == (int)Enums_ExamTypes.Andhra_Pradesh_SSC)
            {
                vMResultsApInterMaster.APSSCResult = _repResults.GetAPSSCResult(HallTicketNumber, ExamYear);
            }
            else if (ExamType == (int)Enums_ExamTypes.Andhra_Pradesh_EAPCET_Engineering_11)
            {
                vMResultsApInterMaster.APEapcetEngResult = _repResults.GetAPEapcetEngResult(HallTicketNumber, ExamYear);
            }
            else if (ExamType == (int)Enums_ExamTypes.Andhra_Pradesh_EAPCET_AgricultureMedical_12)
            {
                vMResultsApInterMaster.APEapcetAMResult = _repResults.GetAPEapcetAMResult(HallTicketNumber, ExamYear);
            }
            else if(ExamType == (int)Enums_ExamTypes.AP_POLYCET_15)
            {
                vMResultsApInterMaster.APPolycetResult = _repResultsPolytec.GetAPPolycetResult(HallTicketNumber, ExamYear);
            }
            else if(ExamType == (int)Enums_ExamTypes.AP_ICET_17)
            {
                vMResultsApInterMaster.APIcetResult = _repResultsIcet.GetAPIcetResult(HallTicketNumber, ExamYear);
            }

            return View(vMResultsApInterMaster);
        }

        [HttpPost]
        public ActionResult UploadApInterResults(int UploadExamTypeID, string ExamYear, string ExamMonth, string ResultsReleaseDate)
        {
            string message = "Results Uploaded";
            string responseFileName = "";

            if (UploadExamTypeID > 0)
            {
                HttpPostedFileBase file = Request.Files["ResultsUploadFile"];
                string fileExtension = Path.GetExtension(file.FileName);
                string path = Server.MapPath(ConfigurationManager.AppSettings["ResultsPath"]);
                
                if (arrAPExamTypesForFileUploadNoTxt.Contains(UploadExamTypeID))
                {
                    path = Server.MapPath(ConfigurationManager.AppSettings["ResultsDoNotDeletePath"]);
                }

                if (file != null && file.ContentLength > 0 && Constants_General.listUploadAllowedFileTypes.Contains(fileExtension.ToLower()))
                {
                    var _help = new Liason.Help();

                    if (fileExtension.ToLower() == ".zip")
                    {
                        responseFileName = _help.extractUploadZipTxtFile(path, file);
                    }
                    else if (Constants_General.listUploadAllowedFileTypes.Contains(fileExtension.ToLower()))
                    {
                        responseFileName = _help.uploadfile(path, file);
                    }

                    if (responseFileName != "" && responseFileName != "ERROR")
                    {
                        if (arrAPExamTypesForFileUploadNoTxt.Contains(UploadExamTypeID))
                        {
                            var uploadResult = _repResults.AddFileUploadLog(UploadExamTypeID, ExamYear, responseFileName, ExamMonth, ResultsReleaseDate);
                            uploadResult.upld_Status = Constants_FileUploadStatus.Done;
                            _repResults.UpdateFileUploadStatus(uploadResult);
                        }
                        else if (UploadExamTypeID != (int)Enums_ExamTypes.Andhra_Pradesh_SSC)
                            message = _repResults.UpdateInterResultsFromUpload(UploadExamTypeID, ExamYear, Path.Combine(path, responseFileName), ExamMonth, ResultsReleaseDate);
                    }
                    else
                    {
                        ViewBag.Message = "UPLOAD FAILED - SAVE FAILED!!";
                        message = "UPLOAD FAILED - SAVE FAILED";
                    }
                }
                else
                {
                    message = "Invalid File";
                }
            }
            else
            {
                message = "Invalid Exam Type";
            }

            return RedirectToAction("ApIntermediateResults", new { message = message });
        }

        #endregion

        #region CONSTANTS

        readonly int[] arrAPExamTypesForFileUploadNoTxt = new int[] { (int)Enums_ExamTypes.Andhra_Pradesh_SSC, 
                                                                   (int)Enums_ExamTypes.Andhra_Pradesh_EAPCET_Engineering_11, 
                                                                   (int)Enums_ExamTypes.Andhra_Pradesh_EAPCET_AgricultureMedical_12,
                                                                   (int)Enums_ExamTypes.AP_ICET_17,
                                                                   (int)Enums_ExamTypes.AP_POLYCET_15};


        readonly int[] arrTSExamTypesForFileUploadNoTxt = new int[] { (int)Enums_ExamTypes.Telangana_SSC,
                                                                   (int)Enums_ExamTypes.Telangana_EAMCET_Engineering_13,
                                                                   (int)Enums_ExamTypes.Telangana_EAMCET_AgricultureMedical_14,
                                                                   (int)Enums_ExamTypes.TS_ICET_18,
                                                                   (int)Enums_ExamTypes.TS_POLYCET_16};

        #endregion
    }
}
