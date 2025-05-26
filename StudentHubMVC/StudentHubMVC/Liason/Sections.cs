using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StudentHubMVC.Liason
{
    public class Sections
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();


        // ---------------------------------------------------------------------------- PROCESS DATA
        public string ProcessData_Section(string Mode, string CourseID, string ID, FormCollection collection)
        {
            if (Mode == "ADD")
                Mode = "AddSection";

            else if (Mode == "UPDATE")
                Mode = "UpdateSection";

            else if (Mode == "DELETE")
                Mode = "DeleteSection";

            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            Liason.EntityLayer.SectionEN SectionEN = new EntityLayer.SectionEN();
            SectionEN = FormDataToEntity(CourseID, ID, collection);

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@CourseId", SectionEN.CourseId));
            parameters.Add(new KeyValuePair<string, string>("@Title", SectionEN.Title));
            parameters.Add(new KeyValuePair<string, string>("@Description", SectionEN.Description));
            parameters.Add(new KeyValuePair<string, string>("@SortId", SectionEN.SortId));

            parameters.Add(new KeyValuePair<string, string>("@User", SectionEN.UserId));
            parameters.Add(new KeyValuePair<string, string>("@Sno", SectionEN.Sno));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", Mode));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("SectionsData", parameters, outparameters);

            return Result[0].Value;
        }


        private Liason.EntityLayer.SectionEN FormDataToEntity(string CourseID, string ID, FormCollection collection)
        {
            Liason.EntityLayer.SectionEN SectionEN = new Liason.EntityLayer.SectionEN();

            SectionEN.CourseId = CourseID;
            SectionEN.Title = collection["Title"];
            SectionEN.Description = collection["Description"];
            SectionEN.SortId = collection["SortId"];

            SectionEN.UserId = "1";
            SectionEN.Sno = ID;

            return SectionEN;
        }


        // ---------------------------------------------------------------------------- GET SECTIONS DATA
        public DataSet GetAllSections(int CourseId)
        {
            DataSet ds = new DataSet();

            ds = obj_dbconnect.procedure_exec_2_data_adapter_dataset("GetSectionData", "OperateMode", "SECTIONSLIST", "KeyValue", CourseId.ToString());

            return ds;
        }


        public DataSet GetSectionDetails(int SectionId)
        {
            DataSet ds = new DataSet();

            ds = obj_dbconnect.procedure_exec_2_data_adapter_dataset("GetSectionData", "OperateMode", "SECTIONDETAILS", "KeyValue", SectionId.ToString());

            return ds;
        }

    }
}