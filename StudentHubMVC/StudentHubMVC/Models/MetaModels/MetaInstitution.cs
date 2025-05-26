using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC
{
    [MetadataType(typeof(MetaInstitution))]
    public partial class Institutions
    {
    }

    public class MetaInstitution
    {
        [AllowHtml]
        public string About { get; set; }

        [DisplayName("Is Top?")]
        public string IsTop { get; set; }

        [DisplayName("Is Featured?")]
        public string IsFeatured { get; set; }

        [DisplayName("Is Trending")]
        public string IsTrending { get; set; }

        [DisplayName("Map location")]
        public string MapLocation { get; set; }

        [DisplayName("100% Job Assistance")]
        public string JobAssistance { get; set; }

        [DisplayName("100% Training Satisfaction")]
        public string TrainingSatisfaction { get; set; }

        [DisplayName("100% Job Guarantee")]
        public string JobGurarantee { get; set; }

        [DisplayName("Verified Best Institution")]
        public string IsVerified { get; set; }
    }
}