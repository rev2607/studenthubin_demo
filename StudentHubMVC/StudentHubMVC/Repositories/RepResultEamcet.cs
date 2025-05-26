using StudentHubMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Repositories
{
    public class RepResultEamcet
    {
        readonly RepResults _repResults = new RepResults();
        readonly Entities db = new Entities();
        readonly Liason.DBConnect _dbConnect = new Liason.DBConnect();

        readonly bool TestModeON = ConfigurationManager.AppSettings["TestMode"] != null && ConfigurationManager.AppSettings["TestMode"].ToString() == "ON" ? true : false;


        #region -------------------------------------------------------------------------- HALLTICKET

        public APEapcetEng GetAPEapcetEngineeringResult(string HallTicketNumber, string ExamYear)
        {
            var result = TestModeON ? APEapcetEngSampleData() : _repResults.GetAPEapcetEngResult(HallTicketNumber, ExamYear);

            return result;
        }

        public APEapcetAM GetAPEapcetAgriMedicalResult(string HallTicketNumber, string ExamYear)
        {
            var result = TestModeON ? APEapcetAMSampleData() : _repResults.GetAPEapcetAMResult(HallTicketNumber, ExamYear);

            return result;
        }

        public TSEamcetEng GetTSEamcetEngineeringResult(string HallTicketNumber, string ExamYear)
        {
            var result = TestModeON ? TSEamcetEngSampleData() : _repResults.GetTSEamcetEngResult(HallTicketNumber, ExamYear);

            return result;
        }

        public TSEamcetAM GetTSEamcetAgriMedicalResult(string HallTicketNumber, string ExamYear)
        {
            var result = TestModeON ? TSEamcetAMSampleData() : _repResults.GetTSEamcetAMResult(HallTicketNumber, ExamYear);

            return result;
        }

        #endregion

        #region -------------------------------------------------------------------------- NAME

        public List<ExamResultsNameSearchResult> GetAPEapcetEngineeringResultNameSearch(string Name, string ExamYear)
        {
            return _repResults.GetNameSearchResults(
                Constants_FileFields.EapcetEng_FullName_AP,
                Name,
                (int)Enums_ExamTypes.Andhra_Pradesh_EAPCET_Engineering_11,
                ExamYear,
                Constants_FileFields.EapcetEng_FullName_AP,
                Constants_FileFields.EapcetEng_HallTicket_AP,
                Constants_ExamIds.AndhraPradeshEAPCETEngineering_APEAPENG);
        }

        public List<ExamResultsNameSearchResult> GetAPEapcetAgriMedicalResultNameSearch(string Name, string ExamYear)
        {
            return _repResults.GetNameSearchResults(
                Constants_FileFields.EapcetAM_FullName_AP,
                Name,
                (int)Enums_ExamTypes.Andhra_Pradesh_EAPCET_AgricultureMedical_12,
                ExamYear,
                Constants_FileFields.EapcetAM_FullName_AP,
                Constants_FileFields.EapcetAM_HallTicket_AP,
                Constants_ExamIds.AndhraPradeshEAPCETAgricultureMedical_APEAPAM);
        }

        public List<ExamResultsNameSearchResult> GetTSEamcetEngineeringNameSearch(string Name, string ExamYear)
        {
            return _repResults.GetNameSearchResults(
                Constants_FileFields.EamcetEng_FullName_TS,
                Name,
                (int)Enums_ExamTypes.Telangana_EAMCET_Engineering_13,
                ExamYear,
                Constants_FileFields.EamcetEng_FullName_TS,
                Constants_FileFields.EamcetEng_HallTicket_TS,
                Constants_ExamIds.TelanganaEAMCETEngineering_TSEAMENG);
        }

        public List<ExamResultsNameSearchResult> GetTSEamcetAgriMedicalResultNameSearch(string Name, string ExamYear)
        {
            return _repResults.GetNameSearchResults(
                Constants_FileFields.EamcetAM_FullName_TS,
                Name,
                (int)Enums_ExamTypes.Telangana_EAMCET_AgricultureMedical_14,
                ExamYear,
                Constants_FileFields.EamcetAM_FullName_TS,
                Constants_FileFields.EamcetAM_HallTicket_TS,
                Constants_ExamIds.TelanganaEAMCETAgricultureMedical_TSEAMAM);
        }

        #endregion

        #region ------------------------------------------------------------------------------------------------- TEST DATA

        private APEapcetEng APEapcetEngSampleData()
        {
            var result = new APEapcetEng()
            {
                Exam = "Andhra Pradesh EAPCET Engineering 2021",
                IsResultSet = true,
                HallTicketNumber = "50151010021",
                FullName = "KARAMTHOAT GOWTHAMI",
                FathersName = "KARAMTHOAT GANGADHAR NAIK",
                MothersName = "KARAMTHOAT JAYALAKSHMI",
                Mathematics = "25.306",
                Physics = "9.8397…",
                Chemistry = "13.4852…",
                Total = "48.6314…",
                Rank = "63401",
                Status = "Qualified"
            };

            return result;
        }

        private APEapcetAM APEapcetAMSampleData()
        {
            var result = new APEapcetAM()
            {
                Exam = "Andhra Pradesh EAPCET Agriculture/Medical 2021",
                IsResultSet = true,
                HallTicketNumber = "91151010019",
                FullName = "VANNUR VARUN KUMAR",
                FathersName = "VANNUR VANNURAPPA",
                MothersName = "VANNUR VARALAKSHMI",
                Botany = "11.1677",
                Zoology = "13.9782…",
                Physics = "16.0531…",
                Chemistry = "13.7558…",
                Total = "54.9550…",
                Rank = "35211",
                Status = "Qualified"
            };

            return result;
        }

        private TSEamcetEng TSEamcetEngSampleData()
        {
            var result = new TSEamcetEng()
            {
                Exam = "Telangana EAMCET Engineering 2021",
                IsResultSet = true,
                HallTicketNumber = "2124D07084",
                FullName = "SANDA PREETHAM SAGAR",
                FathersName = "SANDA JAYANTH KUMAR",
                MothersName = "",
                Gender = "M",
                LocalArea = "Osmania University (OU)",
                Category = "BC_D",
                Maths = "31.493965",
                Physics = "21.18303",
                Chemistry = "10.465969",
                TotalMarks = "63.142964",
                Rank = "14937",
                Result = "QUALIFIED IN TSEAMCET-2021"
            };

            return result;
        }

        private TSEamcetAM TSEamcetAMSampleData()
        {
            var result = new TSEamcetAM()
            {
                Exam = "Telangana EAMCET Agriculture/Medical 2021",
                IsResultSet = true,
                HallTicketNumber = "2111R01008",
                FullName = "MOKALLA APARNA",
                FathersName = "MOKALLA SRINU",
                MothersName = "",
                Gender = "F",
                LocalArea = "Andhra University (AU)",
                Category = "SC",
                Botany = "14.487948",
                Zoology = "16.682294",
                Physics = "14.941877",
                Chemistry = "8.810312",
                TotalMarks = "54.922431",
                Rank = "32590",
                Result = "QUALIFIED IN TSEAMCET-2021"
            };

            return result;
        }

        #endregion
    }
}