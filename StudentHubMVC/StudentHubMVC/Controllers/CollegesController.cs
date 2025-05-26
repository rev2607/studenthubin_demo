using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentHubMVC.Models;
using StudentHubMVC.Models.ViewModels.Home;
using StudentHubMVC.Liason;
using System.Configuration;
using PagedList;

namespace StudentHubMVC.Controllers
{
    public class CollegesController : Controller
    {
        Entities db = new Entities();
        MColleges obj_Colleges = new MColleges();

        // GET: College
        public ActionResult Index(VMHomeCollegesSearch collegeSearch, int? page)
        {
            VMHomeCollegesSearch tmpSearch = new VMHomeCollegesSearch();
            collegeSearch.CollegeStates = obj_Colleges.GetCollegeStates() ?? new string[] { };
            collegeSearch.CollegeCities = obj_Colleges.GetCollegeCities() ?? new string[] { };
            collegeSearch.CollegeTypes = obj_Colleges.GetCollegeTypes() ?? new Dictionary<int, string>();
            collegeSearch.CollegeCourses = obj_Colleges.GetCollegeCourses() ?? new Dictionary<int, string>();
            
            return View(GetCollegeSearchResults(collegeSearch));
        }

        private VMHomeCollegesSearch GetCollegeSearchResults(VMHomeCollegesSearch collegeSearch)
        {
            int page = int.Parse(collegeSearch.page);
            List<Colleges> colleges = new Entities().Colleges.Where(c => c.coll_Status == Constants.Active).ToList();

            if (string.IsNullOrEmpty(collegeSearch.searchType) == false)
            {
                string[] searchTypes = collegeSearch.searchType.ToLower().Split(',');
                colleges = colleges.Where(c => searchTypes.Contains(c.coll_Type.ToString())).ToList();
            }
            if (string.IsNullOrEmpty(collegeSearch.searchCity) == false)
            {
                string[] searchCities = collegeSearch.searchCity.ToLower().Split(',');
                colleges = colleges.Where(c => searchCities.Contains(c.coll_City.ToLower())).ToList();
            }
            if (string.IsNullOrEmpty(collegeSearch.searchState) == false)
            {
                string[] searchStates = collegeSearch.searchState.ToLower().Split(',');
                colleges = colleges.Where(c => searchStates.Contains(c.coll_State.ToLower())).ToList();
            }
            if (string.IsNullOrEmpty(collegeSearch.searchCourse) == false)
            {
                string[] searchCourses = collegeSearch.searchCourse.ToLower().Split(',');
                long[] collegesWithCourse = new Entities().CollegeCoursesRelations.Where(c => searchCourses.Contains(c.collcrsrel_CollegeCourseId.ToString())).Select(c => c.collcrsrel_CollegeId).ToArray() ?? new long[] { };

                colleges = colleges.Where(c => collegesWithCourse.Contains(c.coll_ID)).ToList();
            }

            colleges = colleges.OrderBy(i => i.coll_Name).ToList();
            collegeSearch.totalResults = colleges.Count;

            int resultsPerPage = int.Parse(ConfigurationManager.AppSettings["Paging15"]);
            collegeSearch.totalPages = (colleges.Count % resultsPerPage) == 0 ? (colleges.Count / resultsPerPage) : (colleges.Count / resultsPerPage) + 1;
            collegeSearch.lastPage = collegeSearch.totalPages;

            if (colleges != null && colleges.Count > 0)
            {
                colleges = colleges.OrderBy(i => i.coll_Name).Skip((page - 1) * resultsPerPage).Take(resultsPerPage).ToList();

                if (colleges.Count >= resultsPerPage)
                {
                    if (page > 1)
                    {
                        collegeSearch.previousPage = (page - 1).ToString();
                        collegeSearch.PreviousPageExist = true;
                    }
                    else
                        collegeSearch.PreviousPageExist = false;

                    collegeSearch.nextPage = (page + 1).ToString();
                    collegeSearch.NextPageExist = true;
                }
                else
                {
                    if (page > 1)
                    {
                        collegeSearch.previousPage = (page - 1).ToString();
                        collegeSearch.PreviousPageExist = true;
                    }
                    else
                        collegeSearch.PreviousPageExist = false;

                    //collegeSearch.nextPage = (page + 1).ToString();
                    collegeSearch.NextPageExist = false;
                }
            }

            collegeSearch.Colleges = colleges;

            return collegeSearch;
        }

        [HttpPost]
        public ActionResult GetCollegeSearchResultsView(VMHomeCollegesSearch collegeSearch)
        {
            return View("__SearchResults", GetCollegeSearchResults(collegeSearch));
        }

        // GET: Details/name
        public ActionResult Details(string name = "")
        {
            ViewBag.name = name;

            Colleges college = new Colleges();

            if (name != "")
            {
                if (db.Colleges.Where(c => c.coll_UrlKeyword == name).Any())
                {
                    college = db.Colleges.Where(c => c.coll_UrlKeyword == name).FirstOrDefault();
                }
            }

            return View(college);
        }

    }
}