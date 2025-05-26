using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using System.Linq;
using System.Web.Mvc;

namespace StudentHubMVC
{
    [MetadataType(typeof(MetaJobAlert))]
    public partial class JobAlerts
    { }

    public class MetaJobAlert
    {
        [AllowHtml]
        [DisplayName("Overview")]
        public string Details { get; set; }

        [AllowHtml]
        [DisplayName("Job Description")]
        public string JD { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public string Date { get; set; }

        [DisplayName("Website")]
        public string job_Website { get; set; }

        [DisplayName("Domain")]
        public string job_Domain { get; set; }

        [DisplayName("Designation")]
        public string job_Designation { get; set; }

        [AllowHtml]
        [DisplayName("Eligibility Criteria")]
        public string job_EligibilityCriteria { get; set; }

        [DisplayName("CTC/Annum")]
        public string job_CtcAnnum { get; set; }

        [DisplayName("Bond Period")]
        public string job_BondPeriod { get; set; }

        [AllowHtml]
        [DisplayName("Skills Required")]
        public string job_SkillsRequired { get; set; }

        [AllowHtml]
        [DisplayName("Selection Process")]
        public string job_SelectionProcess { get; set; }

        [AllowHtml]
        [DisplayName("Additional Information")]
        public string job_AdditionalInformation { get; set; }

        [AllowHtml]
        [DisplayName("Contact Info")]
        public string job_ContactInfo { get; set; }

        [DisplayName("Last Date To Apply")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> job_LastDateToApply { get; set; }
    }
}