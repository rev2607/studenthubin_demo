using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels
{
    public class VMResultsCoursesMaster
    {
        public VMResultsCoursesMaster()
        {
            ResultsCoursesMasterList = new List<rst_CoursesMaster>();
            ResultsExamTypesList = new List<rslt_CourseTypes>();
            ResultCourse = new rst_CoursesMaster();
            ResultCourse.corse_Sno = 0;
        }

        public List<rst_CoursesMaster> ResultsCoursesMasterList { get; set; }
        public List<rslt_CourseTypes> ResultsExamTypesList { get; set; }
        public rst_CoursesMaster ResultCourse { get; set; }
    }
}