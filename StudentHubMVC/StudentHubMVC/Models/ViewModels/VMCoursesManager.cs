using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace StudentHubMVC.Models.ViewModels
{
    public class VMCoursesManager
    {
        public string SearchName { get; set; } = "";
        public string Stream { get; set; }
        public string Status { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsTrending { get; set; }
        public bool IsInTopList { get; set; }

        public IPagedList<StudentHubMVC.MainCourses> Courses { get; set; }
    }
}