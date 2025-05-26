using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason.EntityLayer
{
    public class PlacementEN
    {
        [DisplayName("Student Name")]
        public string StudentName { get; set; }
        public string Gender { get; set; }

        [DisplayName("Company")]
        public string CompanyName { get; set; }
        public string Photo { get; set; }

        [DisplayName("Job Position")]
        public string JobPosition { get; set; }
        public string City { get; set; }

        [DisplayName("Company Logo")]
        public string CompanyLogo { get; set; }

        public string Sno { get; set; }
        public string UserId { get; set; }
    }
}