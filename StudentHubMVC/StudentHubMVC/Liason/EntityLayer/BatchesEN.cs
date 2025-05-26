using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason.EntityLayer
{
    public class BatchesEN
    {
        [DisplayName("Course")]
        public string CourseId { get; set; }

        [DisplayName("Instructor 1")]
        public string InstructorId1 { get; set; }

        [DisplayName("Instructor 2")]
        public string InstructorId2 { get; set; }

        [DisplayName("Start Date")]
        public string StartDate { get; set; }
        public string Timing { get; set; }

        [DisplayName("Class Duration")]
        public string ClassDuration { get; set; }

        [DisplayName("Class Mode")]
        public string ClassMode { get; set; }

        [DisplayName("Max Strength")]
        public string MaxStrength { get; set; }

        [DisplayName("Discount (%)")]
        public string Discount { get; set; }

        [DisplayName("Video Link")]
        public string VideoLink { get; set; }

        [DisplayName("Payment Link")]
        public string PaymentLink { get; set; }

        [DisplayName("Online Classroom Link")]
        public string VirtualClassRoomLink { get; set; }

        [DisplayName("Course Duration")]
        public string CourseDuration { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

        public string ID { get; set; }
        public string UserID { get; set; }
    }
}