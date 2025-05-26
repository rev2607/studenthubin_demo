using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason.EntityLayer
{
    public class InstructorEN
    {
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Education { get; set; }

        [DisplayName("Work Experience")]
        public string WorkExperience { get; set; }

        [DisplayName("Current Work Position")]
        public string CurrentWorkPosition { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }

        [DisplayName("Date Of Birth")]
        public string Dob { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Email2 { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }

        [DisplayName("Status")]
        public string StatusUser { get; set; }
        public string Featured { get; set; }
        public string Website { get; set; }

        [DisplayName("Profile Picture")]
        public string ProfilePic { get; set; }

        [DisplayName("Cover Picture")]
        public string CoverPic { get; set; }

        [DisplayName("Facebook Link")]
        public string FacebookLink { get; set; }

        [DisplayName("Twitter Link")]
        public string TwitterLink { get; set; }

        [DisplayName("Linkedin Link")]
        public string LinkedinLink { get; set; }
        public string VerificationCode { get; set; }

        public string ID { get; set; }
        public string UserId { get; set; }
    }
}