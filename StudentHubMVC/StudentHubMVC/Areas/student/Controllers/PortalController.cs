using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentHubMVC.Areas.student.Models;

namespace StudentHubMVC.Areas.student.Controllers
{
    public class PortalController : Controller
    {
        Entities db = new Entities();

        // GET: student/Panel
        public ActionResult Index()
        {
            return View();
        }


        // ----------------------------------------------------------------------- MOCK TESTS
        // GET: student/Portal/MockTests
        public ActionResult MockTests()
        {
            IEnumerable<VM_MockTestsList> MockTestsList = db.VM_MockTestsList.ToList();
            return View(MockTestsList);
        }


        // GET: student/Portal/MockTestDetails
        public ActionResult MockTestDetails(int id)
        {
            VM_MockTestTypesList courseMockTestTypes = new VM_MockTestTypesList();

            List<MockTests> courseTests = db.MockTests.Where(m => m.CourseId == id).ToList();
            List<string> testDifficultyTypes = db.MockTests.Where(m => m.CourseId == id).GroupBy(d => d.Difficulty).Select(m => m.FirstOrDefault()).ToList().Select(m => m.Difficulty).ToList();
            List<MockTestDetails> testDetailsList = new List<MockTestDetails>();

            foreach(MockTests test in courseTests)
            {
                MockTestDetails testDetails = new MockTestDetails();

                testDetails.MockTestId = test.MockTestId;
                testDetails.Difficulty = test.Difficulty;
                testDetails.PercentCompleted = 90;
                testDetails.DateStarted = "Jan 20";
                testDetails.Status = "Completed";
                testDetails.TotalQuestions = 25;
                testDetails.AnsweredQuestions = 22;

                testDetailsList.Add(testDetails);
            }

            courseMockTestTypes.CourseId = id;
            courseMockTestTypes.CourseName = db.MainCourses.Where(c => c.MainCourseId == id).SingleOrDefault().Name;
            courseMockTestTypes.MockTestDetails = testDetailsList;
            courseMockTestTypes.DifficultyTypes = testDifficultyTypes;

            ViewBag.course = id;
            return View(courseMockTestTypes);
        }


        // GET: student/Portal/MockTest/id
        public ActionResult MockTest(int id, int qid = 1)
        {
            VM_MockTestQuestion testQuestion = new VM_MockTestQuestion();
            MockTestQuestions questionDetails = new MockTestQuestions();

            questionDetails = db.MockTestQuestions.Where(q => q.MockTestId == id).OrderBy(q => q.QuestionID).Skip(qid - 1).FirstOrDefault();// .ElementAtOrDefault(qid - 1);

            MockTests mockTest = db.MockTests.Where(m => m.MockTestId == id).SingleOrDefault();
            testQuestion.CourseName = db.MainCourses.Where(c => c.MainCourseId == mockTest.CourseId).SingleOrDefault().Name;
            testQuestion.Difficulty = mockTest.Difficulty;
            testQuestion.QuestionSNo = qid.ToString();
            testQuestion.TotalQuestions = db.MockTestQuestions.Where(q => q.MockTestId == id).Count().ToString();
            testQuestion.QuestionDetails = questionDetails;
            return View(testQuestion);
        }


        // ----------------------------------------------------------------------- TUTORIALS
        // GET: student/Portal/Tutorials
        public ActionResult Tutorials()
        {

            return View();
        }


        // GET: student/Portal/Tutorial
        public ActionResult Tutorial(string id)
        {
            ViewBag.course = id;
            return View();
        }


        // ----------------------------------------------------------------------- INSTITUTIONS
        // GET: student/Portal/Institutions
        public ActionResult Institutions()
        {

            return View();
        }
    }
}