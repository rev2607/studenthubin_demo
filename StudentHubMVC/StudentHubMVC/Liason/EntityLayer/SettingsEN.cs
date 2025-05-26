using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason.EntityLayer
{
    public class SettingsEN
    {
        // ---------------- SITE DETAILS
        public string Logo { get; set; }
        public string Website { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string FaceBookLink { get; set; }
        public string TwitterLink { get; set; }
        public string LinkedInLink { get; set; }
        public string YoutubeLink { get; set; }
        public string InstagramLink { get; set; }
        public string GooglePlusLink { get; set; }
        public string RssFeedLink { get; set; }
        public string BlogLink { get; set; }
        public string Address1Name { get; set; }
        public string Address1 { get; set; }
        public string Address2Name { get; set; }
        public string Address2 { get; set; }
        public string ContactUsEmail { get; set; }

        // ---------------- HOME BANNERS
        public string Banner1 { get; set; }
        public string Banner2 { get; set; }
        public string Banner3 { get; set; }
        public string Banner4 { get; set; }
        public string Banner5 { get; set; }

        // ---------------- HOME POP-UP BANNER
        public string PopUpBanner1 { get; set; }
        public string PopUpBanner2 { get; set; }


    }
}