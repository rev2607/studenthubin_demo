using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Liason
{
    public class Courses
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();


        // ---------------------------------------------------------------------------- PROCESS DATA
        public string ProcessData_Course(string Mode, string ID, FormCollection collection, HttpPostedFileBase file)
        {
            if (Mode == "ADD")
                Mode = "AddCourse";

            else if (Mode == "UPDATE")
                Mode = "UpdateCourse";

            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            Liason.EntityLayer.CourseEN CourseEN = new EntityLayer.CourseEN();
            CourseEN = FormDataToEntity(ID, collection, file);

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@Name", CourseEN.Name));
            parameters.Add(new KeyValuePair<string, string>("@UrlName", CourseEN.UrlName));
            parameters.Add(new KeyValuePair<string, string>("@Fee", CourseEN.Fee));
            parameters.Add(new KeyValuePair<string, string>("@ShortDescription", CourseEN.ShortDescription));
            parameters.Add(new KeyValuePair<string, string>("@Description", CourseEN.Description));
            parameters.Add(new KeyValuePair<string, string>("@Status", CourseEN.Status));
            parameters.Add(new KeyValuePair<string, string>("@Featured", CourseEN.Featured));
            parameters.Add(new KeyValuePair<string, string>("@Duration", CourseEN.Duration));
            parameters.Add(new KeyValuePair<string, string>("@Logo", CourseEN.Logo));
            parameters.Add(new KeyValuePair<string, string>("@CoverPicture", CourseEN.CoverPicture));
            parameters.Add(new KeyValuePair<string, string>("@Curriculum", CourseEN.Curriculum));
            parameters.Add(new KeyValuePair<string, string>("@DemoLink", CourseEN.DemoLink));
            parameters.Add(new KeyValuePair<string, string>("@DemoLink2", CourseEN.DemoLink2));

            parameters.Add(new KeyValuePair<string, string>("@UserID", CourseEN.UserId));
            parameters.Add(new KeyValuePair<string, string>("@Sno", CourseEN.Sno));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", Mode));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("CoursesData", parameters, outparameters);

            return Result[0].Value;
        }


        private Liason.EntityLayer.CourseEN FormDataToEntity(string ID, FormCollection collection, HttpPostedFileBase file)
        {
            Liason.EntityLayer.CourseEN CourseEN = new Liason.EntityLayer.CourseEN();

            CourseEN.Name = collection["Name"];
            CourseEN.UrlName = collection["UrlName"];
            CourseEN.Fee = collection["Fee"];
            CourseEN.ShortDescription = collection["ShortDescription"];
            CourseEN.Description = collection["Description"];
            CourseEN.Status = collection["Status"];
            CourseEN.Featured = collection["Featured"];
            CourseEN.Duration = collection["Duration"];
            CourseEN.CoverPicture = ""; // collection["Address"];
            CourseEN.Curriculum = collection["Curriculum"];
            CourseEN.DemoLink = collection["DemoLink"];
            CourseEN.DemoLink2 = collection["DemoLink2"];
            CourseEN.UserId = "1";
            CourseEN.Sno = ID;

            if (file != null && file.FileName != "")
            {
                string name = System.IO.Path.GetFileNameWithoutExtension(file.FileName); //getting file name without extension 
                string ext = System.IO.Path.GetExtension(file.FileName);
                string myfile = name + "_" + DateTime.Now.ToString("yyyymmdd") + ext; //appending the name with id

                var path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/assets/img/logos/"), myfile);

                file.SaveAs(path);

                CourseEN.Logo = myfile;
            }

            else
            {
                if (collection["Logo"] != "" && collection["Logo"] != null)
                    CourseEN.Logo = collection["Logo"];

                else
                {
                    if (collection["Gender"] == "MALE")
                        CourseEN.Logo = "profileman.png";

                    else if (collection["Gender"] == "FEMALE")
                        CourseEN.Logo = "profilewoman.png";
                }
            }

            return CourseEN;
        }


        // ---------------------------------------------------------------------------- EDIT COURSE
        public Liason.EntityLayer.CourseEN EditCourse(string CourseID)
        {
            Liason.EntityLayer.CourseEN obj_CourseEN = new EntityLayer.CourseEN();

            DataTable dt_EditCourseData = new DataTable();

            dt_EditCourseData = obj_dbconnect.procedure_exec_2_data_adapter("GetCoursesData", "OperateMode", "EDITCOURSE", "KeyValue", CourseID);

            if (dt_EditCourseData.Rows.Count > 0)
            {
                obj_CourseEN.Name = dt_EditCourseData.Rows[0]["Name"].ToString();
                obj_CourseEN.UrlName = dt_EditCourseData.Rows[0]["UrlName"].ToString();
                obj_CourseEN.Fee = dt_EditCourseData.Rows[0]["Fee"].ToString();
                obj_CourseEN.ShortDescription = dt_EditCourseData.Rows[0]["ShortDescription"].ToString();
                obj_CourseEN.Description = dt_EditCourseData.Rows[0]["Description"].ToString();
                obj_CourseEN.Status = dt_EditCourseData.Rows[0]["Status"].ToString();
                obj_CourseEN.Featured = dt_EditCourseData.Rows[0]["Featured"].ToString();
                obj_CourseEN.Duration = dt_EditCourseData.Rows[0]["Duration"].ToString();
                obj_CourseEN.Logo = dt_EditCourseData.Rows[0]["Logo"].ToString();
                obj_CourseEN.CoverPicture = dt_EditCourseData.Rows[0]["CoverPicture"].ToString();
                obj_CourseEN.Curriculum = dt_EditCourseData.Rows[0]["Curriculum"].ToString();
                obj_CourseEN.DemoLink = dt_EditCourseData.Rows[0]["DemoLink"].ToString();
                obj_CourseEN.DemoLink2 = dt_EditCourseData.Rows[0]["DemoLink2"].ToString();
            }

            return obj_CourseEN;
        }


        // ---------------------------------------------------------------------------- GET ALL COURSES
        public DataTable GetAllCourses()
        {
            DataTable dt_AllCourses = new DataTable();

            dt_AllCourses = obj_dbconnect.procedure_exec_2_data_adapter("GetCoursesData", "OperateMode", "ALLCOURSES", "KeyValue", "");

            return dt_AllCourses;
        }


        // ---------------------------------------------------------------------------- GET COURSE NAME
        public string GetCourseName(string CourseId)
        {
            DataTable dt_AllCourses = new DataTable();

            dt_AllCourses = obj_dbconnect.procedure_exec_2_data_adapter("GetCoursesData", "OperateMode", "SECTIONCOURSENAME", "KeyValue", CourseId);

            string CourseName = "";

            if(dt_AllCourses.Rows.Count > 0)
            {
                CourseName = dt_AllCourses.Rows[0]["CourseName"].ToString();
            }

            return CourseName;
        }
    }
}