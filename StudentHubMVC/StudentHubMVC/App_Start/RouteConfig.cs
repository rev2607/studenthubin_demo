using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentHubMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("");

            routes.MapRoute(
                name: "404-NotFound",
                url: "NotFound",
                defaults: new { controller = "Error", action = "NotFound" }
            );

            routes.MapRoute(
                name: "500-Error",
                url: "Error",
                defaults: new { controller = "Error", action = "Error" }
            );


            // ------------------------------------------------------------------------- ADMIN PANEL

            routes.MapRoute(
                name: "AdminLogin",
                url: "admin/login",
                defaults: new { controller = "Admin", action = "LoginAdmin" }
            );

            routes.MapRoute(
                name: "AdminLogout",
                url: "admin/logout",
                defaults: new { controller = "Admin", action = "Logout" }
            );

            routes.MapRoute(
                name: "AdminDashboard",
                url: "admin/dashboard",
                defaults: new { controller = "Admin", action = "Dashboard" }
            );


            // STUDENT
            routes.MapRoute(
                name: "StudentsManager",
                url: "admin/studentsmanager",
                defaults: new { controller = "Admin", action = "StudentsManager" }
            );

            routes.MapRoute(
                name: "AddStudent",
                url: "admin/addstudent",
                defaults: new { controller = "Admin", action = "AddStudent" }
            );  

            routes.MapRoute(
                name: "SaveStudent",
                url: "admin/savestudent/{id}",
                defaults: new { controller = "Admin", action = "SaveStudent", id = "" }
            );


            // COURSE
            routes.MapRoute(
                name: "CoursesManager",
                url: "admin/coursesmanager",
                defaults: new { controller = "Admin", action = "CoursesManager" }
            );

            routes.MapRoute(
                name: "AddCourse",
                url: "admin/addcourse",
                defaults: new { controller = "Admin", action = "AddCourse" }
            );

            routes.MapRoute(
                name: "SaveCourse",
                url: "admin/savecourse/{id}",
                defaults: new { controller = "Admin", action = "SaveCourse", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "AddCourseContent",
            //    url: "admin/addcoursecontent",
            //    defaults: new { controller = "Admin", action = "AddCourseContent", course = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "TutorialsManager",
                url: "admin/tutorialsmanager",
                defaults: new { controller = "Admin", action = "TutorialsManager", course = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SaveCourseContent",
                url: "admin/savetutorials/{course}",
                defaults: new { controller = "Admin", action = "SaveCourseContent", course = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AddSection",
                url: "admin/addsection/{course}",
                defaults: new { controller = "Admin", action = "AddSection", course = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SaveSection",
                url: "admin/savesection/{course}/{sectionid}",
                defaults: new { controller = "Admin", action = "SaveSection", course = UrlParameter.Optional, sectionid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AddTest",
                url: "admin/addtest/{course}/{sectionid}",
                defaults: new { controller = "Admin", action = "AddTest", course = UrlParameter.Optional, sectionid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SaveTest",
                url: "admin/savetest/{course}/{sectionid}/{testid}",
                defaults: new { controller = "Admin", action = "SaveTest", course = UrlParameter.Optional, sectionid = UrlParameter.Optional, testid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DeleteTest",
                url: "admin/deletetest/{course}/{sectionid}/{testid}",
                defaults: new { controller = "Admin", action = "DeleteTest", course = UrlParameter.Optional, sectionid = UrlParameter.Optional, testid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UploadMaterial",
                url: "admin/uploadmaterial/{course}/{sectionid}",
                defaults: new { controller = "Admin", action = "UploadMaterial", course = UrlParameter.Optional, sectionid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DeleteMaterials",
                url: "admin/deletematerial/{course}/{sectionid}/{fileid}",
                defaults: new { controller = "Admin", action = "DeleteMaterial", course = UrlParameter.Optional, sectionid = UrlParameter.Optional, fileid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TestQuestions",
                url: "admin/testquestions/{course}/{sectionid}/{testid}",
                defaults: new { controller = "Admin", action = "TestQuestions", course = UrlParameter.Optional, sectionid = UrlParameter.Optional, testid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DeleteQuestion",
                url: "admin/deletequestion/{course}/{sectionid}/{testid}/{questionid}",
                defaults: new { controller = "Admin", action = "DeleteQuestion", course = UrlParameter.Optional, sectionid = UrlParameter.Optional, testid = UrlParameter.Optional, questionid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AddChapter",
                url: "admin/addchapter/{course}/{sectionid}",
                defaults: new { controller = "Admin", action = "AddChapter", course = UrlParameter.Optional, sectionid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SaveChapter",
                url: "admin/savechapter/{course}/{sectionid}/{chapterid}",
                defaults: new { controller = "Admin", action = "SaveChapter", course = UrlParameter.Optional, sectionid = UrlParameter.Optional, chapterid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DeleteChapter",
                url: "admin/deletechapter/{course}/{sectionid}/{chapterid}",
                defaults: new { controller = "Admin", action = "DeleteChapter", course = UrlParameter.Optional, sectionid = UrlParameter.Optional, chapterid = UrlParameter.Optional }
            );


            // INSTRUCTOR
            routes.MapRoute(
                name: "InstructorsManager",
                url: "admin/instructorsmanager",
                defaults: new { controller = "Admin", action = "InstructorsManager" }
            );

            routes.MapRoute(
                name: "AddInstructor",
                url: "admin/addinstructor",
                defaults: new { controller = "Admin", action = "AddInstructor" }
            );

            routes.MapRoute(
                name: "SaveInstructor",
                url: "admin/saveinstructor/{id}",
                defaults: new { controller = "Admin", action = "SaveInstructor", id = "" }
            );


            // BATCHES
            routes.MapRoute(
                name: "BatchesManager",
                url: "admin/batchesmanager",
                defaults: new { controller = "Admin", action = "BatchesManager" }
            );

            routes.MapRoute(
                name: "AddBatch",
                url: "admin/addbatch",
                defaults: new { controller = "Admin", action = "AddBatch" }
            );

            routes.MapRoute(
                name: "SaveBatch",
                url: "admin/saveBatch/{id}",
                defaults: new { controller = "Admin", action = "SaveBatch", id = "" }
            );

            routes.MapRoute(
                name: "BatchStudents",
                url: "admin/batchstudents/{id}",
                defaults: new { controller = "Admin", action = "BatchStudents", id = "" }
            );

            routes.MapRoute(
                name: "BatchStudentActivate",
                url: "admin/batchstudentactivate/{BatchId}/{StudentId}/{Mode}",
                defaults: new { controller = "Admin", action = "StudentsBatchActivate", BatchId = "", StudentId = "", Mode = "" }
            );


            // ENQUIRIES
            routes.MapRoute(
                name: "Enquiries",
                url: "admin/enquiries",
                defaults: new { controller = "Admin", action = "Enquiries" }
            );


            // VOUCHERS
            routes.MapRoute(
                name: "Vouchers",
                url: "admin/vouchersmanager",
                defaults: new { controller = "Admin", action = "VouchersManager" }
            );

            routes.MapRoute(
                name: "VouchersSave",
                url: "admin/vouchersmanager/{id}",
                defaults: new { controller = "Admin", action = "SaveVoucher", id = "" }
            );


            // PLACEMENTS
            routes.MapRoute(
                name: "Placements",
                url: "admin/placementsmanager",
                defaults: new { controller = "Admin", action = "PlacementsManager" }
            );

            routes.MapRoute(
                name: "PlacementSave",
                url: "admin/placementsmanager/{id}",
                defaults: new { controller = "Admin", action = "SavePlacement", id = "" }
            );


            // SETTINGS
            routes.MapRoute(
                name: "Settings",
                url: "admin/settings",
                defaults: new { controller = "Admin", action = "Settings"}
            );


            // PAGES
            routes.MapRoute(
                name: "PagesManager",
                url: "admin/pagesmanager",
                defaults: new { controller = "Admin", action = "PagesManager" }
            );


            routes.MapRoute(
                name: "PagesManagerGet",
                url: "admin/pagesmanager/{id}",
                defaults: new { controller = "Admin", action = "PageManagerGet", id = "" }
            );


            // DROPDOWN TYPES
            routes.MapRoute(
                name: "DropdownTypes",
                url: "admin/grouptype/{id}",
                defaults: new { controller = "Admin", action = "DropdownTypes", id = UrlParameter.Optional }
            );


            // MOCK TESTS
            routes.MapRoute(
                name: "MockTestsManager",
                url: "admin/mocktestsmanager",
                defaults: new { controller = "Admin", action = "MockTestsManager", course = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "MockTestSave",
                url: "admin/savemocktest/{courseid}/{id}",
                defaults: new { controller = "Admin", action = "SaveMockTest", courseid = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "MockTestUsers",
                url: "admin/MockTestUsers",
                defaults: new { controller = "Admin", action = "MockTestUsers" }
            );


            // MOCK TEST QUESTIONS
            routes.MapRoute(
                name: "MockTestQuestionSave",
                url: "admin/savemocktestquestions/{courseid}/{testid}/{questionid}",
                defaults: new { controller = "Admin", action = "SaveMockTestQuestions", testid = UrlParameter.Optional, questionid = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "MockTestQuestionSave2",
                url: "admin/savemocktestquestions2/{courseid}/{testid}",
                defaults: new { controller = "Admin", action = "SaveMockTestQuestions2", testid = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "GetMockTestQuestion",
                url: "admin/GetQuestionData/{MockTestQuestionId}",
                defaults: new { controller = "Admin", action = "GetQuestionData", MockTestQuestionId = UrlParameter.Optional }
            );


            // NEWS ALERTS
            routes.MapRoute(
                name: "NewsAlerts",
                url: "admin/newsalerts",
                defaults: new { controller = "Admin", action = "NewsAlertsManager" }
            );

            routes.MapRoute(
                name: "NewsAlertAdd",
                url: "admin/savenewsalert/{newsid}",
                defaults: new { controller = "Admin", action = "SaveNewsAlert", newsid = UrlParameter.Optional }
            );


            // JOB ALERTS
            routes.MapRoute(
                name: "JobAlertsManager",
                url: "admin/jobalerts",
                defaults: new { controller = "Admin", action = "JobAlertsManager" }
            );

            routes.MapRoute(
                name: "JobAlertAdd",
                url: "admin/savejobalert/{jobid}",
                defaults: new { controller = "Admin", action = "SaveJobAlert", jobid = UrlParameter.Optional }
            );


            // QUESTION PAPERS
            routes.MapRoute(
                name: "QuestionPapersManager",
                url: "admin/questionpapersmanager/{courseid}",
                defaults: new { controller = "Admin", action = "QuestionPapersManager", courseid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "QuestionPaperSave",
                url: "admin/savequestionpaper/{questionpaperid}",
                defaults: new { controller = "Admin", action = "SaveQuestionPaper", questionpaperid = UrlParameter.Optional}
            );


            // INSTITUTIONS
            routes.MapRoute(
                name: "InstitutionsManager",
                url: "admin/institutionsmanager",
                defaults: new { controller = "Admin", action = "InstitutionsManager" }
            );

            //routes.MapRoute(
            //    name: "InstitutionSave",
            //    url: "admin/saveinstitution/{institutionid}",
            //    defaults: new { controller = "Admin", action = "SaveInstitution", institutionid = UrlParameter.Optional }
            //);


            // INSTITUTION COURSES
            routes.MapRoute(
                name: "InstitutionCourseSave",
                url: "admin/saveinstitutioncourse/{institutionid}/{InstitutionCourseId}",
                defaults: new { controller = "Admin", action = "SaveInstitutionCourse", institutionid = UrlParameter.Optional, InstitutionCourseId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "InstitutionCourseDelete",
                url: "admin/delinstitutioncourse/{institutionid}/{InstitutionCourseId}",
                defaults: new { controller = "Admin", action = "DeleteInstitutionCourse", institutionid = UrlParameter.Optional, InstitutionCourseId = UrlParameter.Optional }
            );

            // ------------------------------------------------------------------------- DEFAULT
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] { "MvcArea.Controllers" }
            //);

            routes.MapRoute(
                name: "DefaultRoute",
                url: "{controller}/{action}",
                defaults: new { controller = "", action = "Index" }
            );


            //Catch All InValid (NotFound) Routes
            routes.MapRoute(
                name: "NotFound",
                url: "{*url}",
                defaults: new { controller = "Error", action = "NotFound" }
            );
        }
    }
}
