using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Liason
{
    public class Enquiries
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();


        // ---------------------------------------------------------------------------- PROCESS DATA
        public string ProcessData_Enquiry(string name, string email, string phonenumber, string subject, string message)
        {
            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            Liason.EntityLayer.EnquiryEN EnquiryEN = new EntityLayer.EnquiryEN();

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@Name", name));
            parameters.Add(new KeyValuePair<string, string>("@Email", email));
            parameters.Add(new KeyValuePair<string, string>("@Phone", phonenumber));
            parameters.Add(new KeyValuePair<string, string>("@Subject", subject));
            parameters.Add(new KeyValuePair<string, string>("@Message", message));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", "AddEnquiry"));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("EnquiryData", parameters, outparameters);

            return Result[0].Value;
        }


        // ---------------------------------------------------------------------------- GET ALL ENQUIRIES
        public DataTable GetAllEnquiries()
        {
            DataTable dt_AllEnquiries = new DataTable();

            dt_AllEnquiries = obj_dbconnect.procedure_exec_2_data_adapter("GetEnquiriesData", "OperateMode", "AllEnquiries", "KeyValue", "");

            return dt_AllEnquiries;
        }
    }
}