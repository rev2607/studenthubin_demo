using StudentHubMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Repositories
{
    public class RepResultsPolycet
    {
        readonly Liason.Help obj_Help = new Liason.Help();
        readonly Entities db = new Entities();
        readonly Liason.DBConnect _dbConnect = new Liason.DBConnect();
        readonly RepResults _repResults = new RepResults();

        readonly string ConfigTableSheetName = ConfigurationManager.AppSettings["TableSheetName"];
        readonly string ConfigResultsDoNotDeletePath = ConfigurationManager.AppSettings["ResultsDoNotDeletePath"];
        readonly bool TestModeON = ConfigurationManager.AppSettings["TestMode"] != null && ConfigurationManager.AppSettings["TestMode"].ToString() == "ON" ? true : false;


        #region ------------------------------------------------------------------------------------ HALL TICKET

        public TSPolycet GetTSPolycetResult(string HallTicketNumber, string ExamYear)
        {
            TSPolycet result = new TSPolycet();

            if (TestModeON)
                return TSPolycetSampleData();

            var fileLog = _repResults.GetLatestExamTypeLog((int)Enums_ExamTypes.TS_POLYCET_16, ExamYear);

            if (fileLog != null && !string.IsNullOrEmpty(fileLog.upld_FileName))
            {
                string path = HttpContext.Current.Server.MapPath(ConfigResultsDoNotDeletePath);

                DataTable dt = _dbConnect.GetMSAccessData(path, fileLog.upld_FileName, ConfigTableSheetName, "HTNO", HallTicketNumber);

                if (dt != null && dt.Rows.Count == 1)
                {
                    result.IsResultSet = true;
                    result.HallTicketNumber = dt.Columns.Contains("HTNO") ? dt.Rows[0]["HTNO"].ToString() : "";
                    result.FullName = dt.Columns.Contains("CNAME") ? dt.Rows[0]["CNAME"].ToString() : "";
                    result.Maths = dt.Columns.Contains("MATHS") ? dt.Rows[0]["MATHS"].ToString() : "";
                    result.Physics = dt.Columns.Contains("PHYSICS") ? dt.Rows[0]["PHYSICS"].ToString() : "";
                    result.Chemistry = dt.Columns.Contains("CHEMISTRY") ? dt.Rows[0]["CHEMISTRY"].ToString() : "";
                    result.Biology = dt.Columns.Contains("BIOLOGY") ? dt.Rows[0]["BIOLOGY"].ToString() : "";
                    result.MPCTotal = dt.Columns.Contains("MPC_TOTAL") ? dt.Rows[0]["MPC_TOTAL"].ToString() : "";
                    result.MPCStatus = dt.Columns.Contains("MPC_PASSTAG") ? dt.Rows[0]["MPC_PASSTAG"].ToString() : "";
                    result.MPCRank = dt.Columns.Contains("MPC_RANK") ? dt.Rows[0]["MPC_RANK"].ToString() : "";
                    result.MBiPCTotal = dt.Columns.Contains("MBIPC_TOTAL") ? dt.Rows[0]["MBIPC_TOTAL"].ToString() : "";
                    result.MBiPCStatus = dt.Columns.Contains("MBIPC_PASSTAG") ? dt.Rows[0]["MBIPC_PASSTAG"].ToString() : "";
                    result.MBiPCRank = dt.Columns.Contains("MBIPC_RANK") ? dt.Rows[0]["MBIPC_RANK"].ToString() : "";

                    result.Exam = string.Format("{0} - {1}", _repResults.GetResultsExamTypeById(Constants_ExamIds.TSPOLYCET_TSPOLYCET).corseTyp_Name, ExamYear);
                }
            }

            return result;
        }

        public APPolycet GetAPPolycetResult(string HallTicketNumber, string ExamYear)
        {
            APPolycet result = new APPolycet();

            if (TestModeON)
                return APPolycetSampleData();

            var fileLog = _repResults.GetLatestExamTypeLog((int)Enums_ExamTypes.AP_POLYCET_15, ExamYear);

            if (fileLog != null && !string.IsNullOrEmpty(fileLog.upld_FileName))
            {
                string path = HttpContext.Current.Server.MapPath(ConfigResultsDoNotDeletePath);

                DataTable dt = _dbConnect.GetMSAccessData(path, fileLog.upld_FileName, ConfigTableSheetName, "HTNO", HallTicketNumber);

                if (dt != null && dt.Rows.Count == 1)
                {
                    result.IsResultSet = true;
                    result.HallTicketNumber = dt.Columns.Contains("HTNO") ? dt.Rows[0]["HTNO"].ToString() : "";
                    result.FullName = dt.Columns.Contains("CNAME") ? dt.Rows[0]["CNAME"].ToString() : "";
                    result.Maths = dt.Columns.Contains("MATHS") ? dt.Rows[0]["MATHS"].ToString() : "";
                    result.Physics = dt.Columns.Contains("PHYSICS") ? dt.Rows[0]["PHYSICS"].ToString() : "";
                    result.Chemistry = dt.Columns.Contains("CHEMISTRY") ? dt.Rows[0]["CHEMISTRY"].ToString() : "";
                    result.Total = dt.Columns.Contains("TOTAL") ? dt.Rows[0]["TOTAL"].ToString() : "";
                    result.Status = dt.Columns.Contains("PASSTAG") ? dt.Rows[0]["PASSTAG"].ToString() : "";
                    result.Rank = dt.Columns.Contains("RNK") ? dt.Rows[0]["RNK"].ToString() : "";

                    result.Exam = string.Format("{0} - {1}", _repResults.GetResultsExamTypeById(Constants_ExamIds.APPOLYCET_APPOLYCET).corseTyp_Name, ExamYear);
                }
            }

            return result;
        }


        #endregion


        #region ------------------------------------------------------------------------------------ NAME

        public List<ExamResultsNameSearchResult> GetTSPolycetResultNameSearch(string Name, string ExamYear)
        {
            return _repResults.GetNameSearchResults(
                Constants_FileFields.Polycet_FullName_TS,
                Name,
                (int)Enums_ExamTypes.TS_POLYCET_16,
                ExamYear,
                Constants_FileFields.Polycet_FullName_TS,
                Constants_FileFields.Polycet_HallTicket_TS,
                Constants_ExamIds.TSPOLYCET_TSPOLYCET);
        }

        public List<ExamResultsNameSearchResult> GetAPPolycetResultNameSearch(string Name, string ExamYear)
        {
            return _repResults.GetNameSearchResults(
                Constants_FileFields.Polycet_FullName_AP,
                Name,
                (int)Enums_ExamTypes.AP_POLYCET_15,
                ExamYear,
                Constants_FileFields.Polycet_FullName_AP,
                Constants_FileFields.Polycet_HallTicket_AP,
                Constants_ExamIds.APPOLYCET_APPOLYCET);
        }


        #endregion


        #region ------------------------------------------------------------------------------------------------- TEST DATA

        public TSPolycet TSPolycetSampleData()
        {
            var result = new TSPolycet()
            {
                Exam = "Telangana POLYCET 2021",
                IsResultSet = true,
                HallTicketNumber = "4005014",
                FullName = "DORNALA PRANATHI",
                Maths = "38",
                Physics = "9",
                Chemistry = "8",
                Biology = "0",
                MPCTotal = "55",
                MPCStatus = "Y",
                MPCRank = "12263",
                MBiPCTotal = "36",
                MBiPCStatus = "Y",
                MBiPCRank = "59863"
            };

            return result;
        }

        public APPolycet APPolycetSampleData()
        {
            var result = new APPolycet()
            {
                Exam = "Andhra Pradesh POLYCET 2021",
                IsResultSet = true,
                HallTicketNumber = "2611218",
                FullName = "JERRIPOTHULA GUNATHMINI",
                Maths = "49",
                Physics = "35",
                Chemistry = "25",
                Total = "109",
                Status = "Y",
                Rank = "632"
            };

            return result;
        }

        #endregion
    }
}