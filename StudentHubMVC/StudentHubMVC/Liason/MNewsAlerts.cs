using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason
{
    public class MNewsAlerts
    {
        Entities db = new Entities();

        // -------------------------------------------------- LATEST NEWS ALERTS
        public List<NewsAlerts> GetLatestNewsAlerts()
        {
            return db.NewsAlerts.Where(n => n.Status == Constants.Active).OrderByDescending(n => n.NewsId).Take(10).ToList();
        }
    }
}