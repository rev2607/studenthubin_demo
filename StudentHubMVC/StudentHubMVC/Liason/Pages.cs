using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason
{
    public class Pages
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();


        // ---------------------------------------------------------------------------- PROCESS DATA
        public string ProcessData_Page(string PageName, System.Web.Mvc.FormCollection collection)
        {
            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            Liason.EntityLayer.PageEN PageEN = new EntityLayer.PageEN();

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@Heading", collection["Heading"]));
            parameters.Add(new KeyValuePair<string, string>("@Name", PageName));
            parameters.Add(new KeyValuePair<string, string>("@Content", collection["Content"]));
            parameters.Add(new KeyValuePair<string, string>("@MetaTitle", collection["MetaTitle"]));
            parameters.Add(new KeyValuePair<string, string>("@MetaDescription", collection["MetaDescription"]));

            parameters.Add(new KeyValuePair<string, string>("@UserID", "1"));
            parameters.Add(new KeyValuePair<string, string>("@Sno", collection["Sno"]));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", "UpdatePage"));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("PagesData", parameters, outparameters);

            return Result[0].Value;
        }



        // ---------------------------------------------------------------------------- EDIT PAGE
        public Liason.EntityLayer.PageEN GetPageData(string PageID)
        {
            Liason.EntityLayer.PageEN obj_PageEN = new EntityLayer.PageEN();

            DataTable dt_EditCourseData = new DataTable();

            dt_EditCourseData = obj_dbconnect.procedure_exec_2_data_adapter("GetFrontData", "OperateMode", "PAGES", "KeyValue", PageID);

            if (dt_EditCourseData.Rows.Count > 0)
            {
                obj_PageEN.Sno = dt_EditCourseData.Rows[0]["ID"].ToString();
                obj_PageEN.Heading = dt_EditCourseData.Rows[0]["Heading"].ToString();
                obj_PageEN.Content = dt_EditCourseData.Rows[0]["Content"].ToString();
                obj_PageEN.MetaTitle = dt_EditCourseData.Rows[0]["MetaTitle"].ToString();
                obj_PageEN.MetaDescription = dt_EditCourseData.Rows[0]["MetaDescription"].ToString();
            }

            return obj_PageEN;
        }

    }
}