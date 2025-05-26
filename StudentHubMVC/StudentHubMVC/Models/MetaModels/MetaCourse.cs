using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace StudentHubMVC
{
    [MetadataType(typeof(MetaCourse))]
    public partial class MainCourses
    {
    }

    public class MetaCourse
    {
        [DisplayName("URL Keyword")]
        public string UrlKeyword { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [AllowHtml]
        public string Syllabus { get; set; }

        [AllowHtml]
        public string Eligibility { get; set; }

        [DisplayName("Is In TopList?")]
        public string IsInTopList { get; set; }

        [DisplayName("Is Featured?")]
        public string IsFeatured { get; set; }

        [DisplayName("Is Trending?")]
        public string IsTrending { get; set; }

    }


}