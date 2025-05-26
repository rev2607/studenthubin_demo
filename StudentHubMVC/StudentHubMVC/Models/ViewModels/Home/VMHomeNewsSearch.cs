using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels.Home
{
    public class VMHomeNewsSearch
    {
        public string Keyword { get; set; }
        public string Type { get; set; }

        public IPagedList<NewsAlerts> NewsAlerts { get; set; }
    }
}