using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels
{
    public class VMResultsSubjectsMaster
    {
        public VMResultsSubjectsMaster()
        {
            ResultsExamTypesList = new List<rslt_CourseTypes>();
            ResultsSubjectsList = new List<rslt_SubjectsMaster>();
            ResultSubject = new rslt_SubjectsMaster();
            ResultSubject.sbjct_Sno = 0;
        }

        public List<rslt_CourseTypes> ResultsExamTypesList { get; set; }
        public List<rslt_SubjectsMaster> ResultsSubjectsList { get; set; }
        public rslt_SubjectsMaster ResultSubject { get; set; }
    }
}