using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Liason.EntityLayer
{
    public class PageEN
    {
        public string Heading { get; set; }

        [AllowHtml]
        //[DataType(DataType.Text)]
        public string Content { get; set; }

        [DisplayName("Meta Title")]
        public string MetaTitle { get; set; }

        [DisplayName("Meta Description")]
        public string MetaDescription { get; set; }

        public string Sno { get; set; }
        public string UserId { get; set; }

    }
}