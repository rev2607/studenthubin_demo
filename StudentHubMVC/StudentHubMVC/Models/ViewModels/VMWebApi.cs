using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels
{
    public class VMWebApi
    {
        public VMWebApi()
        {
            ResultsSearchFilters = new VMResultsSearch();
            WebApiList = new List<ExamWebApi>();
        }

        public VMResultsSearch ResultsSearchFilters { get; set; }
        public List<ExamWebApi> WebApiList { get; set;}
    }
}