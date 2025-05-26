using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Liason
{
    public class Admin
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();
        readonly Entities db = new Entities();


        // ---------------------------------------------------------------------------- ADMIN LOGIN
        /// <summary>
        /// For admin Login (Give Email, Password)
        /// Returns : FAILED - LOGGEDIN - INCORRECT PASSWORD - USER DO NOT EXIST
        /// </summary>
        /// <returns>FAILED - LOGGEDIN - INCORRECT PASSWORD - USER DO NOT EXIST</returns>
        public string AdminLogin(FormCollection collection)
        {
            string Response = "";

            if (collection.AllKeys.Contains("adminemail"))
            {
                if (collection["adminemail"].ToString() != "")
                {
                    string email = collection["adminemail"].ToString();
                    string pass = collection["adminpassword"].ToString();

                    DataTable user = obj_dbconnect.GetData("select * from shub_users where user_Email = '" + email + "' and user_Password = '" + pass + "';");

                    if (user != null && user.Rows.Count == 1)
                    {
                        Response = "SUCCESS";
                        //if(user == pass)
                        //{
                        //    Response = "SUCCESS";
                        //}
                        //else
                        //{
                        //    Response = "INCORRECT PASSWORD";
                        //}
                    }
                    else
                    {
                        Response = "INCORRECT EMAIL OR PASSWORD / USER DO NOT EXIST";
                    }
                    //string[] outparameters = new string[1];
                    //outparameters[0] = "@Result";

                    //var parameters = new List<KeyValuePair<string, string>>();
                    //parameters.Add(new KeyValuePair<string, string>("@OperateMode", "LoginAdmin"));
                    //parameters.Add(new KeyValuePair<string, string>("@Email", collection["adminemail"]));
                    //parameters.Add(new KeyValuePair<string, string>("@Password", collection["adminpassword"]));
                    //parameters.Add(new KeyValuePair<string, string>("@UserID", ""));
                    //parameters.Add(new KeyValuePair<string, string>("@VerificationCode", ""));

                    //var Result = new List<KeyValuePair<string, string>>();

                    //Result = obj_dbconnect.ProcedureDynamic("AdminLogin", parameters, outparameters);
                    //Response = Result[0].Value;
                }
            }

            // if login successful get the name and userid of the logged in user

            return Response;
        }


        // ---------------------------------------------------------------------------- GET USER LOGIN
        public DataTable GetLoginDetails(string Email)
        {
            string OperateMode = "ADMINLOGINDATA";

            DataTable dt_LoginDetails = new DataTable();

            dt_LoginDetails = obj_dbconnect.GetData("SELECT user_Id AS ID, user_Email AS Email, user_FullName AS FullName FROM shub_users WHERE user_Email = '" + Email + "'");

            return dt_LoginDetails;
        }
    }
}