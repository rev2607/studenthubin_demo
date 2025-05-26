using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels.Home
{
    public class VMHomeInstitutionSearch
    {
        public VMHomeInstitutionSearch()
        {
            searchType = "";
            searchCity = "";
            searchArea = "";
            searchCourse = "";
            searchTrainingMode = "";
            instituionCities = new string[] { };
            instituionArea = new string[] { };
            instituionTypes = new Dictionary<int, string>();
            instituionCourses = new Dictionary<int, string>();
            instituionTrainingModes = new Dictionary<int, string>();
            NextPageExist = true;
            PreviousPageExist = true;
            page = "1";

            Institutions = new List<Institutions>();
        }

        public string searchKeyword { get; set; }
        public string searchType { get; set; }
        public string searchCity { get; set; }
        public string searchArea { get; set; }
        public string searchCourse { get; set; }
        public string searchTrainingMode { get; set; }

        public Dictionary<int, string> instituionTypes { get; set; }
        public string[] instituionCities { get; set; }
        public string[] instituionArea { get; set; }
        public Dictionary<int, string> instituionCourses { get; set; }
        public Dictionary<int, string> instituionTrainingModes { get; set; }

        public string page { get; set; }
        public string previousPage { get; set; }
        public string nextPage { get; set; }
        public int lastPage { get; set; }
        public int totalPages { get; set; }
        public int totalResults { get; set; }

        public bool NextPageExist { get; set; }
        public bool PreviousPageExist { get; set; }

        public List<Institutions> Institutions { get; set; }
    }
}