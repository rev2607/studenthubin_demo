using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason.EntityLayer
{
    public class CourseChaptersEN
    {
        [DisplayName("Course Section")]
        public string SectionID { get; set; }

        [DisplayName("Chapter Title")]
        public string ChapterTitle { get; set; }

        [DisplayName("Sort ID")]
        public string SortId { get; set; }

        [DisplayName("Chapter Description")]
        public string Description { get; set; }

        [DisplayName("Video Link 1")]
        public string VideoLink1 { get; set; }

        [DisplayName("Duration")]
        public string Video1Duration { get; set; }

        [DisplayName("Video Link 2")]
        public string VideoLink2 { get; set; }

        [DisplayName("Duration")]
        public string Video2Duration { get; set; }

        public string Sno { get; set; }
        public string UserId { get; set; }
    }
}