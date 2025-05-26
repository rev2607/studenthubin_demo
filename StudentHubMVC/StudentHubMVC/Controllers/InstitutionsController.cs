using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentHubMVC.Models.ViewModels.Home;
using StudentHubMVC.Liason;
using System.Configuration;

namespace StudentHubMVC.Controllers
{
    public class InstitutionsController : Controller
    {
        // GET: Institutions
        Entities db = new Entities();
        MInstitutions obj_Institutes = new MInstitutions();
        Liason.DropdownTypes obj_DropdownTypes = new Liason.DropdownTypes();

        // GET: College
        public ActionResult Index(VMHomeInstitutionSearch instituteSearch)
        {
            VMHomeInstitutionSearch tmpSearch = new VMHomeInstitutionSearch();
            instituteSearch.instituionTypes = obj_DropdownTypes.GetDropdownTypesDictionary(Constants.InstitutionTypes) ?? new Dictionary<int, string>();
            instituteSearch.instituionCities = obj_Institutes.GetInstitutionCities() ?? new string[] { };
            instituteSearch.instituionArea = obj_Institutes.GetInstitutionAreas() ?? new string[] { };
            instituteSearch.instituionCourses = obj_Institutes.GetInstitutionCourses() ?? new Dictionary<int, string>();
            instituteSearch.instituionTrainingModes = obj_DropdownTypes.GetDropdownTypesDictionary(Constants.TrainingMode) ?? new Dictionary<int, string>();

            return View(GetInstituteSearchResults(instituteSearch));
        }

        private VMHomeInstitutionSearch GetInstituteSearchResults(VMHomeInstitutionSearch instituteSearch)
        {
            int page = int.Parse(instituteSearch.page);
            List<Institutions> institutions = new Entities().Institutions.Where(i => i.Status == Constants.Active).ToList();

            if (string.IsNullOrEmpty(instituteSearch.searchType) == false)
            {
                string[] searchTypes = instituteSearch.searchType.ToLower().Split(',');
                institutions = institutions.Where(c => searchTypes.Contains(c.Type.ToString())).ToList();
            }
            if (string.IsNullOrEmpty(instituteSearch.searchCity) == false)
            {
                string[] searchCities = instituteSearch.searchCity.ToLower().Split(',');
                institutions = institutions.Where(c => searchCities.Contains(c.City.ToLower())).ToList();
            }
            if (string.IsNullOrEmpty(instituteSearch.searchArea) == false)
            {
                string[] searchStates = instituteSearch.searchArea.ToLower().Split(',');
                institutions = institutions.Where(c => searchStates.Contains(c.Area.ToLower())).ToList();
            }
            if (string.IsNullOrEmpty(instituteSearch.searchCourse) == false)
            {
                string[] searchCourses = instituteSearch.searchCourse.ToLower().Split(',');
                long[] instititutesWithCourse = new Entities().InstitutionCourses.Where(c => searchCourses.Contains(c.CourseId.ToString())).Select(c => c.InstitutionId).ToArray() ?? new long[] { };

                institutions = institutions.Where(c => instititutesWithCourse.Contains(c.InstitutionId)).ToList();
            }

            institutions = institutions.OrderBy(i => i.Name).ToList();
            instituteSearch.totalResults = institutions.Count;

            int resultsPerPage = int.Parse(ConfigurationManager.AppSettings["Paging25"]);
            instituteSearch.totalPages = (institutions.Count % resultsPerPage) == 0 ? (institutions.Count / resultsPerPage) : (institutions.Count / resultsPerPage) + 1;
            instituteSearch.lastPage = instituteSearch.totalPages;

            if (institutions != null && institutions.Count > 0)
            {
                institutions = institutions.OrderBy(i => i.Name).Skip((page - 1) * resultsPerPage).Take(resultsPerPage).ToList();

                if (institutions.Count >= resultsPerPage)
                {
                    if (page > 1)
                    {
                        instituteSearch.previousPage = (page - 1).ToString();
                        instituteSearch.PreviousPageExist = true;
                    }
                    else
                        instituteSearch.PreviousPageExist = false;

                    instituteSearch.nextPage = (page + 1).ToString();
                    instituteSearch.NextPageExist = true;
                }
                else
                {
                    if (page > 1)
                    {
                        instituteSearch.previousPage = (page - 1).ToString();
                        instituteSearch.PreviousPageExist = true;
                    }
                    else
                        instituteSearch.PreviousPageExist = false;

                    //instituteSearch.nextPage = (page + 1).ToString();
                    instituteSearch.NextPageExist = false;
                }
            }

            instituteSearch.Institutions = institutions;

            return instituteSearch;
        }

        [HttpPost]
        public ActionResult GetInstituteSearchResultsView(VMHomeInstitutionSearch instituteSearch)
        {
            return View("__SearchResults", GetInstituteSearchResults(instituteSearch));
        }

        // GET: Details/name
        public ActionResult Details(string name = "")
        {
            ViewBag.name = name;

            Institutions institute = new Institutions();

            if (name != "")
            {
                if (db.Institutions.Where(c => c.UrlKeyword == name).Any())
                {
                    institute = db.Institutions.Where(c => c.UrlKeyword == name).FirstOrDefault();
                }
            }

            return View(institute);
        }
    }
}