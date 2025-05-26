using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentHubMVC.Models.ViewModels.Home;
using PagedList;
using PagedList.Mvc;

namespace StudentHubMVC.Controllers
{
    public class NewsController : Controller
    {
        Entities db = new Entities();


        // GET: News
        public ActionResult Index(VMHomeNewsSearch newsSearch, int? page)
        {
            VMHomeNewsSearch NS = new VMHomeNewsSearch();
            IEnumerable<NewsAlerts> newsAlerts = db.NewsAlerts.Where(n => n.ExpiryDate > DateTime.Now || n.ExpiryDate == null).ToList();

            if (string.IsNullOrEmpty(newsSearch.Keyword) == false)
                newsAlerts = newsAlerts.Where(n => n.Title.Contains(newsSearch.Keyword)).ToList();

            if (string.IsNullOrEmpty(newsSearch.Type) == false)
                newsAlerts = newsAlerts.Where(n => n.NewsTypeId.ToString() == newsSearch.Type).ToList();

            newsSearch.NewsAlerts = newsAlerts.OrderByDescending(i => i.NewsId).ToPagedList(page ?? 1, int.Parse(ConfigurationManager.AppSettings["Paging15"]));

            return View(newsSearch);
        }

        // GET: Details/news-id
        public ActionResult Details(int? id)
        {
            NewsAlerts news = new NewsAlerts();

            if (id != null)
            {
                if (db.NewsAlerts.Where(c => c.NewsId == id).Any())
                {
                    news = db.NewsAlerts.Where(c => c.NewsId == id).FirstOrDefault();
                }
            }

            return View(news);
        }
    }
}