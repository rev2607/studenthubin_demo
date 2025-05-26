using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Liason
{
    public class Reviews
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();


        // ---------------------------------------------------------------------------- PROCESS DATA
        public string ProcessData_Review(string Mode, FormCollection collection)
        {
            if (Mode == "ADD")
                Mode = "AddCourseReview";

            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            Liason.EntityLayer.ReviewsEN ReviewsEN = FormDataToEntity(collection);

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@CourseId", ReviewsEN.CourseId));
            parameters.Add(new KeyValuePair<string, string>("@StudentId", ReviewsEN.StudentId));
            parameters.Add(new KeyValuePair<string, string>("@BatchId", ReviewsEN.BatchId));
            parameters.Add(new KeyValuePair<string, string>("@Rating", ReviewsEN.Rating));
            parameters.Add(new KeyValuePair<string, string>("@Comment", ReviewsEN.Comment));
            parameters.Add(new KeyValuePair<string, string>("@Status", ReviewsEN.Status));
            parameters.Add(new KeyValuePair<string, string>("@Sno", ReviewsEN.Sno));
            parameters.Add(new KeyValuePair<string, string>("@UserId", ReviewsEN.UserId));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", Mode));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("CourseReviewData", parameters, outparameters);

            return Result[0].Value;
        }


        private Liason.EntityLayer.ReviewsEN FormDataToEntity(FormCollection collection)
        {
            Liason.EntityLayer.ReviewsEN ReviewsEN = new EntityLayer.ReviewsEN();

            ReviewsEN.CourseId = collection["CourseId"];
            ReviewsEN.StudentId = collection["StudentId"];
            ReviewsEN.BatchId = collection["BatchId"];
            ReviewsEN.Rating = collection["Rating"];
            ReviewsEN.Comment = collection["Comment"];
            ReviewsEN.Status = "";
            ReviewsEN.Sno = "1"; // collection["Sno"];
            ReviewsEN.UserId = "1"; // System.Web.HttpContext.Current.Session["ID"].ToString();

            return ReviewsEN;
        }


        // ---------------------------------------------------------------------------- ACTIVE/INACTIVE REVIEW
        public string ActiveInactiveReview(string Mode, string ReviewId)
        {
            if (Mode == "INACTIVE")
                Mode = "InactiveCourseReview";

            else if (Mode == "ACTIVE")
                Mode = "ActiveCourseReview";

            string[] outparameters = new string[1];
            outparameters[0] = "@Result";
            
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@CourseId", ""));
            parameters.Add(new KeyValuePair<string, string>("@StudentId", ""));
            parameters.Add(new KeyValuePair<string, string>("@BatchId", ""));
            parameters.Add(new KeyValuePair<string, string>("@Rating", ""));
            parameters.Add(new KeyValuePair<string, string>("@Comment", ""));
            parameters.Add(new KeyValuePair<string, string>("@Status", ""));
            parameters.Add(new KeyValuePair<string, string>("@Sno", ReviewId));
            parameters.Add(new KeyValuePair<string, string>("@UserId", "1"));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", Mode));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("CourseReviewData", parameters, outparameters);

            return Result[0].Value;
        }



        // ---------------------------------------------------------------------------- EDIT REVIEW
        //public Liason.EntityLayer.ReviewsEN EditReview(string ReviewsID)
        //{
        //    Liason.EntityLayer.ReviewsEN obj_ReviewsEN = new EntityLayer.ReviewsEN();

        //    DataTable dt_EditReviewsData = new DataTable();

        //    dt_EditReviewsData = obj_dbconnect.procedure_exec_2_data_adapter("GetReviewssData", "OperateMode", "EDITReviews", "KeyValue", ReviewsID);

        //    if (dt_EditReviewsData.Rows.Count > 0)
        //    {
        //        obj_ReviewsEN.Name = dt_EditReviewsData.Rows[0]["Name"].ToString();
        //        obj_ReviewsEN.Code = dt_EditReviewsData.Rows[0]["Code"].ToString();
        //        obj_ReviewsEN.StartDate = dt_EditReviewsData.Rows[0]["StartDate"].ToString();
        //        obj_ReviewsEN.EndDate = dt_EditReviewsData.Rows[0]["EndDate"].ToString();
        //        obj_ReviewsEN.Notes = dt_EditReviewsData.Rows[0]["Notes"].ToString();
        //        obj_ReviewsEN.Image = dt_EditReviewsData.Rows[0]["Image"].ToString();
        //        obj_ReviewsEN.Status = dt_EditReviewsData.Rows[0]["Status"].ToString();
        //        obj_ReviewsEN.Sno = dt_EditReviewsData.Rows[0]["ID"].ToString();
        //    }

        //    return obj_ReviewsEN;
        //}


        // ---------------------------------------------------------------------------- GET ALL REVIEWS
        public DataTable GetAllReviews()
        {
            DataTable dt_AllReviews = new DataTable();

            dt_AllReviews = obj_dbconnect.procedure_exec_2_data_adapter("GetReviewssData", "OperateMode", "ALLREVIEWS", "KeyValue", "");

            return dt_AllReviews;
        }
    }
}