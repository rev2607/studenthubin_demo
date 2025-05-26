using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason.EntityLayer
{
    public class HomeEN
    {
        DataTable Banners { get; set; } = new DataTable();
        DataTable Courses { get; set; } = new DataTable();
        DataTable Testimonials { get; set; } = new DataTable();
        DataTable Placemets { get; set; } = new DataTable();

        string test { get; set; }
    }
}