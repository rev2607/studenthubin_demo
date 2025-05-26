using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels.Home
{
    public class VMHomeIndex
    {
        public VMHomeIndex()
        {
            this.Colleges = new List<Colleges>();
            //this.Articles = new List<Articles>();
            this.News = new List<NewsAlerts>();
            this.Placements = new List<Placements>();
            this.Institutions = new List<Institutions>();
            this.Banners = new List<string>();
        }

        public List<Colleges> Colleges { get; set; }
        //public List<Articles> Articles { get; set; }
        public List<NewsAlerts> News { get; set; }
        public List<Placements> Placements { get; set; }
        public List<Institutions> Institutions { get; set; }

        public List<string> Banners { get; set; }
    }
}