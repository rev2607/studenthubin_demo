using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Liason
{
    public class Instructors
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();


        // ---------------------------------------------------------------------------- PROCESS DATA
        public string ProcessData_Instructor(string Mode, string ID, FormCollection collection, HttpPostedFileBase file)
        {
            if (Mode == "ADD")
                Mode = "AddInstructor";

            else if (Mode == "UPDATE")
                Mode = "UpdateInstructor";

            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            Liason.EntityLayer.InstructorEN InstructorEN = new EntityLayer.InstructorEN();
            InstructorEN = FormDataToEntity(ID, collection, file);

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@FullName", InstructorEN.FullName));
            parameters.Add(new KeyValuePair<string, string>("@Age", InstructorEN.Age));
            parameters.Add(new KeyValuePair<string, string>("@Gender", InstructorEN.Gender));
            parameters.Add(new KeyValuePair<string, string>("@Education", InstructorEN.Education));
            parameters.Add(new KeyValuePair<string, string>("@WorkExperience", InstructorEN.WorkExperience));
            parameters.Add(new KeyValuePair<string, string>("@CurrentWorkPosition", InstructorEN.CurrentWorkPosition));
            parameters.Add(new KeyValuePair<string, string>("@Description", InstructorEN.Description));
            parameters.Add(new KeyValuePair<string, string>("@Address", InstructorEN.Address));
            parameters.Add(new KeyValuePair<string, string>("@City", InstructorEN.City));
            parameters.Add(new KeyValuePair<string, string>("@State", InstructorEN.State));
            parameters.Add(new KeyValuePair<string, string>("@Country", InstructorEN.Country));
            parameters.Add(new KeyValuePair<string, string>("@Dob", InstructorEN.Dob));
            parameters.Add(new KeyValuePair<string, string>("@Pincode", InstructorEN.Pincode));
            parameters.Add(new KeyValuePair<string, string>("@Email", InstructorEN.Email));
            parameters.Add(new KeyValuePair<string, string>("@PasswordUser", InstructorEN.Password));
            parameters.Add(new KeyValuePair<string, string>("@PasswordStatus", ""));
            parameters.Add(new KeyValuePair<string, string>("@Email2", InstructorEN.Email2));
            parameters.Add(new KeyValuePair<string, string>("@Phone1", InstructorEN.Phone1));
            parameters.Add(new KeyValuePair<string, string>("@Phone2", InstructorEN.Phone2));
            parameters.Add(new KeyValuePair<string, string>("@StatusUser", InstructorEN.StatusUser));
            parameters.Add(new KeyValuePair<string, string>("@Featured", InstructorEN.Featured));
            parameters.Add(new KeyValuePair<string, string>("@Website", InstructorEN.Website));
            parameters.Add(new KeyValuePair<string, string>("@ProfilePic", InstructorEN.ProfilePic));
            parameters.Add(new KeyValuePair<string, string>("@CoverPic", InstructorEN.CoverPic));
            parameters.Add(new KeyValuePair<string, string>("@TwitterLink", InstructorEN.TwitterLink));
            parameters.Add(new KeyValuePair<string, string>("@FacebookLink", InstructorEN.FacebookLink));
            parameters.Add(new KeyValuePair<string, string>("@LinkedinLink", InstructorEN.LinkedinLink));
            parameters.Add(new KeyValuePair<string, string>("@VerificationCode", InstructorEN.VerificationCode));

            parameters.Add(new KeyValuePair<string, string>("@UserID", InstructorEN.UserId));
            parameters.Add(new KeyValuePair<string, string>("@Sno", InstructorEN.ID));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", Mode));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("InstructorsData", parameters, outparameters);

            return Result[0].Value;
        }


        private Liason.EntityLayer.InstructorEN FormDataToEntity(string ID, FormCollection collection, HttpPostedFileBase file)
        {
            Liason.EntityLayer.InstructorEN InstructorEN = new Liason.EntityLayer.InstructorEN();

            InstructorEN.FullName = collection["FullName"];
            InstructorEN.Age = collection["Age"];
            InstructorEN.Gender = collection["Gender"];
            InstructorEN.Education = collection["Education"];
            InstructorEN.WorkExperience = collection["WorkExperience"];
            InstructorEN.CurrentWorkPosition = collection["CurrentWorkPosition"];
            InstructorEN.Description = collection["Description"];
            InstructorEN.Address = collection["Address"];
            InstructorEN.City = collection["City"];
            InstructorEN.State = collection["State"];
            InstructorEN.Country = collection["Country"];

            if (collection["Dob"] != null && collection["Dob"] != "")
                InstructorEN.Dob = (DateTime.Parse(collection["Dob"])).ToString("yyyy-MM-dd");
            else
                InstructorEN.Dob = "1980-01-01";

            InstructorEN.Pincode = collection["Pincode"];
            InstructorEN.Email = collection["Email"];

            if (ID == "")
                InstructorEN.Password = collection["Password"];
            else
                InstructorEN.Password = "";

            // change password
            if (InstructorEN.Password != "" && InstructorEN.Password != null && ID != "")
            {
                string response = PasswordChange(ID, collection["Email"], InstructorEN.Password, "1");
            }

            InstructorEN.Email2 = collection["Email2"];
            InstructorEN.Phone1 = collection["Phone1"];
            InstructorEN.Phone2 = collection["Phone2"];
            InstructorEN.StatusUser = collection["StatusUser"];
            InstructorEN.Featured = collection["Featured"];
            InstructorEN.Website = collection["Website"];
            InstructorEN.CoverPic = collection["CoverPic"];
            InstructorEN.FacebookLink = collection["FacebookLink"];
            InstructorEN.TwitterLink = collection["TwitterLink"];
            InstructorEN.LinkedinLink = collection["LinkedinLink"];

            InstructorEN.UserId = "1";
            InstructorEN.ID = ID;

            if (file != null && file.FileName != "")
            {
                string name = System.IO.Path.GetFileNameWithoutExtension(file.FileName); //getting file name without extension 
                string ext = System.IO.Path.GetExtension(file.FileName);
                string myfile = name + "_" + DateTime.Now.ToString("yyyymmdd") + ext; //appending the name with id

                var path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/assets/img/profilepics/"), myfile);

                file.SaveAs(path);

                InstructorEN.ProfilePic = myfile;
            }

            else
            {
                if (collection["ProfilePic"] != "" && collection["ProfilePic"] != null)
                    InstructorEN.ProfilePic = collection["ProfilePic"];

                else
                {
                    if (collection["Gender"] == "MALE")
                        InstructorEN.ProfilePic = "profileman.png";

                    else if (collection["Gender"] == "FEMALE")
                        InstructorEN.ProfilePic = "profilewoman.png";
                }
            }

            if (ID == "")
                InstructorEN.VerificationCode = obj_Help.RandomString(30);
            else
                InstructorEN.VerificationCode = "";

            return InstructorEN;
        }


        // ---------------------------------------------------------------------------- EDIT INSTRUCTOR
        public Liason.EntityLayer.InstructorEN EditInstructor(string InstructorID)
        {
            Liason.EntityLayer.InstructorEN obj_InstructorEN = new EntityLayer.InstructorEN();

            DataTable dt_EditCourseData = new DataTable();

            dt_EditCourseData = obj_dbconnect.procedure_exec_2_data_adapter("GetInstructorsData", "OperateMode", "EDITINSTRUCTOR", "KeyValue", InstructorID);

            if (dt_EditCourseData.Rows.Count > 0)
            {
                obj_InstructorEN.FullName = dt_EditCourseData.Rows[0]["Name"].ToString();
                obj_InstructorEN.Age = dt_EditCourseData.Rows[0]["Age"].ToString();
                obj_InstructorEN.Gender = dt_EditCourseData.Rows[0]["Gender"].ToString();
                obj_InstructorEN.Description = dt_EditCourseData.Rows[0]["Description"].ToString();
                obj_InstructorEN.Education = dt_EditCourseData.Rows[0]["Education"].ToString();
                obj_InstructorEN.WorkExperience = dt_EditCourseData.Rows[0]["WorkExperience"].ToString();
                obj_InstructorEN.CurrentWorkPosition = dt_EditCourseData.Rows[0]["CurrentWorkPosition"].ToString();
                obj_InstructorEN.Description = dt_EditCourseData.Rows[0]["Description"].ToString();
                obj_InstructorEN.Address = dt_EditCourseData.Rows[0]["Address"].ToString();
                obj_InstructorEN.City = dt_EditCourseData.Rows[0]["City"].ToString();
                obj_InstructorEN.State = dt_EditCourseData.Rows[0]["State"].ToString();
                obj_InstructorEN.Country = dt_EditCourseData.Rows[0]["Country"].ToString();
                obj_InstructorEN.Pincode = dt_EditCourseData.Rows[0]["Pincode"].ToString();
                obj_InstructorEN.Email = dt_EditCourseData.Rows[0]["Email"].ToString();
                obj_InstructorEN.Email2 = dt_EditCourseData.Rows[0]["Email2"].ToString();
                obj_InstructorEN.Phone1 = dt_EditCourseData.Rows[0]["Phone1"].ToString();
                obj_InstructorEN.Phone2 = dt_EditCourseData.Rows[0]["Phone2"].ToString();
                obj_InstructorEN.StatusUser = dt_EditCourseData.Rows[0]["Status"].ToString();
                obj_InstructorEN.Featured = dt_EditCourseData.Rows[0]["Featured"].ToString();
                obj_InstructorEN.Website = dt_EditCourseData.Rows[0]["Website"].ToString();
                obj_InstructorEN.Dob = dt_EditCourseData.Rows[0]["Dob"].ToString();
                obj_InstructorEN.ProfilePic = dt_EditCourseData.Rows[0]["Photo"].ToString();
                //obj_InstructorEN.CoverPhoto = dt_EditCourseData.Rows[0]["CoverPhoto"].ToString();
                obj_InstructorEN.FacebookLink = dt_EditCourseData.Rows[0]["FacebookLink"].ToString();
                obj_InstructorEN.TwitterLink = dt_EditCourseData.Rows[0]["TwitterLink"].ToString();
                obj_InstructorEN.LinkedinLink = dt_EditCourseData.Rows[0]["LinkedinLink"].ToString();
            }

            return obj_InstructorEN;
        }


        // ---------------------------------------------------------------------------- STUDENT LOGIN
        /// <summary>
        /// For admin Login (Give Email, Password)
        /// Returns : FAILED - LOGGEDIN - INCORRECT PASSWORD - USER DO NOT EXIST
        /// </summary>
        /// <returns>FAILED - LOGGEDIN - INCORRECT PASSWORD - USER DO NOT EXIST</returns>
        public string InstructorLogin(FormCollection collection)
        {
            string LoginType = "";
            string Response = "";

            if (collection.AllKeys.Contains("loginemail"))
            {
                if (collection["loginemail"].ToString() != "" && collection["logintype"].ToString() == "INSTRUCTOR")
                {
                    LoginType = "LoginInstructor";

                    string[] outparameters = new string[1];
                    outparameters[0] = "@Result";

                    var parameters = new List<KeyValuePair<string, string>>();
                    parameters.Add(new KeyValuePair<string, string>("@OperateMode", LoginType));
                    parameters.Add(new KeyValuePair<string, string>("@Email", collection["loginemail"]));
                    parameters.Add(new KeyValuePair<string, string>("@Password", collection["loginpassword"]));
                    parameters.Add(new KeyValuePair<string, string>("@UserID", ""));
                    parameters.Add(new KeyValuePair<string, string>("@VerificationCode", ""));

                    var Result = new List<KeyValuePair<string, string>>();

                    Result = obj_dbconnect.ProcedureDynamic("InstructorLogin", parameters, outparameters);
                    Response = Result[0].Value;
                }
            }

            // if login successful get the name and userid of the logged in user

            return Response;
        }


        // ---------------------------------------------------------------------------- GET USER LOGIN
        public DataTable GetLoginDetails(string Email)
        {
            string OperateMode = "";

            OperateMode = "INSTRUCTORLOGINDATA";

            DataTable dt_LoginDetails = new DataTable();

            dt_LoginDetails = obj_dbconnect.procedure_exec_2_data_adapter("GetInstructorsData", "OperateMode", OperateMode, "KeyValue", Email);

            return dt_LoginDetails;
        }


        // ---------------------------------------------------------------------------- GET ALL INSTRUCTORS
        public DataTable GetAllInstructors()
        {
            DataTable dt_AllInsturctors = new DataTable();

            dt_AllInsturctors = obj_dbconnect.procedure_exec_2_data_adapter("GetInstructorsData", "OperateMode", "ALLINSTRUCTORS", "KeyValue", "");

            return dt_AllInsturctors;
        }


        // ---------------------------------------------------------------------------- GET INSTRUCTOR BATCHES
        public DataTable GetInstructorBatches(string InstructorId)
        {
            DataTable dt_InstructorBatches = new DataTable();

            dt_InstructorBatches = obj_dbconnect.procedure_exec_2_data_adapter("GetInstructorsData", "OperateMode", "INSTRUCTORBATCHES", "KeyValue", InstructorId);

            return dt_InstructorBatches;
        }


        // ---------------------------------------------------------------------------- GET INSTRUCTOR BATCH STUDENTS
        public DataTable GetInstructorBatchStudents(string BatchId)
        {
            DataTable dt_InstructorBatchstudents = new DataTable();

            dt_InstructorBatchstudents = obj_dbconnect.procedure_exec_2_data_adapter("GetInstructorsData", "OperateMode", "BATCHSTUDENTSINSTRUCTOR", "KeyValue", BatchId);

            return dt_InstructorBatchstudents;
        }


        // ---------------------------------------------------------------------------- CHANGE PASSWORD
        public string PasswordChange(string ID, string email, string password, string userID)
        {
            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@Email", email));
            parameters.Add(new KeyValuePair<string, string>("@Password", password));
            parameters.Add(new KeyValuePair<string, string>("@VerificationCode", ""));

            parameters.Add(new KeyValuePair<string, string>("@UserID", userID));
            parameters.Add(new KeyValuePair<string, string>("@Sno", ID));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", "UpdatePassword"));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("InstructorLogin", parameters, outparameters);

            return Result[0].Value;
        }
    }
}