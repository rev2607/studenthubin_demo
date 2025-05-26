using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels
{
    public class NewsAlertsInfo
    {
        public long NewsId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public string CreatedDate { get; set; }
        public string UrlKeyWord { get; set; }
        public string KeyWords { get; set; }
        public long NewsTypeId { get; set; }

        public string NewsType { get; set; }
    }
}