using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason.EntityLayer
{
    public class MaterialEN
    {
        [DisplayName("Course Section")]
        public string SectionId { get; set; }

        [DisplayName("Material File")]
        public string FileName { get; set; }

        public string Sno { get; set; }
        public string UserId { get; set; }
    }
}