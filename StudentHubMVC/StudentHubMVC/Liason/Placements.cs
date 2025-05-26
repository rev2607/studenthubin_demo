using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StudentHubMVC.Liason
{
    public class Placements
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();


        // ---------------------------------------------------------------------------- PROCESS DATA
        public string ProcessData_Placement(string Mode, string ID, FormCollection collection, HttpPostedFileBase file)
        {
            if (Mode == "ADD")
                Mode = "AddPlacement";

            else if (Mode == "UPDATE")
                Mode = "UpdatePlacement";

            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            Liason.EntityLayer.PlacementEN PlacementEN = new EntityLayer.PlacementEN();
            PlacementEN = FormDataToEntity(ID, collection, file);

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@StudentName", PlacementEN.StudentName));
            parameters.Add(new KeyValuePair<string, string>("@Gender", PlacementEN.Gender));
            parameters.Add(new KeyValuePair<string, string>("@CompanyName", PlacementEN.CompanyName));
            parameters.Add(new KeyValuePair<string, string>("@Photo", PlacementEN.Photo));
            parameters.Add(new KeyValuePair<string, string>("@JobPosition", PlacementEN.JobPosition));
            parameters.Add(new KeyValuePair<string, string>("@City", PlacementEN.City));
            parameters.Add(new KeyValuePair<string, string>("@CompanyLogo", PlacementEN.CompanyLogo));

            parameters.Add(new KeyValuePair<string, string>("@UserID", PlacementEN.UserId));
            parameters.Add(new KeyValuePair<string, string>("@Sno", PlacementEN.Sno));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", Mode));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("PlacementsData", parameters, outparameters);

            return Result[0].Value;
        }


        private Liason.EntityLayer.PlacementEN FormDataToEntity(string ID, FormCollection collection, HttpPostedFileBase file)
        {
            Liason.EntityLayer.PlacementEN PlacementEN = new Liason.EntityLayer.PlacementEN();

            PlacementEN.StudentName = collection["StudentName"];
            PlacementEN.Gender = collection["Gender"];
            PlacementEN.CompanyName = collection["CompanyName"];
            PlacementEN.Photo = collection["Photo"];
            PlacementEN.JobPosition = collection["JobPosition"];
            PlacementEN.City = collection["City"];
            PlacementEN.CompanyLogo = collection["CompanyLogo"];

            PlacementEN.UserId = "1";
            PlacementEN.Sno = ID;

            if (file != null && file.FileName != "")
            {
                string name = System.IO.Path.GetFileNameWithoutExtension(file.FileName); //getting file name without extension 
                string ext = System.IO.Path.GetExtension(file.FileName);
                string myfile = name + "_" + DateTime.Now.ToString("yyyymmdd") + ext; //appending the name with id

                var path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/assets/img/placements/"), myfile);

                file.SaveAs(path);

                PlacementEN.Photo = myfile;
            }

            else
            {
                if (collection["Photo"] != "" && collection["Photo"] != null)
                    PlacementEN.Photo = collection["Photo"];

                else
                {
                    if (collection["Gender"] == "MALE")
                        PlacementEN.Photo = "profileman.png";

                    else if (collection["Gender"] == "FEMALE")
                        PlacementEN.Photo = "profilewoman.png";
                }
            }

            return PlacementEN;
        }


        // ---------------------------------------------------------------------------- EDIT PLACEMENT
        public Liason.EntityLayer.PlacementEN EditPlacement(string PlacementID)
        {
            Liason.EntityLayer.PlacementEN obj_PlacementEN = new EntityLayer.PlacementEN();

            DataTable dt_EditPlacementData = new DataTable();

            dt_EditPlacementData = obj_dbconnect.procedure_exec_2_data_adapter("GetPlacementsData", "OperateMode", "EDITPLACEMENT", "KeyValue", PlacementID);

            if (dt_EditPlacementData.Rows.Count > 0)
            {
                obj_PlacementEN.StudentName = dt_EditPlacementData.Rows[0]["StudentName"].ToString();
                obj_PlacementEN.CompanyName = dt_EditPlacementData.Rows[0]["CompanyName"].ToString();
                obj_PlacementEN.Gender = dt_EditPlacementData.Rows[0]["Gender"].ToString();
                obj_PlacementEN.Photo = dt_EditPlacementData.Rows[0]["Photo"].ToString();
                obj_PlacementEN.JobPosition = dt_EditPlacementData.Rows[0]["JobPosition"].ToString();
                obj_PlacementEN.City = dt_EditPlacementData.Rows[0]["City"].ToString();
                obj_PlacementEN.CompanyLogo = dt_EditPlacementData.Rows[0]["CompanyLogo"].ToString();
            }

            return obj_PlacementEN;
        }


        // ---------------------------------------------------------------------------- GET ALL PLACEMENTS
        public DataTable GetAllPlacements()
        {
            DataTable dt_AllPlacements = new DataTable();

            dt_AllPlacements = obj_dbconnect.procedure_exec_2_data_adapter("GetPlacementsData", "OperateMode", "ALLPLACEMENTS", "KeyValue", "");

            return dt_AllPlacements;
        }
    }
}