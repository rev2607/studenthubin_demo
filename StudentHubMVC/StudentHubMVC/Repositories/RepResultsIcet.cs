using StudentHubMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;

namespace StudentHubMVC.Repositories
{
    public class RepResultsIcet
    {
        readonly Liason.Help obj_Help = new Liason.Help();
        readonly Entities db = new Entities();
        readonly Liason.DBConnect _dbConnect = new Liason.DBConnect();
        readonly RepResults _repResults = new RepResults();

        readonly string ConfigTableSheetName = ConfigurationManager.AppSettings["TableSheetName"];
        readonly string ConfigResultsDoNotDeletePath = ConfigurationManager.AppSettings["ResultsDoNotDeletePath"];
        readonly bool TestModeON = ConfigurationManager.AppSettings["TestMode"] != null && ConfigurationManager.AppSettings["TestMode"].ToString() == "ON" ? true : false;


        #region ----------------------------------------------------------------------- HALLTICKET

        public TSIcet GetTSIcetResult(string HallTicketNumber, string ExamYear)
        {
            TSIcet result = new TSIcet();

            if (TestModeON)
                return TSIcetSampleData();

            var fileLog = _repResults.GetLatestExamTypeLog((int)Enums_ExamTypes.TS_ICET_18, ExamYear);

            if (fileLog != null && !string.IsNullOrEmpty(fileLog.upld_FileName))
            {
                string path = HttpContext.Current.Server.MapPath(ConfigResultsDoNotDeletePath);

                DataTable dt = _dbConnect.GetMSAccessData(path, fileLog.upld_FileName, ConfigTableSheetName, "HTNO", HallTicketNumber);

                if (dt != null && dt.Rows.Count == 1)
                {
                    result.IsResultSet = true;
                    result.HallTicketNumber = dt.Columns.Contains("HTNO") ? dt.Rows[0]["HTNO"].ToString() : "";
                    result.FullName = dt.Columns.Contains("CNAME") ? dt.Rows[0]["CNAME"].ToString() : "";
                    result.FatherName = dt.Columns.Contains("FNAME") ? dt.Rows[0]["FNAME"].ToString() : "";
                    result.Category = dt.Columns.Contains("CATEGORY_NAME") ? dt.Rows[0]["CATEGORY_NAME"].ToString() : "";
                    result.Total = dt.Columns.Contains("TOTAL_MARKS") ? dt.Rows[0]["TOTAL_MARKS"].ToString() : "";
                    result.SecA = dt.Columns.Contains("SEC_A") ? dt.Rows[0]["SEC_A"].ToString() : "";
                    result.SecB = dt.Columns.Contains("SEC_B") ? dt.Rows[0]["SEC_B"].ToString() : "";
                    result.SecC = dt.Columns.Contains("SEC_C") ? dt.Rows[0]["SEC_C"].ToString() : "";
                    result.Rank = dt.Columns.Contains("ICET_RANK") ? dt.Rows[0]["ICET_RANK"].ToString() : "";
                    result.Status = dt.Columns.Contains("RESULT_STATUS") ? dt.Rows[0]["RESULT_STATUS"].ToString() : "";

                    result.Exam = string.Format("{0} - {1}", _repResults.GetResultsExamTypeById(Constants_ExamIds.TSICET_TSICET).corseTyp_Name, ExamYear);
                }
            }

            return result;
        }

        public APIcet GetAPIcetResult(string HallTicketNumber, string ExamYear)
        {
            APIcet result = new APIcet();

            if (TestModeON)
                return APIcetSampleData();

            var fileLog = _repResults.GetLatestExamTypeLog((int)Enums_ExamTypes.AP_ICET_17, ExamYear);

            if (fileLog != null && !string.IsNullOrEmpty(fileLog.upld_FileName))
            {
                string path = HttpContext.Current.Server.MapPath(ConfigResultsDoNotDeletePath);

                DataTable dt = _dbConnect.GetMSAccessData(path, fileLog.upld_FileName, ConfigTableSheetName, "HTNO", HallTicketNumber);

                if (dt != null && dt.Rows.Count == 1)
                {
                    result.IsResultSet = true;
                    result.HallTicketNumber = dt.Columns.Contains("HTNO") ? dt.Rows[0]["HTNO"].ToString() : "";
                    result.FullName = dt.Columns.Contains("CNAME") ? dt.Rows[0]["CNAME"].ToString() : "";
                    result.Total = dt.Columns.Contains("TOTAL_MARKS") ? dt.Rows[0]["TOTAL_MARKS"].ToString() : "";
                    result.SecA = dt.Columns.Contains("SECTION_A_TOTAL") ? dt.Rows[0]["SECTION_A_TOTAL"].ToString() : "";
                    result.SecB = dt.Columns.Contains("SECTION_B_TOTAL") ? dt.Rows[0]["SECTION_B_TOTAL"].ToString() : "";
                    result.SecC = dt.Columns.Contains("SECTION_C_TOTAL") ? dt.Rows[0]["SECTION_C_TOTAL"].ToString() : "";
                    result.Rank = dt.Columns.Contains("RANK") ? dt.Rows[0]["RANK"].ToString() : "";

                    result.Exam = string.Format("{0} - {1}", _repResults.GetResultsExamTypeById(Constants_ExamIds.APICET_APICET).corseTyp_Name, ExamYear);
                }
            }

            return result;
        }


        #endregion


        #region ----------------------------------------------------------------------- NAME

        public List<ExamResultsNameSearchResult> GetTSIcetResultNameSearch(string Name, string ExamYear)
        {
            return _repResults.GetNameSearchResults(
                Constants_FileFields.Icet_FullName_TS,
                Name,
                (int)Enums_ExamTypes.TS_ICET_18,
                ExamYear,
                Constants_FileFields.Icet_FullName_TS,
                Constants_FileFields.Icet_HallTicket_TS,
                Constants_ExamIds.TSICET_TSICET);
        }

        public List<ExamResultsNameSearchResult> GetAPIcetResultNameSearch(string Name, string ExamYear)
        {
            return _repResults.GetNameSearchResults(
                Constants_FileFields.Icet_FullName_AP,
                Name,
                (int)Enums_ExamTypes.AP_ICET_17,
                ExamYear,
                Constants_FileFields.Icet_FullName_AP,
                Constants_FileFields.Icet_HallTicket_AP,
                Constants_ExamIds.APICET_APICET);
        }


        #endregion


        #region ------------------------------------------------------------------------------------------------- TEST DATA

        public TSIcet TSIcetSampleData()
        {
            var result = new TSIcet()
            {
                Exam = "Telangana ICET 2021",
                IsResultSet = true,
                HallTicketNumber = "2127901038",
                FullName = "DYAVANAPALLY RAMPRASAD",
                FatherName = "DYAVANAPALLY LAXMAN",
                Category = "BC_B",
                Total = "37.718231",
                SecA = "11.550888",
                SecB = "11.080694",
                SecC = "15.086649",
                Rank = "23369",
                Status = "NOT QUALIFIED IN TS ICET-2021"
            };

            return result;
        }

        public APIcet APIcetSampleData()
        {
            var result = new APIcet()
            {
                Exam = "Andhra Pradesh ICET 2020",
                IsResultSet = true,
                HallTicketNumber = "2256020911",
                FullName = "ADIGERLA SIVAKUMAR",
                Total = "96.6519",
                SecA = "55.9149",
                SecB = "21.2543",
                SecC = "19.4826",
                Rank = "927"
            };

            return result;
        }

        #endregion
    }
}