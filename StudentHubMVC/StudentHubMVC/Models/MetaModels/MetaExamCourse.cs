using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC
{
    [MetadataType(typeof(MetaExamCourse))]
    public partial class ExamCourses
    {
    }

    public class MetaExamCourse
    {
        [DisplayName("Course Name")]
        [Required]
        public string exmcrs_Name { get; set; }
        [DisplayName("Url Keywords")]
        [Required]
        public string exmcrs_UrlKeyword { get; set; }
        [DisplayName("Description")]
        [AllowHtml]
        public string exmcrs_Description { get; set; }
        [DisplayName("Exam Type")]
        [Required]
        public long exmcrs_ExamType { get; set; }
        [DisplayName("Exam Stream")]
        [Required]
        public long exmcrs_ExamStream { get; set; }
        [DisplayName("Exam Level")]
        [Required]
        public long exmcrs_ExamLevel { get; set; }
        [DisplayName("Exam Mode")]
        [Required]
        public long exmcrs_ExamMode { get; set; }
        [DisplayName("Registration")]
        [AllowHtml]
        public string exmcrs_Registration { get; set; }
        [DisplayName("Eligibility")]
        [AllowHtml]
        public string exmcrs_Eligibility { get; set; }
        [DisplayName("Pattern & Syllabus")]
        [AllowHtml]
        public string exmcrs_PatternSyllabus { get; set; }
        [DisplayName("Preparation Tips")]
        [AllowHtml]
        public string exmcrs_PreparationTips { get; set; }
        [DisplayName("Admit Card")]
        [AllowHtml]
        public string exmcrs_AdmitCard { get; set; }
        [DisplayName("Results")]
        [AllowHtml]
        public string exmcrs_Results { get; set; }
        [DisplayName("Important Dates")]
        [AllowHtml]
        public string exmcrs_ImportantDates { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DisplayName("Application Start Date")]
        public Nullable<System.DateTime> exmcrs_ApplicationStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DisplayName("Application End Date")]
        public Nullable<System.DateTime> exmcrs_ApplicationEndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DisplayName("Exam Date")]
        public Nullable<System.DateTime> exmcrs_ExamDate { get; set; }
        [DisplayName("Counselling")]
        [AllowHtml]
        public string exmcrs_Counselling { get; set; }
        [DisplayName("Related Articles")]
        [AllowHtml]
        public string exmcrs_RelatedArticles { get; set; }
        [DisplayName("Logo")]
        public string exmcrs_Logo { get; set; }
        [DisplayName("Status")]
        public string exmcrs_Status { get; set; }
    }
}