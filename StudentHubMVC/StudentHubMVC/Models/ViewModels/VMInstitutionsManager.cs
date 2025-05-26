using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace StudentHubMVC.Models.ViewModels
{
    public class VMInstitutionsManager
    {
        public string SearchName { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Status { get; set; }

        public IPagedList<StudentHubMVC.Institutions> Institutions { get; set; }
    }
}