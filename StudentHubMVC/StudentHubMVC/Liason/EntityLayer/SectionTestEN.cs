using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;


namespace StudentHubMVC.Liason.EntityLayer
{
    public class SectionTestEN
    {
        [DisplayName("Course Section")]
        public string SectionId { get; set; }
        public string Difficulty { get; set; }

        public string Sno { get; set; }
        public string UserId { get; set; }
    }
}