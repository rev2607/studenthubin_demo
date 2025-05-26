using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Liason
{
    public class Students
    {
        DBConnect obj_dbconnect = new DBConnect();
        Liason.Help obj_Help = new Liason.Help();

        Entities db = new Entities();


        // ---------------------------------------------------------------------------- PROCESS DATA
        public string ProcessData_Student(string Mode, string ID, FormCollection collection, HttpPostedFileBase file)
        {
            if (Mode == "ADD")
                Mode = "AddStudent";

            else if (Mode == "UPDATE")
                Mode = "UpdateStudent";

            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            Liason.EntityLayer.StudentEN StudentEN = new EntityLayer.StudentEN();
            StudentEN = FormDataToEntity(ID, collection, file);

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@FullName", StudentEN.FullName));
            parameters.Add(new KeyValuePair<string, string>("@Gender", StudentEN.Gender));
            parameters.Add(new KeyValuePair<string, string>("@Age", StudentEN.Age.ToString()));
            parameters.Add(new KeyValuePair<string, string>("@Education", StudentEN.Education));
            parameters.Add(new KeyValuePair<string, string>("@WhoReferredUs", StudentEN.WhoReferredUs));
            parameters.Add(new KeyValuePair<string, string>("@WorkExperience", StudentEN.WorkExperience));
            parameters.Add(new KeyValuePair<string, string>("@CurrentWorkPosition", StudentEN.CurrentWorkPosition));
            parameters.Add(new KeyValuePair<string, string>("@Description", StudentEN.Description));
            parameters.Add(new KeyValuePair<string, string>("@Address", StudentEN.Address));
            parameters.Add(new KeyValuePair<string, string>("@City", StudentEN.City));
            parameters.Add(new KeyValuePair<string, string>("@State", StudentEN.State));
            parameters.Add(new KeyValuePair<string, string>("@Country", StudentEN.Country));
            parameters.Add(new KeyValuePair<string, string>("@Dob", StudentEN.Dob));
            parameters.Add(new KeyValuePair<string, string>("@Pincode", StudentEN.Pincode));
            parameters.Add(new KeyValuePair<string, string>("@Email", StudentEN.Email));
            parameters.Add(new KeyValuePair<string, string>("@PasswordUser", StudentEN.Password));
            //parameters.Add(new KeyValuePair<string, string>("@PasswordStatus", StudentEN.PasswordStatus));
            parameters.Add(new KeyValuePair<string, string>("@Email2", StudentEN.Email2));
            parameters.Add(new KeyValuePair<string, string>("@Phone1", StudentEN.Phone1));
            parameters.Add(new KeyValuePair<string, string>("@Phone2", StudentEN.Phone2));
            parameters.Add(new KeyValuePair<string, string>("@ProfilePic", StudentEN.ProfilePic));
            parameters.Add(new KeyValuePair<string, string>("@StatusUser", StudentEN.Status));
            parameters.Add(new KeyValuePair<string, string>("@VerificationCode", StudentEN.VerificationCode));

            parameters.Add(new KeyValuePair<string, string>("@UserID", StudentEN.UserId));
            parameters.Add(new KeyValuePair<string, string>("@Sno", StudentEN.Sno));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", Mode));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("StudentsData", parameters, outparameters);

            return Result[0].Value;
        }


        private Liason.EntityLayer.StudentEN FormDataToEntity(string ID, FormCollection collection, HttpPostedFileBase file)
        {
            Liason.EntityLayer.StudentEN StudentEN = new Liason.EntityLayer.StudentEN();

            StudentEN.FullName = collection["FullName"];
            StudentEN.Gender = collection["Gender"];
            StudentEN.Age = int.Parse(collection["Age"]);
            StudentEN.Education = collection["Education"];
            StudentEN.WhoReferredUs = collection["WhoReferredUs"];
            StudentEN.WorkExperience = collection["WorkExperience"];
            StudentEN.CurrentWorkPosition = collection["CurrentWorkPosition"];
            StudentEN.Description = collection["Description"];
            StudentEN.Address = collection["Address"];
            StudentEN.City = collection["City"];
            StudentEN.State = collection["State"];
            StudentEN.Country = collection["Country"];

            if (collection["Dob"] != null && collection["Dob"] != "")
                StudentEN.Dob = (DateTime.Parse(collection["Dob"])).ToString("yyyy-MM-dd");
            else
                StudentEN.Dob = "1999-01-01";

            StudentEN.Pincode = collection["Pincode"];
            StudentEN.Email = collection["Email"];

            if (ID == "")
                StudentEN.Password = collection["Password"];
            else
                StudentEN.Password = "";

            // change password
            if(StudentEN.Password != "" && StudentEN.Password != null && ID != "")
            {
                string response = PasswordChange(ID, collection["Email"], StudentEN.Password, "1");
            }

            StudentEN.Email2 = collection["Email2"];
            StudentEN.Phone1 = collection["Phone1"];
            StudentEN.Phone2 = collection["Phone2"];
            StudentEN.ProfilePic = collection["ProfilePic"];
            StudentEN.Status = collection["Status"];

            if (ID == "")
                StudentEN.VerificationCode = obj_Help.RandomString(30);
            else
                StudentEN.VerificationCode = "";

            StudentEN.UserId = "1";
            StudentEN.Sno = ID;

            if (file != null && file.FileName != "")
            {
                string name = System.IO.Path.GetFileNameWithoutExtension(file.FileName); //getting file name without extension 
                string ext = System.IO.Path.GetExtension(file.FileName);
                string myfile = name + "_" + DateTime.Now.ToString("yyyymmdd") + ext; //appending the name with id

                var path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/assets/img/profilepics/"), myfile);

                file.SaveAs(path);

                StudentEN.ProfilePic = myfile;
            }

            else
            {
                if (collection["ProfilePic"] != "" && collection["ProfilePic"] != null)
                    StudentEN.ProfilePic = collection["ProfilePic"];

                else
                {
                    if (collection["Gender"] == "MALE")
                        StudentEN.ProfilePic = "profileman.png";

                    else if (collection["Gender"] == "FEMALE")
                        StudentEN.ProfilePic = "profilewoman.png";
                }
            }

            return StudentEN;
        }
        

        // ---------------------------------------------------------------------------- EDIT STUDENT
        public Liason.EntityLayer.StudentEN EditStudent(string StudentID)
        {
            Liason.EntityLayer.StudentEN obj_StudentEN = new EntityLayer.StudentEN();

            DataTable dt_EditStudentData = new DataTable();

            dt_EditStudentData = obj_dbconnect.procedure_exec_2_data_adapter("GetStudentsData", "OperateMode", "EDITSTUDENT", "KeyValue", StudentID);

            if(dt_EditStudentData.Rows.Count > 0)
            {
                obj_StudentEN.FullName = dt_EditStudentData.Rows[0]["FullName"].ToString();
                obj_StudentEN.Gender = dt_EditStudentData.Rows[0]["Gender"].ToString();
                obj_StudentEN.Age = int.Parse(dt_EditStudentData.Rows[0]["Age"].ToString());
                obj_StudentEN.Education = dt_EditStudentData.Rows[0]["Education"].ToString();
                obj_StudentEN.WhoReferredUs = dt_EditStudentData.Rows[0]["WhoReferredUs"].ToString();
                obj_StudentEN.WorkExperience = dt_EditStudentData.Rows[0]["WorkExperience"].ToString();
                obj_StudentEN.CurrentWorkPosition = dt_EditStudentData.Rows[0]["CurrentWorkPosition"].ToString();
                obj_StudentEN.Description = dt_EditStudentData.Rows[0]["Description"].ToString();
                obj_StudentEN.Address = dt_EditStudentData.Rows[0]["Address"].ToString();
                obj_StudentEN.City = dt_EditStudentData.Rows[0]["City"].ToString();
                obj_StudentEN.State = dt_EditStudentData.Rows[0]["State"].ToString();
                obj_StudentEN.Country = dt_EditStudentData.Rows[0]["Country"].ToString();
                obj_StudentEN.Dob = dt_EditStudentData.Rows[0]["Dob"].ToString();
                obj_StudentEN.Pincode = dt_EditStudentData.Rows[0]["Pincode"].ToString();
                obj_StudentEN.Email = dt_EditStudentData.Rows[0]["Email"].ToString();
                obj_StudentEN.Email2 = dt_EditStudentData.Rows[0]["Email2"].ToString();
                obj_StudentEN.Phone1 = dt_EditStudentData.Rows[0]["Phone1"].ToString();
                obj_StudentEN.Phone2 = dt_EditStudentData.Rows[0]["Phone2"].ToString();
                obj_StudentEN.Status = dt_EditStudentData.Rows[0]["Status"].ToString();
                obj_StudentEN.ProfilePic = dt_EditStudentData.Rows[0]["ProfilePic"].ToString();
            }

            return obj_StudentEN;
        }


        // ---------------------------------------------------------------------------- UPDATE STUDENT
        public string StudentUpdate()
        {
            string Result = "";

            //Result = ProcessData_Student("UpdateStudent");

            if (Result == "Updated")
            {
                //TestsSelectedSave();
            }

            return Result;
        }


        // ---------------------------------------------------------------------------- UPDATE STUDENT
        public string VerifyEmail(string VerificationCode, string Email)
        {
            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@Email", Email));
            parameters.Add(new KeyValuePair<string, string>("@Password", ""));
            parameters.Add(new KeyValuePair<string, string>("@VerificationCode", VerificationCode));

            parameters.Add(new KeyValuePair<string, string>("@UserID", "1"));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", "VerifyEmail"));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("StudentLogin", parameters, outparameters);

            return Result[0].Value;
        }


        // ---------------------------------------------------------------------------- STUDENT LOGIN
        /// <summary>
        /// For admin Login (Give Email, Password)
        /// Returns : FAILED - LOGGEDIN - INCORRECT PASSWORD - USER DO NOT EXIST - SUCCESS
        /// </summary>
        /// <returns>FAILED - LOGGEDIN - INCORRECT PASSWORD - USER DO NOT EXIST - SUCCESS</returns>
        public string StudentLogin(FormCollection collection)
        {
            string LoginType = "";
            string Response = "";

            if(collection.AllKeys.Contains("loginemail"))
            {
                if(collection["loginemail"].ToString() != "")
                {
                    if (collection["logintype"].ToString() == "STUDENT")
                        LoginType = "LoginStudent";

                    else if (collection["logintype"].ToString() == "INSTRUCTOR")
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

                    Result = obj_dbconnect.ProcedureDynamic("StudentLogin", parameters, outparameters);
                    Response = Result[0].Value;
                }
            }            

            // if login successful get the name and userid of the logged in user

            return Response;
        }


        /// <summary>
        /// For admin Login (Give Email, Password)
        /// Returns : FAILED - LOGGEDIN - INCORRECT PASSWORD - USER DO NOT EXIST - Invalid Email Id - SUCCESS
        /// </summary>
        /// <returns>FAILED - LOGGEDIN - INCORRECT PASSWORD - USER DO NOT EXIST - Invalid Email Id - SUCCESS</returns>
        public string StudentLogin(string Email, string Password)
        {
            string LoginType = "";
            string Response = "";

            if (Email != "" && Password != "")
            {
                LoginType = "LoginStudent";

                string[] outparameters = new string[1];
                outparameters[0] = "@Result";

                var parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("@OperateMode", LoginType));
                parameters.Add(new KeyValuePair<string, string>("@Email", Email));
                parameters.Add(new KeyValuePair<string, string>("@Password", Password));
                parameters.Add(new KeyValuePair<string, string>("@UserID", ""));
                parameters.Add(new KeyValuePair<string, string>("@VerificationCode", ""));

                var Result = new List<KeyValuePair<string, string>>();

                Result = obj_dbconnect.ProcedureDynamic("StudentLogin", parameters, outparameters);
                Response = Result[0].Value;
            }

            else
                Response = "Invalid Email Id or Password";

            // if login successful get the name and userid of the logged in user

            return Response;

            //shub_students student = db.shub_students.Where(s => s.std_Email == Email).SingleOrDefault();

            //if(student == null)
            //{
            //    return "Email ID Does not Exist. Please Register to Proceed";
            //}

            //else
            //{
            //    if (student.std_Password == Password)
            //        return "TRUE";

            //    else
            //        return "Password is Incorrect. Please Try again";
            //}
        }

        // ---------------------------------------------------------------------------- GET USER LOGIN
        public DataTable GetLoginDetails(string Email)
        {
            string OperateMode = "";

            OperateMode = "STUDENTLOGINDATA";
            
            DataTable dt_LoginDetails = new DataTable();

            dt_LoginDetails = obj_dbconnect.procedure_exec_2_data_adapter("GetStudentsData", "OperateMode", OperateMode, "KeyValue", Email);

            return dt_LoginDetails;
        }


        // ---------------------------------------------------------------------------- RESET PASSWORD



        // ---------------------------------------------------------------------------- GET ALL STUDENTS DATA
        public DataTable GetAllStudents()
        {
            DataTable dt_AllStudents = new DataTable();

            dt_AllStudents = obj_dbconnect.procedure_exec_2_data_adapter("GetStudentsData", "OperateMode", "ALLSTUDENTS", "KeyValue", "");

            return dt_AllStudents;
        }


        // ---------------------------------------------------------------------------- GET VERFICATION CODE
        public DataTable GetVerificationCode(string ID)
        {
            DataTable dt_Verification = new DataTable();

            dt_Verification = obj_dbconnect.procedure_exec_2_data_adapter("GetStudentsData", "OperateMode", "VERIFYEMAIL", "KeyValue", ID);

            return dt_Verification;
        }


        // --------------------------- STUDENT PANEL

        // ---------------------------------------------------------------------------- DASHBOARD DATA
        public DataSet GetStudentDashboard(string ID)
        {
            DataSet ds_Result = new DataSet();

            ds_Result = obj_dbconnect.procedure_exec_2_data_adapter_dataset("GetStudentDashboard", "OperateMode", "", "KeyValue", ID);

            return ds_Result;
        }


        // ---------------------------------------------------------------------------- REGISTER COURSE
        public string RegisterCourseBatch(string BatchId, string StudentId)
        {
            string[] outparameters = new string[1];
            outparameters[0] = "@Result";

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("@BatchId", BatchId));
            parameters.Add(new KeyValuePair<string, string>("@StudentId", StudentId));
            parameters.Add(new KeyValuePair<string, string>("@Status", ""));
            parameters.Add(new KeyValuePair<string, string>("@Payment", ""));

            parameters.Add(new KeyValuePair<string, string>("@UserID", StudentId));
            parameters.Add(new KeyValuePair<string, string>("@Sno", ""));

            parameters.Add(new KeyValuePair<string, string>("@OperateMode", "AddBatchStudent"));

            var Result = new List<KeyValuePair<string, string>>();

            Result = obj_dbconnect.ProcedureDynamic("BatchesStudentsRelationData", parameters, outparameters);

            return Result[0].Value;
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

            Result = obj_dbconnect.ProcedureDynamic("StudentLogin", parameters, outparameters);

            return Result[0].Value;
        }
    }
}