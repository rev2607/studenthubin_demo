using StudentHubMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace StudentHubMVC.Repositories
{
    public class RepResultSsc
    {
        readonly RepResults _repResults = new RepResults();
        readonly Entities db = new Entities();
        readonly Liason.DBConnect _dbConnect = new Liason.DBConnect();

        readonly bool TestModeON = ConfigurationManager.AppSettings["TestMode"] != null && ConfigurationManager.AppSettings["TestMode"].ToString() == "ON" ? true : false;


        #region -------------------------------------------------------------------------- HALLTICKET

        public APSSCResults GetAPSSCResult(string HallTicketNumber, string ExamYear)
        {
            var result = TestModeON ? APSSCTestData() : _repResults.GetAPSSCResult(HallTicketNumber, ExamYear);

            return result;
        }

        public TSSSCResults GetTSSSCResult(string HallTicketNumber, string ExamYear)
        {
            var result = TestModeON ? TSSSCTestData() : _repResults.GetTSSSCResult(HallTicketNumber, ExamYear);

            return result;
        }

        #endregion

        #region -------------------------------------------------------------------------- NAME

        public List<ExamResultsNameSearchResult> GetAPSSCResultNameSearch(string Name, string ExamYear)
        {
            return _repResults.GetNameSearchResults(
                Constants_FileFields.SSC_FullName_AP, 
                Name, 
                (int)Enums_ExamTypes.Andhra_Pradesh_SSC, 
                ExamYear, 
                Constants_FileFields.SSC_FullName_AP, 
                Constants_FileFields.SSC_HallTicket_AP,
                Constants_ExamIds.AndhraPradeshSSC_APSSC);
        }

        public List<ExamResultsNameSearchResult> GetTSSSCResultNameSearch(string Name, string ExamYear)
        {
            return _repResults.GetNameSearchResults(
                Constants_FileFields.SSC_FullName_TS,
                Name,
                (int)Enums_ExamTypes.Telangana_SSC,
                ExamYear,
                Constants_FileFields.SSC_FullName_TS,
                Constants_FileFields.SSC_HallTicket_TS,
                Constants_ExamIds.TelanganaSSC_TSSSC);
        }

        #endregion

        #region ------------------------------------------------------------------------------------------------- TEST DATA

        private APSSCResults APSSCTestData()
        {
            var result = new APSSCResults()
            {
                Exam = "Andhra Pradesh SSC 2021",
                IsResultSet = true,
                District = "05",
                SchoolCode = "04001",
                RollNo = "1905142745",
                FullName = "GUJJU SWATHI",
                PHDescription = "",
                Language1Flag = "",
                Language1InternalGrade = "A1",
                Language1Grade1 = "A2",
                Language1FinalGrade = "A2",
                Language1Points = "09",
                Language2Flag = "",
                Language2InternalGrade = "A1",
                Language2Grade1 = "A2",
                Language2FinalGrade = "A2",
                Language2Points = "09",
                Language3Flag = "",
                Language3InternalGrade = "B2",
                Language3Grade1 = "E",
                Language3FinalGrade = "E",
                Language3Points = "",
                MathsFlag = "*",
                MathsInternalGrade = "C2",
                MathsGrade1 = "C2",
                MathsFinalGrade = "C2",
                MathsPoints = "05",
                ScienceFlag = "*",
                ScienceInternalGrade = "B2",
                ScienceGrade1 = "D",
                ScienceFinalGrade = "C2",
                SciencePoints = "05",
                SocialFlag = "*",
                SocialInternalGrade = "C1",
                SocialGrade1 = "C1",
                SocialFinalGrade = "C1",
                SocialPoints = "06",
                GPA = "9.0",
                Result = "PASSED",
                OptionalLanguageFlag = "",
                OptionalLanguageInternalGrade = "",
                OptionalLanguageGrade1 = "",
                OptionalLanguageFinalGrade = "",
                OptionalLanguagePoints = "",
                ValueEducationLifeSkillsGrade = "A+",
                ArtCulturalEducationGrade = "A",
                WorkComputerEducationGrade = "A+",
                PhysicalHealthEducationGrade = "A+",
                DistrictName = "SRIKAKULAM",
                Message = "",
            };

            return result;
        }

        private TSSSCResults TSSSCTestData()
        {
            var result = new TSSSCResults()
            {
                Exam = "Telangana SSC 2019",
                IsResultSet = true,
                District = "05",
                SchoolCode = "04001",
                RollNo = "1905200697",
                FullName = "KADIYALA ASHOK",
                PHDescription = "",
                Language1Flag = "*",
                Language1InternalGrade = "B2",
                Language1Grade1 = "C2",
                Language1FinalGrade = "C1",
                Language1Points = "06",
                Language2Flag = "*",
                Language2InternalGrade = "C1",
                Language2Grade1 = "C1",
                Language2FinalGrade = "C1",
                Language2Points = "06",
                Language3Flag = "",
                Language3InternalGrade = "B2",
                Language3Grade1 = "E",
                Language3FinalGrade = "E",
                Language3Points = "",
                MathsFlag = "*",
                MathsInternalGrade = "C2",
                MathsGrade1 = "C2",
                MathsFinalGrade = "C2",
                MathsPoints = "05",
                ScienceFlag = "*",
                ScienceInternalGrade = "B2",
                ScienceGrade1 = "D",
                ScienceFinalGrade = "C2",
                SciencePoints = "05",
                SocialFlag = "*",
                SocialInternalGrade = "C1",
                SocialGrade1 = "C1",
                SocialFinalGrade = "C1",
                SocialPoints = "06",
                GPA = "3.2",
                Result = "FAIL",
                OptionalLanguageFlag = "",
                OptionalLanguageInternalGrade = "",
                OptionalLanguageGrade1 = "",
                OptionalLanguageFinalGrade = "",
                OptionalLanguagePoints = "",
                ValueEducationLifeSkillsGrade = "B",
                ValueEducationLifeSkillsPoints = "5.0",
                ArtCulturalEducationGrade = "A",
                ArtCulturalEducationPoints = "6.0",
                WorkComputerEducationGrade = "C",
                WorkComputerEducationPoints = "5.0",
                PhysicalHealthEducationGrade = "B+",
                PhysicalHealthEducationPoints = "6.5",
                DistrictName = "NALGONDA",
                Message = "",
            };

            return result;
        }

        #endregion
    }
}