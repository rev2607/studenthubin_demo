using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Liason
{
    public class Batches
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();


        // ---------------------------------------------------------------------------- PROCESS DATA
        public string ProcessData_Batch(string Mode, string ID, FormCollection collection)
        {
            if (Mode == "ADD")
                Mode = "AddBatch";

            else if (Mode == "UPDATE")
                Mode = "UpdateBatch";

            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            Liason.EntityLayer.BatchesEN BatchEN = new EntityLayer.BatchesEN();
            BatchEN = FormDataToEntity(ID, collection);

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@CourseId", BatchEN.CourseId));
            parameters.Add(new KeyValuePair<string, string>("@InstructorId1", BatchEN.InstructorId1));
            parameters.Add(new KeyValuePair<string, string>("@InstructorId2", BatchEN.InstructorId2));
            parameters.Add(new KeyValuePair<string, string>("@StartDate", BatchEN.StartDate));
            parameters.Add(new KeyValuePair<string, string>("@Timing", BatchEN.Timing));
            parameters.Add(new KeyValuePair<string, string>("@ClassDuration", BatchEN.ClassDuration));
            parameters.Add(new KeyValuePair<string, string>("@ClassMode", BatchEN.ClassMode));
            parameters.Add(new KeyValuePair<string, string>("@MaxStrength", BatchEN.MaxStrength));
            parameters.Add(new KeyValuePair<string, string>("@Discount", BatchEN.Discount));
            parameters.Add(new KeyValuePair<string, string>("@VideoLink", BatchEN.VideoLink));
            parameters.Add(new KeyValuePair<string, string>("@PayementLink", BatchEN.PaymentLink));
            parameters.Add(new KeyValuePair<string, string>("@VirtualClassRoomLink", BatchEN.VirtualClassRoomLink));
            parameters.Add(new KeyValuePair<string, string>("@CourseDuration", BatchEN.CourseDuration));
            parameters.Add(new KeyValuePair<string, string>("@Notes", BatchEN.Notes));
            parameters.Add(new KeyValuePair<string, string>("@Status", BatchEN.Status));

            parameters.Add(new KeyValuePair<string, string>("@UserId", BatchEN.UserID));
            parameters.Add(new KeyValuePair<string, string>("@Sno", BatchEN.ID));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", Mode));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("BatchesData", parameters, outparameters);

            return Result[0].Value;
        }


        private Liason.EntityLayer.BatchesEN FormDataToEntity(string ID, FormCollection collection)
        {
            Liason.EntityLayer.BatchesEN BatchEN = new Liason.EntityLayer.BatchesEN();

            BatchEN.CourseId = collection["CourseId"];
            BatchEN.InstructorId1 = collection["InstructorId1"];
            BatchEN.InstructorId2 = collection["InstructorId2"];
            BatchEN.StartDate = collection["StartDate"];
            BatchEN.Timing = collection["Timing"];
            BatchEN.ClassDuration = collection["ClassDuration"];
            BatchEN.ClassMode = collection["ClassMode"];
            BatchEN.MaxStrength = collection["MaxStrength"];
            BatchEN.Discount = collection["Discount"];
            BatchEN.VideoLink = collection["VideoLink"];
            BatchEN.PaymentLink = collection["PayementLink"];
            BatchEN.VirtualClassRoomLink = collection["VirtualClassRoomLink"];
            BatchEN.CourseDuration = collection["CourseDuration"];
            BatchEN.Notes = collection["Notes"];
            BatchEN.Status = collection["Status"];

            BatchEN.UserID = "1";
            BatchEN.ID = ID;

            return BatchEN;
        }


        // ---------------------------------------------------------------------------- EDIT BATCH
        public Liason.EntityLayer.BatchesEN EditBatch(string BatchID)
        {
            Liason.EntityLayer.BatchesEN obj_BatchEN = new EntityLayer.BatchesEN();

            DataTable dt_EditBatchData = new DataTable();

            dt_EditBatchData = obj_dbconnect.procedure_exec_2_data_adapter("GetBatchesData", "OperateMode", "EDITBATCH", "KeyValue", BatchID);

            if (dt_EditBatchData.Rows.Count > 0)
            {
                obj_BatchEN.CourseId = dt_EditBatchData.Rows[0]["CourseId"].ToString();
                obj_BatchEN.InstructorId1 = dt_EditBatchData.Rows[0]["InstructorId1"].ToString();
                obj_BatchEN.InstructorId2 = dt_EditBatchData.Rows[0]["InstructorId2"].ToString();
                obj_BatchEN.StartDate = dt_EditBatchData.Rows[0]["StartDate"].ToString();
                obj_BatchEN.Timing = dt_EditBatchData.Rows[0]["Timing"].ToString();
                obj_BatchEN.ClassDuration = dt_EditBatchData.Rows[0]["ClassDuration"].ToString();
                obj_BatchEN.ClassMode = dt_EditBatchData.Rows[0]["ClassMode"].ToString();
                obj_BatchEN.MaxStrength = dt_EditBatchData.Rows[0]["MaxStrength"].ToString();
                obj_BatchEN.Discount = dt_EditBatchData.Rows[0]["Discount"].ToString();
                obj_BatchEN.VideoLink = dt_EditBatchData.Rows[0]["VideoLink"].ToString();
                obj_BatchEN.PaymentLink = dt_EditBatchData.Rows[0]["PaymentLink"].ToString();
                obj_BatchEN.VirtualClassRoomLink = dt_EditBatchData.Rows[0]["VirtualClassRoomLink"].ToString();
                obj_BatchEN.CourseDuration = dt_EditBatchData.Rows[0]["CourseDuration"].ToString();
                obj_BatchEN.Notes = dt_EditBatchData.Rows[0]["Notes"].ToString();
                obj_BatchEN.Status = dt_EditBatchData.Rows[0]["Status"].ToString();
            }

            return obj_BatchEN;
        }


        // ---------------------------------------------------------------------------- GET ALL BATCHES
        public DataTable GetAllBatches()
        {
            DataTable dt_AllBatches = new DataTable();

            dt_AllBatches = obj_dbconnect.procedure_exec_2_data_adapter("GetBatchesData", "OperateMode", "ALLBATCHES", "KeyValue", "");

            return dt_AllBatches;
        }


        // ---------------------------------------------------------------------------- GET ALL COURSES
        public SelectList GetAllCourses()
        {
            DataTable dt_AllCourses = new DataTable();

            dt_AllCourses = obj_dbconnect.procedure_exec_2_data_adapter("GetBatchesData", "OperateMode", "ALLCOURSES", "KeyValue", "");

            return ToSelectList(dt_AllCourses, "CourseId", "Name");
        }


        // ---------------------------------------------------------------------------- GET ALL INSTRUCTORS
        public SelectList GetAllInstructors()
        {
            DataTable dt_AllInstructors = new DataTable();

            dt_AllInstructors = obj_dbconnect.procedure_exec_2_data_adapter("GetBatchesData", "OperateMode", "ALLINSTRUCTORS", "KeyValue", "");

            return ToSelectList(dt_AllInstructors, "InstructorId", "Name");
        }


        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }


        // ---------------------------------------------------------------------------- GET BATCH STUDENTS
        public DataTable GetBatchStudents(string BatchId)
        {
            DataTable dt_BatchStudents = new DataTable();

            dt_BatchStudents = obj_dbconnect.procedure_exec_2_data_adapter("GetBatchesData", "OperateMode", "BATCHSTUDENTSADMIN", "KeyValue", BatchId);

            return dt_BatchStudents;
        }



        public string StudentsBatchActivate(string Mode, string BatchId, string StudentId)
        {
            string OperateMode = "";

            if (Mode == "ACTIVATE")
                OperateMode = "ActivateBatchStudent";

            else if (Mode == "INACTIVATE")
                OperateMode = "InactivateBatchStudent";

            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@BatchId", BatchId));
            parameters.Add(new KeyValuePair<string, string>("@StudentId", StudentId));
            parameters.Add(new KeyValuePair<string, string>("@Status", ""));
            parameters.Add(new KeyValuePair<string, string>("@Payment", ""));

            parameters.Add(new KeyValuePair<string, string>("@UserID", StudentId));
            parameters.Add(new KeyValuePair<string, string>("@Sno", ""));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", OperateMode));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("BatchesStudentsRelationData", parameters, outparameters);

            return Result[0].Value;
        }
    }
}