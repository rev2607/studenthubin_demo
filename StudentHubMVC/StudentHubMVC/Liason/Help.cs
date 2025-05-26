using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO.Compression;
using System.IO;

namespace StudentHubMVC.Liason
{
    public class Help
    {
        DBConnect obj_dbConnect = new DBConnect();


        // ---------------------------------------------------------------- STRING LIST TO DATATABLE
        public DataTable ConvertListToDataTable(List<string> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }


        // ---------------------------------------------------------------- RANDOM ALPHANUMERIC STRING
        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        // ----------------------------------------------------------------- EMAIL
        public string send_email(string subject, string body, string ToEmail, string mailer_name)
        {
            string response = "";

            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["Host"], int.Parse(ConfigurationManager.AppSettings["Port"]));
            client.EnableSsl = false; // client.EnableSsl = true; // changed due to Godaddy restrictions of only using the email account hosted in godaddy only
            MailMessage message = new MailMessage(ConfigurationManager.AppSettings["StoreMail"], ToEmail);
            message.Headers.Add("Reply-To", ConfigurationManager.AppSettings["StoreMail"]);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            NetworkCredential myCreds = new NetworkCredential(ConfigurationManager.AppSettings["EmailUsername"], ConfigurationManager.AppSettings["EmailPassword"], "");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            //client.Credentials = myCreds;

            try
            {
                client.Send(message);
                response = "SENT";
            }

            catch (Exception ex)
            {
                response = "ERROR";
                ExcpetionsHandling(ex);
            }

            finally
            {
                client.Dispose();
            }

            return response;
        }


        // ------------------------------------------------------------------ UPLOAD FILE
        public string uploadfile(string savePath, HttpPostedFileBase file)
        {
            string myfile = "";

            try
            {
                if (file != null && file.FileName != "")
                {
                    string name = Path.GetFileNameWithoutExtension(file.FileName); //getting file name without extension 
                    string ext = Path.GetExtension(file.FileName);
                    myfile = name + "_" + DateTime.Now.ToString("yyyyMMddhhmm") + ext; //appending the name with id

                    var path = Path.Combine(savePath, myfile);

                    file.SaveAs(path);
                }
            }
            catch (Exception ex)
            {
                myfile = "ERROR";
                ExcpetionsHandling(ex);
            }

            return myfile;
        }

        public string extractUploadZipTxtFile(string savePath, HttpPostedFileBase file)
        {
            string myfile = "";
            ZipArchive archive = new ZipArchive(file.InputStream);

            if (archive.Entries.Count == 1 && (Constants_General.listUploadAllowedFileTypes.Contains(Path.GetExtension(archive.Entries[0].Name).ToLower())))
            {
                try
                {
                    ZipArchiveEntry entry = archive.Entries[0];
                    string name = Path.GetFileNameWithoutExtension(entry.FullName); //getting file name without extension 
                    string ext = Path.GetExtension(entry.FullName);
                    myfile = name + "_" + DateTime.Now.ToString("yyyyMMddhhmm") + ext;

                    entry.ExtractToFile(Path.Combine(savePath, myfile));
                }
                catch (Exception ex)
                {
                    myfile = "ERROR";
                    ExcpetionsHandling(ex);
                }
            }
            else
                myfile = "ERROR";

            return myfile;
        }

        // ---------------------------------------------------------------- EXCEPTIONS
        public void ExcpetionsHandling(Exception ex)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            string InnerException = "";
            if (ex.InnerException != null)
                InnerException = ex.InnerException.ToString();
            obj_dbConnect.exceptions_final(trace, ex.Message.ToString() + " - " + InnerException);
        }

    }
}