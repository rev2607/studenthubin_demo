using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason.EntityLayer
{
    public class CourseEN
    {
        public string Name { get; set; }

        [DisplayName ("URL Name")]
        public string UrlName { get; set; }
        public string Fee { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Featured { get; set; }
        public string Duration { get; set; }
        public string Logo { get; set; }
        public string CoverPicture { get; set; }
        public string Curriculum { get; set; }
        public string DemoLink { get; set; }
        public string DemoLink2 { get; set; }

        public string Sno { get; set; }
        public string UserId { get; set; }
    }
}