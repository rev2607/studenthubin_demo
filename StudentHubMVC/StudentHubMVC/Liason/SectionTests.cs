using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StudentHubMVC.Liason
{
    public class SectionTests
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();


        // ---------------------------------------------------------------------------- PROCESS DATA
        public string ProcessData_Test(string Mode, int SectionId, string TestId, FormCollection collection)
        {
            if (Mode == "ADD")
                Mode = "AddSectionTest";

            else if (Mode == "UPDATE")
                Mode = "UpdateSectionTest";

            else if (Mode == "DELETE")
                Mode = "DeleteSectionTest";

            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            Liason.EntityLayer.SectionTestEN SectionTestEN = new EntityLayer.SectionTestEN();
            SectionTestEN = FormDataToEntity(SectionId, TestId, collection);

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@SectionId", SectionTestEN.SectionId));
            parameters.Add(new KeyValuePair<string, string>("@Difficulty", SectionTestEN.Difficulty));

            parameters.Add(new KeyValuePair<string, string>("@User", SectionTestEN.UserId));
            parameters.Add(new KeyValuePair<string, string>("@Sno", SectionTestEN.Sno));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", Mode));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("SectionTestsData", parameters, outparameters);

            return Result[0].Value;
        }


        public string DeleteTest(int SectionId, string TestId)
        {
            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@SectionId", SectionId.ToString()));
            parameters.Add(new KeyValuePair<string, string>("@Difficulty", ""));

            parameters.Add(new KeyValuePair<string, string>("@User", ""));
            parameters.Add(new KeyValuePair<string, string>("@Sno", TestId));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", "DeleteSectionTest"));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("SectionTestsData", parameters, outparameters);

            return Result[0].Value;
        }


        private Liason.EntityLayer.SectionTestEN FormDataToEntity(int SectionId, string TestId, FormCollection collection)
        {
            Liason.EntityLayer.SectionTestEN SectionTestEN = new Liason.EntityLayer.SectionTestEN();

            SectionTestEN.SectionId = SectionId.ToString();
            SectionTestEN.Difficulty = collection["Difficulty"];

            SectionTestEN.UserId = "1";
            SectionTestEN.Sno = TestId;

            return SectionTestEN;
        }
    }
}