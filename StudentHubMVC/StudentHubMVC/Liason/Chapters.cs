using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Liason
{
    public class Chapters
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();


        // ---------------------------------------------------------------------------- PROCESS DATA
        public string ProcessData_Chapter(string Mode, int SectionId, string ID, FormCollection collection, HttpRequestBase Request)
        {
            if (Mode == "ADD")
                Mode = "AddChapter";

            else if (Mode == "UPDATE")
                Mode = "UpdateChapter";

            else if (Mode == "DELETE")
                Mode = "DeleteChapter";

            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            Liason.EntityLayer.CourseChaptersEN CourseChaptersEN = new EntityLayer.CourseChaptersEN();
            CourseChaptersEN = FormDataToEntity(SectionId, ID, collection, Request);

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@SectionId", CourseChaptersEN.SectionID));
            parameters.Add(new KeyValuePair<string, string>("@ChapterTitle", CourseChaptersEN.ChapterTitle));
            parameters.Add(new KeyValuePair<string, string>("@SortId", CourseChaptersEN.SortId));
            parameters.Add(new KeyValuePair<string, string>("@Description", CourseChaptersEN.Description));
            parameters.Add(new KeyValuePair<string, string>("@VideoLink1", CourseChaptersEN.VideoLink1));
            parameters.Add(new KeyValuePair<string, string>("@Video1Duration", CourseChaptersEN.Video1Duration));
            parameters.Add(new KeyValuePair<string, string>("@VideoLink2", CourseChaptersEN.VideoLink2));
            parameters.Add(new KeyValuePair<string, string>("@Video2Duration", CourseChaptersEN.Video2Duration));

            parameters.Add(new KeyValuePair<string, string>("@User", CourseChaptersEN.UserId));
            parameters.Add(new KeyValuePair<string, string>("@Sno", CourseChaptersEN.Sno));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", Mode));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("ChaptersData", parameters, outparameters);

            return Result[0].Value;
        }


        // ---------------------------------------------------------------------------- DELETE CHAPTER
        public string DeleteChapter(int SectionId, string ID)
        {
            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@SectionId", SectionId.ToString()));
            parameters.Add(new KeyValuePair<string, string>("@ChapterTitle", ""));
            parameters.Add(new KeyValuePair<string, string>("@SortId", ""));
            parameters.Add(new KeyValuePair<string, string>("@Description", ""));
            parameters.Add(new KeyValuePair<string, string>("@VideoLink1", ""));
            parameters.Add(new KeyValuePair<string, string>("@Video1Duration", ""));
            parameters.Add(new KeyValuePair<string, string>("@VideoLink2", ""));
            parameters.Add(new KeyValuePair<string, string>("@Video2Duration", ""));

            parameters.Add(new KeyValuePair<string, string>("@User", ""));
            parameters.Add(new KeyValuePair<string, string>("@Sno", ID));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", "DeleteChapter"));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("ChaptersData", parameters, outparameters);

            return Result[0].Value;
        }


        private Liason.EntityLayer.CourseChaptersEN FormDataToEntity(int SectionId, string ID, FormCollection collection, HttpRequestBase Request)
        {
            Liason.EntityLayer.CourseChaptersEN CourseChaptersEN = new Liason.EntityLayer.CourseChaptersEN();

            CourseChaptersEN.SectionID = SectionId.ToString();
            CourseChaptersEN.ChapterTitle = collection["ChapterTitle"];
            CourseChaptersEN.SortId = collection["SortId"];
            CourseChaptersEN.Description = collection["Description"];
            CourseChaptersEN.VideoLink1 = collection["VideoLink1"];
            CourseChaptersEN.VideoLink2 = collection["VideoLink2"];
            CourseChaptersEN.Video1Duration = collection["Video1Duration"];
            CourseChaptersEN.Video2Duration = collection["Video2Duration"];

            CourseChaptersEN.UserId = "1";
            CourseChaptersEN.Sno = ID;

            HttpPostedFileBase VideoLink1 = Request.Files["Video1Upload"];
            if (VideoLink1 != null && VideoLink1.FileName != "")
            {
                CourseChaptersEN.VideoLink1 = SaveVideo(VideoLink1);
            }
            else
            {
                if (collection["VideoLink1"] != "" && collection["VideoLink1"] != null)
                    CourseChaptersEN.VideoLink1 = collection["VideoLink1"];
            }

            HttpPostedFileBase VideoLink2 = Request.Files["Video2Upload"];
            if (VideoLink2 != null && VideoLink2.FileName != "")
            {
                CourseChaptersEN.VideoLink2 = SaveVideo(VideoLink2);
            }
            else
            {
                if (collection["VideoLink2"] != "" && collection["VideoLink2"] != null)
                    CourseChaptersEN.VideoLink2 = collection["VideoLink2"];
            }

            return CourseChaptersEN;
        }


        // ---------------------------------------------------------- UPLOADING VIDEO
        private string SaveVideo(HttpPostedFileBase file)
        {
            string name = System.IO.Path.GetFileNameWithoutExtension(file.FileName); //getting file name without extension 
            string ext = System.IO.Path.GetExtension(file.FileName);
            string myfile = name + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ext; //appending the name with id
            string VideosPath = ConfigurationManager.AppSettings["VideosPath"];

            var path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath(VideosPath), myfile);

            file.SaveAs(path);

            return myfile;
        }


        // ---------------------------------------------------------------------------- EDIT COURSE
        public Liason.EntityLayer.CourseChaptersEN EditChapter(string ChapterId)
        {
            Liason.EntityLayer.CourseChaptersEN obj_CourseChaptersEN = new EntityLayer.CourseChaptersEN();

            DataTable dt_EditCourseData = new DataTable();

            dt_EditCourseData = obj_dbconnect.procedure_exec_2_data_adapter("GetChaptersData", "OperateMode", "CHAPTERDETAILS", "KeyValue", ChapterId);

            if (dt_EditCourseData.Rows.Count > 0)
            {
                obj_CourseChaptersEN.ChapterTitle = dt_EditCourseData.Rows[0]["ChapterTitle"].ToString();
                obj_CourseChaptersEN.SortId = dt_EditCourseData.Rows[0]["SortId"].ToString();
                obj_CourseChaptersEN.Description = dt_EditCourseData.Rows[0]["Description"].ToString();
                obj_CourseChaptersEN.VideoLink1 = dt_EditCourseData.Rows[0]["VideoLink1"].ToString();
                obj_CourseChaptersEN.VideoLink2 = dt_EditCourseData.Rows[0]["VideoLink2"].ToString();
                obj_CourseChaptersEN.Video1Duration = dt_EditCourseData.Rows[0]["VideoDuration1"].ToString();
                obj_CourseChaptersEN.Video2Duration = dt_EditCourseData.Rows[0]["VideoDuration2"].ToString();
            }

            return obj_CourseChaptersEN;
        }


        // ---------------------------------------------------------------------------- GET ALL CHAPTERS
        public DataTable GetAllChapters(string CourseID)
        {
            DataTable dt_AllChapters = new DataTable();

            dt_AllChapters = obj_dbconnect.procedure_exec_2_data_adapter("GetChaptersData", "OperateMode", "ALLCHAPTERS", "KeyValue", CourseID);

            return dt_AllChapters;
        }
    }
}