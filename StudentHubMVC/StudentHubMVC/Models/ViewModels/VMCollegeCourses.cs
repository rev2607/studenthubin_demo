using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels
{
    public class VMCollegeCourses
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UrlKeyword { get; set; }
        public Nullable<long> CourseType { get; set; }
        public Nullable<long> CourseLevel { get; set; }
        public Nullable<long> DegreeType { get; set; }
        public string Logo { get; set; }
        public string CoverPic { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string IsTrending { get; set; }
        public string EligibilityCriteria { get; set; }
        public string Duration { get; set; }
        public string ExamType { get; set; }
        public string FeeDesc { get; set; }
        public string FeeMinimum { get; set; }
        public string FeeMaximum { get; set; }
        public string AdmissionProcess { get; set; }
        public string TopEntranceTest { get; set; }
        public string SelectionProcess { get; set; }
        public string LateralEntry { get; set; }
        public string AcademicOptions { get; set; }
        public string SalaryOfferedFreshes { get; set; }
        public string JobProfile { get; set; }
        public string JobProspects { get; set; }
        public string Top5Recruiters { get; set; }
    }
}