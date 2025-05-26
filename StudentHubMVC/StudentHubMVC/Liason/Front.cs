using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason
{
    public class Front
    {
        Entities db = new Entities();

        MColleges obj_colleges = new MColleges();
        MNewsAlerts obj_newsAlerts = new MNewsAlerts();

        // ----------------------------------------------------------- TOP COLLEGES
        public List<Colleges> GetFeaturedColleges()
        {
            return obj_colleges.GetFeaturedColleges();
        }

        // ----------------------------------------------------------- LATEST NEWS ALERTS
        public List<NewsAlerts> GetLatestNewsAlerts()
        {
            return obj_newsAlerts.GetLatestNewsAlerts();
        }

    }
}