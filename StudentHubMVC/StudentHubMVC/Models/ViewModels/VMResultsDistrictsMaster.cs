using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentHubMVC;

namespace StudentHubMVC.Models.ViewModels
{
    public class VMResultsDistrictsMaster
    {
        public VMResultsDistrictsMaster()
        {
            ExamTypes = new List<rslt_CourseTypes>();
            DistrictsList = new List<rslt_DistrictsMaster>();
            District = new rslt_DistrictsMaster();
            District.dstrct_Sno = 0;
        }

        public List<rslt_CourseTypes> ExamTypes { get; set; }
        public List<rslt_DistrictsMaster> DistrictsList { get; set; }
        public rslt_DistrictsMaster District { get; set; }

    }

    public class District
    {
        
    }
}