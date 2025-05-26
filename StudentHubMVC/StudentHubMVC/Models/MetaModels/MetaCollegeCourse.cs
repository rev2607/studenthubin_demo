using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace StudentHubMVC
{
    [MetadataType(typeof(MetaCollegeCourse))]
    public partial class CollegeCourses
    {
    }

    public class MetaCollegeCourse
    {
        public long colcrs_Id { get; set; }
        [DisplayName("Course Name")]
        [Required]
        public string colcrs_Name { get; set; }
        [DisplayName("URL Keyword")]
        [Required]
        public string colcrs_UrlKeyword { get; set; }
        [DisplayName("Course Type")]
        public Nullable<long> colcrs_CourseType { get; set; }
        [DisplayName("Course Level")]
        public Nullable<long> colcrs_CourseLevel { get; set; }
        [DisplayName("Degree Type")]
        public Nullable<long> colcrs_DegreeType { get; set; }
        [DisplayName("Logo")]
        public string colcrs_Logo { get; set; }
        [DisplayName("Cover Pic")]
        public string colcrs_CoverPic { get; set; }
        [DisplayName("Short Description")]
        [AllowHtml]
        public string colcrs_ShortDescription { get; set; }
        [DisplayName("Description")]
        [AllowHtml]
        public string colcrs_Description { get; set; }
        [DisplayName("Trending")]
        public string colcrs_IsTrending { get; set; }
        [DisplayName("Eligibility Criteria")]
        [AllowHtml]
        public string colcrs_EligibilityCriteria { get; set; }
        [DisplayName("Duration")]
        public string colcrs_Duration { get; set; }
        [DisplayName("Exam Type")]
        public string colcrs_ExamType { get; set; }
        [DisplayName("Fee Description")]
        public string colcrs_FeeDesc { get; set; }
        [DisplayName("Fee Minimum")]
        public string colcrs_FeeMinimum { get; set; }
        [DisplayName("Fee Maximum")]
        public string colcrs_FeeMaximum { get; set; }
        [DisplayName("Admission Process")]
        [AllowHtml]
        public string colcrs_AdmissionProcess { get; set; }
        [DisplayName("Top Entrance Test")]
        public string colcrs_TopEntranceTest { get; set; }
        [DisplayName("Selection Process")]
        public string colcrs_SelectionProcess { get; set; }
        [DisplayName("Lateral Entry")]
        public string colcrs_LateralEntry { get; set; }
        [DisplayName("Academic Options")]
        public string colcrs_AcademicOptions { get; set; }
        [DisplayName("Salary Offered to Freshers")]
        public string colcrs_SalaryOfferedFreshes { get; set; }
        [DisplayName("Job Profile")]
        public string colcrs_JobProfile { get; set; }
        [DisplayName("Job Prospects")]
        public string colcrs_JobProspects { get; set; }
        [DisplayName("Top 5 Recruiters")]
        public string colcrs_Top5Recruiters { get; set; }
        [DisplayName("Status")]
        public string colcrs_Status { get; set; }
        [DisplayName("Featured")]
        public string colcrs_IsFeatured { get; set; }
        [DisplayName("Top")]
        public string colcrs_IsTop { get; set; }
    }
}