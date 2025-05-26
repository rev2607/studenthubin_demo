using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC
{

    [MetadataType(typeof(MetaNewsAlert))]
    public partial class NewsAlerts
    {
    }

    public class MetaNewsAlert
    {
        [DisplayName("News Type")]
        public long NewsTypeId { get; set; }
        [DisplayName("URL Keyword")]
        public string UrlKeyword { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [DisplayName("Cover Image")]
        public string CoverImage { get; set; }
        [DisplayName("Course")]
        public Nullable<long> CourseId { get; set; }
        [DisplayName("Institution")]
        public Nullable<long> InstitutionId { get; set; }
        [DisplayName("Expiry Date")]
        public Nullable<System.DateTime> ExpiryDate { get; set; }
    }
}