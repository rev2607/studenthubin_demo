using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Liason
{
    public class Vouchers
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();


        // ---------------------------------------------------------------------------- PROCESS DATA
        public string ProcessData_Voucher(string Mode, FormCollection collection)
        {
            if (Mode == "ADD")
                Mode = "AddVoucher";

            else if (Mode == "UPDATE")
                Mode = "UpdateVoucher";

            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            Liason.EntityLayer.VoucherEN VoucherEN = FormDataToEntity(collection);

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@Name", VoucherEN.Name));
            parameters.Add(new KeyValuePair<string, string>("@Code", VoucherEN.Code));
            parameters.Add(new KeyValuePair<string, string>("@StartDate", VoucherEN.StartDate));
            parameters.Add(new KeyValuePair<string, string>("@EndDate", VoucherEN.EndDate));
            parameters.Add(new KeyValuePair<string, string>("@Notes", VoucherEN.Notes));
            parameters.Add(new KeyValuePair<string, string>("@Image", VoucherEN.Image));
            parameters.Add(new KeyValuePair<string, string>("@Status", VoucherEN.Status));
            parameters.Add(new KeyValuePair<string, string>("@Sno", VoucherEN.Sno));
            parameters.Add(new KeyValuePair<string, string>("@User", VoucherEN.UserId));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", Mode));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("VouchersData", parameters, outparameters);

            return Result[0].Value;
        }


        private Liason.EntityLayer.VoucherEN FormDataToEntity(FormCollection collection)
        {
            Liason.EntityLayer.VoucherEN VoucherEN = new EntityLayer.VoucherEN();

            VoucherEN.Name = collection["Name"];
            VoucherEN.Code = collection["Code"];
            VoucherEN.StartDate = collection["StartDate"];
            VoucherEN.EndDate = collection["EndDate"];
            VoucherEN.Notes = collection["Notes"];
            VoucherEN.Image = "";
            VoucherEN.Status = collection["Status"];
            VoucherEN.Sno = collection["Sno"];
            VoucherEN.UserId = "1"; // System.Web.HttpContext.Current.Session["ID"].ToString();

            return VoucherEN;
        }


        // ---------------------------------------------------------------------------- EDIT INSTRUCTOR
        public Liason.EntityLayer.VoucherEN EditVoucher(string VoucherID)
        {
            Liason.EntityLayer.VoucherEN obj_VoucherEN = new EntityLayer.VoucherEN();

            DataTable dt_EditVoucherData = new DataTable();

            dt_EditVoucherData = obj_dbconnect.procedure_exec_2_data_adapter("GetVouchersData", "OperateMode", "EDITVOUCHER", "KeyValue", VoucherID);

            if (dt_EditVoucherData.Rows.Count > 0)
            {
                obj_VoucherEN.Name = dt_EditVoucherData.Rows[0]["Name"].ToString();
                obj_VoucherEN.Code = dt_EditVoucherData.Rows[0]["Code"].ToString();
                obj_VoucherEN.StartDate = dt_EditVoucherData.Rows[0]["StartDate"].ToString();
                obj_VoucherEN.EndDate = dt_EditVoucherData.Rows[0]["EndDate"].ToString();
                obj_VoucherEN.Notes = dt_EditVoucherData.Rows[0]["Notes"].ToString();
                obj_VoucherEN.Image = dt_EditVoucherData.Rows[0]["Image"].ToString();
                obj_VoucherEN.Status = dt_EditVoucherData.Rows[0]["Status"].ToString();
                obj_VoucherEN.Sno = dt_EditVoucherData.Rows[0]["ID"].ToString();
            }

            return obj_VoucherEN;
        }


        // ---------------------------------------------------------------------------- GET ALL ENQUIRIES
        public DataTable GetAllVouchers()
        {
            DataTable dt_AllVouchers = new DataTable();

            dt_AllVouchers = obj_dbconnect.procedure_exec_2_data_adapter("GetVouchersData", "OperateMode", "ALLVOUCHERS", "KeyValue", "");

            return dt_AllVouchers;
        }
    }
}