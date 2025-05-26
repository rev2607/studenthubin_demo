using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason.EntityLayer
{
    public class StudentEN
    {
        [DisplayName("Full Name")]
        [Required]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "Min 4 characters & Max 25 characters long.")]
        public string FullName { get; set; }

        [Required (ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }

        public int Age { get; set; }
        public string Education { get; set; }

        [DisplayName("Reference")]
        public string WhoReferredUs { get; set; }

        public string WorkExperience { get; set; }
        public string CurrentWorkPosition { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Dob { get; set; }
        public string Pincode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Email2 { get; set; }

        [DisplayName("Mobile")]
        public string Phone1 { get; set; }

        [DisplayName("Land Line")]
        public string Phone2 { get; set; }
        public string Status { get; set; }

        [DisplayName("Profile Picture")]
        public string ProfilePic { get; set; }

        public string VerificationCode { get; set; }

        public string Sno { get; set; }
        public string UserId { get; set; }
    }
}