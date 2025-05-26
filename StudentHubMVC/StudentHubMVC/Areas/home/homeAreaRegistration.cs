using System.Web.Mvc;

namespace StudentHubMVC.Areas.home
{
    public class homeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "home";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "home_default",
                "home/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}