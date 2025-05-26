using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace StudentHubMVC
{
    /// <summary>
    /// Summary description for bntstudenthubdata
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class bntstudenthubdata : System.Web.Services.WebService
    {

        Liason.DBConnect Obj_DBConnect = new Liason.DBConnect();
        Liason.Help Obj_Help = new Liason.Help();

        // ---------------------------------------------------------------------- ENQUIRY FORM
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string enquiry(string name, string email, string phonenumber, string message)
        {
            string response = "";

            try
            {
                // SAVING TO DB
                Liason.Enquiries obj_Enquiry = new Liason.Enquiries();
                obj_Enquiry.ProcessData_Enquiry(name, email, phonenumber, "", message);

                // SENDING EMAIL
                StringBuilder Body = new StringBuilder();
                Body.AppendLine("<h2>ENQUIRY FROM STUDNETHUB.IN</h2> <br><br>");
                Body.AppendLine("<b>Name:</b><br> " + name + "<br><br>");
                //Body.AppendLine();
                Body.AppendLine("<b>Email:</b><br> " + email + "<br><br>");
                //Body.AppendLine();
                Body.AppendLine("<b>Phone Number:</b><br> " + phonenumber + "<br><br>");
                //Body.AppendLine();
                Body.AppendLine("<b>Message:</b><br> <p>" + message + "</p>");

                response = Obj_Help.send_email("Enquiry for StudentHub.in from Website", Body.ToString(), email, "no-reply@studenthub.in");

                //response = "SUBMITTED";
            }
            catch (Exception Ex)
            {
                response = "ERROR";
                Obj_Help.ExcpetionsHandling(Ex);
            }
            finally
            {

            }

            return response;
        }


        //// ---------------------------------------------------------------------- STUDENT REGISTRATION
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string studentregister(string name, string course, string email, string phonenumber, string password)
        //{
        //    string response = "";

        //    Liason.Students Obj_Students = new Liason.Students();

        //    string VerificationCode = Obj_Help.RandomString(30);

        //    Obj_Students.FullName = name;
        //    //Obj_Students.CoursesSelected = course;
        //    Obj_Students.Email = email;
        //    Obj_Students.Phone1 = phonenumber;
        //    Obj_Students.Password = password; // need to encrypt the password for saving
        //    Obj_Students.StatusUser = "NEW";
        //    Obj_Students.VerificationCode = VerificationCode;

        //    response = Obj_Students.StudentAdd();

        //    // SENDING VERIFICATION EMAIL
        //    if (response != "STUDENT EMAIL ALREADY EXISTS")
        //    {
        //        string body = "Hi <b>" + name + "</b>,<br /><br />";
        //        body += "You have recently registered with us, StudentHub.in! Please follow below link to Verify Your Email.<br /><br />";
        //        body += "Verifcation Link- <b>http://www.studenthub.in/student-verify.html?val=" + VerificationCode + "&i=" + response + "</b><br /><br /><br />";
        //        body += "<b>Welcome to StudentHub.in.</b><br /><br />";
        //        body += "<a href=\"www.studenthub.in\"><img class=\"img - responsive center - block\" src =\"www.studenthub.in/assets/img/StudenthubMenuLogo.png\" alt=\"StudentHub.in\" width=\"150px\"></a>";

        //        // ******* add few social network links
        //        // ******* make email templates and save them seperately in txt files as dynamic content instead of static

        //        Obj_Help.send_email("StudentHub.in - Verification Email for New Registration", body, email, "no-reply@studenthub.in");
        //    }

        //    return response;
        //}


        // ---------------------------------------------------------------------- NEWSLETTER REGISTRATION
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string newsletterregister(string email)
        {
            string response = "";



            return response;
        }
    }
}
