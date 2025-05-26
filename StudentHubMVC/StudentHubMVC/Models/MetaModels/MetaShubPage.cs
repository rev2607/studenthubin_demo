using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentHubMVC
{
    //[MetadataType(typeof(MetaShubPage))]
    //public partial class shub_pages
    //{

    //}

    public class MetaShubPage
    {
        [StringLength(Int32.MaxValue)]
        public string page_content { get; set; }
    }
}