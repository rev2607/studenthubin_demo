using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason.EntityLayer
{
    public class SectionEN
    {
        [DisplayName("Course Name")]
        public string CourseId { get; set; }

        [DisplayName("Section Title")]
        public string Title { get; set; }
        public string Description { get; set; }

        [DisplayName("Serial Number")]
        public string SortId { get; set; }

        public string Sno { get; set; }
        public string UserId { get; set; }
    }
}