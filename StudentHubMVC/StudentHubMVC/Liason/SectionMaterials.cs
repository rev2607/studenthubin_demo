using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StudentHubMVC.Liason
{
    public class SectionMaterials
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();


        // ---------------------------------------------------------------------------- PROCESS DATA
        public string ProcessData_Material(string Mode, int SectionId, string fileid, HttpRequestBase Request)
        {
            if (Mode == "ADD")
                Mode = "AddSectionMaterial";

            else if (Mode == "DELETE")
                Mode = "DeleteSectionMaterial";

            // DeleteDocument --> delete file

            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            Liason.EntityLayer.MaterialEN MaterialEN = new EntityLayer.MaterialEN();
            MaterialEN = FormDataToEntity(SectionId, fileid, Request);

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@SectionId", MaterialEN.SectionId));
            parameters.Add(new KeyValuePair<string, string>("@FileName", MaterialEN.FileName));

            parameters.Add(new KeyValuePair<string, string>("@User", MaterialEN.UserId));
            parameters.Add(new KeyValuePair<string, string>("@Sno", MaterialEN.Sno));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", Mode));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("SectionMaterialData", parameters, outparameters);

            return Result[0].Value;
        }


        private Liason.EntityLayer.MaterialEN FormDataToEntity(int SectionId, string fileid, HttpRequestBase Request)
        {
            Liason.EntityLayer.MaterialEN MaterialEN = new Liason.EntityLayer.MaterialEN();

            MaterialEN.SectionId = SectionId.ToString();
            MaterialEN.FileName = "";

            HttpPostedFileBase Material = Request.Files["MaterialFilePosted"];
            if (Material != null && Material.FileName != "")
            {
                MaterialEN.FileName = SaveDocument(Material);
            }

            MaterialEN.UserId = "1";
            MaterialEN.Sno = fileid;

            return MaterialEN;
        }


        // ---------------------------------------------------------- UPLOADING MATERIALS
        private string SaveDocument(HttpPostedFileBase file)
        {
            string name = System.IO.Path.GetFileNameWithoutExtension(file.FileName); //getting file name without extension 
            string ext = System.IO.Path.GetExtension(file.FileName);
            string myfile = name + "_" + DateTime.Now.ToString("yyyymmdd") + ext; //appending the name with id
            string MaterialsPath = ConfigurationManager.AppSettings["MaterialsPath"];

            var path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath(MaterialsPath), myfile);

            file.SaveAs(path);

            return myfile;
        }


        // ---------------------------------------------------------- DELETE MATERIAL
        private void DeleteDocument(string FileName)
        {
            //string name = System.IO.Path.GetFileNameWithoutExtension(file.FileName); //getting file name without extension 
            //string ext = System.IO.Path.GetExtension(file.FileName);
            //string myfile = name + "_" + DateTime.Now.ToString("yyyymmdd") + ext; //appending the name with id
            //string MaterialsPath = ConfigurationManager.AppSettings["MaterialsPath"];

            //var path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath(MaterialsPath), myfile);

            //file.SaveAs(path);

            //return myfile;
        }
    }
}