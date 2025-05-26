using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels.Home
{
    public class VMHomeCollegesSearch
    {
        public VMHomeCollegesSearch()
        {
            searchType = "";
            searchCity = "";
            searchState = "";
            searchCourse = "";
            CollegeCities = new string[] { };
            CollegeStates = new string[] { };
            CollegeTypes = new Dictionary<int, string>();
            CollegeCourses = new Dictionary<int, string>();
            NextPageExist = true;
            PreviousPageExist = true;
            page = "1";

            Colleges = new List<Colleges>();
        }

        public string searchKeyword { get; set; }
        public string searchType { get; set; }
        public string searchCity { get; set; }
        public string searchState { get; set; }
        public string searchCourse { get; set; }

        public Dictionary<int, string> CollegeTypes { get; set; }
        public string[] CollegeCities { get; set; }
        public string[] CollegeStates { get; set; }
        public Dictionary<int, string> CollegeCourses { get; set; }

        public string page { get; set; }
        public string previousPage { get; set; }
        public string nextPage { get; set; }
        public int lastPage { get; set; }
        public int totalPages { get; set; }
        public int totalResults { get; set; }

        public bool NextPageExist { get; set; }
        public bool PreviousPageExist { get; set; }

        public List<Colleges> Colleges { get; set; }
    }
}