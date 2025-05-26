using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Liason
{
    public class Settings
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();

        
        // ----------------------------------------------------- GET SETTINGS DATA
        public Liason.EntityLayer.SettingsEN GetSettingsData()
        {
            Liason.EntityLayer.SettingsEN Setting = new EntityLayer.SettingsEN();

            DataTable dt_Settings = new DataTable();

            dt_Settings = obj_dbconnect.GetData("CALL SettingsData('AllSettings', '', '', '');");

            if(dt_Settings.Rows.Count > 0)
            {
                Setting.Logo = (dt_Settings.Select("[SettingName] = 'Logo'"))[0]["SettingValue"].ToString();
                Setting.Website = (dt_Settings.Select("[SettingName] = 'Website'"))[0]["SettingValue"].ToString(); 
                Setting.Phone1 = (dt_Settings.Select("[SettingName] = 'Phone1'"))[0]["SettingValue"].ToString();
                Setting.Phone2 = (dt_Settings.Select("[SettingName] = 'Phone2'"))[0]["SettingValue"].ToString();
                Setting.Email1 = (dt_Settings.Select("[SettingName] = 'Email1'"))[0]["SettingValue"].ToString();
                Setting.Email2 = (dt_Settings.Select("[SettingName] = 'Email2'"))[0]["SettingValue"].ToString();
                Setting.FaceBookLink = (dt_Settings.Select("[SettingName] = 'FaceBookLink'"))[0]["SettingValue"].ToString(); 
                Setting.TwitterLink = (dt_Settings.Select("[SettingName] = 'TwitterLink'"))[0]["SettingValue"].ToString(); 
                Setting.LinkedInLink = (dt_Settings.Select("[SettingName] = 'LinkedInLink'"))[0]["SettingValue"].ToString();
                Setting.YoutubeLink = (dt_Settings.Select("[SettingName] = 'YoutubeLink'"))[0]["SettingValue"].ToString();
                Setting.InstagramLink = (dt_Settings.Select("[SettingName] = 'InstagramLink'"))[0]["SettingValue"].ToString();
                Setting.GooglePlusLink = (dt_Settings.Select("[SettingName] = 'GooglePlusLink'"))[0]["SettingValue"].ToString();
                Setting.RssFeedLink = (dt_Settings.Select("[SettingName] = 'RssFeedLink'"))[0]["SettingValue"].ToString();
                Setting.BlogLink = (dt_Settings.Select("[SettingName] = 'BlogLink'"))[0]["SettingValue"].ToString();
                Setting.Address1Name = (dt_Settings.Select("[SettingName] = 'Address1Name'"))[0]["SettingValue"].ToString();
                Setting.Address1 = (dt_Settings.Select("[SettingName] = 'Address1'"))[0]["SettingValue"].ToString();
                Setting.Address2Name = (dt_Settings.Select("[SettingName] = 'Address2Name'"))[0]["SettingValue"].ToString();
                Setting.Address2 = (dt_Settings.Select("[SettingName] = 'Address2'"))[0]["SettingValue"].ToString();
                Setting.ContactUsEmail = (dt_Settings.Select("[SettingName] = 'ContactUsEmail'"))[0]["SettingValue"].ToString();

                // ---------------- HOME BANNERS
                Setting.Banner1 = (dt_Settings.Select("[SettingName] = 'Banner1'"))[0]["SettingValue"].ToString();
                Setting.Banner2 = (dt_Settings.Select("[SettingName] = 'Banner2'"))[0]["SettingValue"].ToString();
                Setting.Banner3 = (dt_Settings.Select("[SettingName] = 'Banner3'"))[0]["SettingValue"].ToString();
                Setting.Banner4 = (dt_Settings.Select("[SettingName] = 'Banner4'"))[0]["SettingValue"].ToString();
                Setting.Banner5 = (dt_Settings.Select("[SettingName] = 'Banner5'"))[0]["SettingValue"].ToString();

                // ---------------- HOME POP-UP BANNER
                Setting.PopUpBanner1 = (dt_Settings.Select("[SettingName] = 'PopUpBanner1'"))[0]["SettingValue"].ToString();
                Setting.PopUpBanner2 = (dt_Settings.Select("[SettingName] = 'PopUpBanner2'"))[0]["SettingValue"].ToString();
            }

            return Setting;
        }


        // ----------------------------------------------------- SAVE SETTINGS DATA
        public string SaveSettingsData(FormCollection collection, HttpRequestBase Request)
        {
            string Response = "";

            // MAIN SETTINGS
            HttpPostedFileBase Logo = Request.Files["LogoUpload"];
            if (Logo != null && Logo.FileName != "")
            {
                SaveSetting("Logo", SaveImage(Logo));
            }

            if(collection["Website"] != collection["WebsiteOld"])
            {
                SaveSetting("Website", collection["Website"]);
            }

            if (collection["Phone1"] != collection["Phone1Old"])
            {
                SaveSetting("Phone1", collection["Phone1"]);
            }

            if (collection["Phone2"] != collection["Phone2Old"])
            {
                SaveSetting("Phone2", collection["Phone2"]);
            }

            if (collection["Email1"] != collection["Email1Old"])
            {
                SaveSetting("Email1", collection["Email1"]);
            }

            if (collection["Email2"] != collection["Email2Old"])
            {
                SaveSetting("Email2", collection["Email2"]);
            }

            if (collection["FaceBookLink"] != collection["FaceBookLinkOld"])
            {
                SaveSetting("FaceBookLink", collection["FaceBookLink"]);
            }

            if (collection["TwitterLink"] != collection["TwitterLinkOld"])
            {
                SaveSetting("TwitterLink", collection["TwitterLink"]);
            }

            if (collection["LinkedInLink"] != collection["LinkedInLinkOld"])
            {
                SaveSetting("LinkedInLink", collection["LinkedInLink"]);
            }

            if (collection["YoutubeLink"] != collection["YoutubeLinkOld"])
            {
                SaveSetting("YoutubeLink", collection["YoutubeLink"]);
            }

            if (collection["InstagramLink"] != collection["InstagramLinkOld"])
            {
                SaveSetting("InstagramLink", collection["InstagramLink"]);
            }

            if (collection["GooglePlusLink"] != collection["GooglePlusLinkOld"])
            {
                SaveSetting("GooglePlusLink", collection["GooglePlusLink"]);
            }

            if (collection["RssFeedLink"] != collection["RssFeedLinkOld"])
            {
                SaveSetting("RssFeedLink", collection["RssFeedLink"]);
            }

            if (collection["BlogLink"] != collection["BlogLinkOld"])
            {
                SaveSetting("BlogLink", collection["BlogLink"]);
            }

            if (collection["Address1Name"] != collection["Address1NameOld"])
            {
                SaveSetting("Address1Name", collection["Address1Name"]);
            }

            if (collection["Address1"] != collection["Address1Old"])
            {
                SaveSetting("Address1", collection["Address1"]);
            }

            if (collection["Address2Name"] != collection["Address2NameOld"])
            {
                SaveSetting("Address2Name", collection["Address2Name"]);
            }

            if (collection["Address2"] != collection["Address2Old"])
            {
                SaveSetting("Address2", collection["Address2"]);
            }

            if (collection["ContactUsEmail"] != collection["ContactUsEmailOld"])
            {
                SaveSetting("ContactUsEmail", collection["ContactUsEmail"]);
            }


            // HOME BANNERS

            HttpPostedFileBase HomeBanner1 = Request.Files["HomeBanner1"];
            if(HomeBanner1 != null && HomeBanner1.FileName != "")
            {
                SaveSetting("Banner1", SaveImage(HomeBanner1));
            }
            HttpPostedFileBase HomeBanner2 = Request.Files["HomeBanner2"];
            if (HomeBanner2 != null && HomeBanner2.FileName != "")
            {
                SaveSetting("Banner2", SaveImage(HomeBanner2));
            }
            HttpPostedFileBase HomeBanner3 = Request.Files["HomeBanner3"];
            if (HomeBanner3 != null && HomeBanner3.FileName != "")
            {
                SaveSetting("Banner3", SaveImage(HomeBanner3));
            }
            HttpPostedFileBase HomeBanner4 = Request.Files["HomeBanner4"];
            if (HomeBanner4 != null && HomeBanner4.FileName != "")
            {
                SaveSetting("Banner4", SaveImage(HomeBanner4));
            }
            HttpPostedFileBase HomeBanner5 = Request.Files["HomeBanner5"];
            if (HomeBanner5 != null && HomeBanner5.FileName != "")
            {
                SaveSetting("Banner5", SaveImage(HomeBanner5));
            }

            
            // HOME POP-UP BANNERS

            HttpPostedFileBase HomePopUpBanner1 = Request.Files["HomePopUpBanner1"];
            if (HomePopUpBanner1 != null && HomePopUpBanner1.FileName != "")
            {
                SaveSetting("PopUpBanner1", SaveImage(HomePopUpBanner1));
            }
            HttpPostedFileBase HomePopUpBanner2 = Request.Files["HomePopUpBanner2"];
            if (HomePopUpBanner2 != null && HomePopUpBanner2.FileName != "")
            {
                SaveSetting("PopUpBanner2", SaveImage(HomePopUpBanner2));
            }

            return Response;
        }


        // ---------------------------------------------------------- SAVE SETTING
        private string SaveSetting(string SettingName, string SettingValue)
        {
            string[] outparameters = new string[0];
            //outparameters[0] = "@Result";
            
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@SettingName", SettingName));
            parameters.Add(new KeyValuePair<string, string>("@SettingValue", SettingValue));

            parameters.Add(new KeyValuePair<string, string>("@UserID", "1"));
            parameters.Add(new KeyValuePair<string, string>("@OperateMode", "UpdateSetting"));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("SettingsData", parameters, outparameters);

            return "";
        }


        // ---------------------------------------------------------- UPLOADING THE FILE
        private string SaveImage(HttpPostedFileBase file)
        {
            string name = System.IO.Path.GetFileNameWithoutExtension(file.FileName); //getting file name without extension 
            string ext = System.IO.Path.GetExtension(file.FileName);
            string myfile = name + "_" + DateTime.Now.ToString("yyyymmdd") + ext; //appending the name with id

            var path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/assets/img/banners/"), myfile);

            file.SaveAs(path);

            return myfile;
        }
    }
}