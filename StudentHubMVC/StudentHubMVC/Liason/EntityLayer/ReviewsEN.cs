using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason.EntityLayer
{
    public class ReviewsEN
    {
        public string CourseId { get; set; }
        public string StudentId { get; set; }
        public string BatchId { get; set; }
        public string Rating { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }

        public string Sno { get; set; }
        public string UserId { get; set; }

    }
}