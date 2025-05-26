using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace StudentHubMVC
{
    public enum Enums_ExamTypes
    {
        Telangana_Intermediate_Year_2_Regular = 1,
        Telangana_Intermediate_Year_2_Vocational = 2,
        Telangana_Intermediate_Year_1_Regular = 3,
        Telangana_Intermediate_Year_1_Vocational = 4,
        Andhra_Pradesh_SSC = 5,
        Telangana_SSC = 6,
        Andhra_Pradesh_Intermediate_Year_1_Regular = 7,
        Andhra_Pradesh_Intermediate_Year_1_Vocational = 8,
        Andhra_Pradesh_Intermediate_Year_2_Regular = 9,
        Andhra_Pradesh_Intermediate_Year_2_Vocational = 10,
        Andhra_Pradesh_EAPCET_Engineering_11 = 11,
        Andhra_Pradesh_EAPCET_AgricultureMedical_12 = 12,
        Telangana_EAMCET_Engineering_13 = 13,
        Telangana_EAMCET_AgricultureMedical_14 = 14,
        AP_POLYCET_15 = 15,
        TS_POLYCET_16 = 16,
        AP_ICET_17 = 17,
        TS_ICET_18 = 18
    }

    public enum Enum_States
    {
        Telangana = 1,
        AndhraPradesh = 2
    }

    public static class Constants_General
    {
        public static List<string> listUploadAllowedFileTypes = new List<string> { ".txt", ".mdb", ".accdb", ".xls", ".xlsx", ".zip" };
    }

    public static class Constants_ExamIds
    {
        public const string TelanganaIntermediateYear1Regular_TSINTR1REG = "TSINTR1REG";
        public const string TelanganaIntermediateYear1Vocational_TSINTR1VOC = "TSINTR1VOC";
        public const string TelanganaIntermediateYear2Regular_TSINTR2REG = "TSINTR2REG";
        public const string TelanganaIntermediateYear2Vocational_TSINTR2VOC = "TSINTR2VOC";
        public const string AndhraPradeshSSC_APSSC = "APSSC";
        public const string TelanganaSSC_TSSSC = "TSSSC";
        public const string AndhraPradeshIntermediateYear1Regular_APINTR1REG = "APINTR1REG";
        public const string AndhraPradeshIntermediateYear1Vocational_APINTR1VOC = "APINTR1VOC";
        public const string AndhraPradeshIntermediateYear2Regular_APINTR2REG = "APINTR2REG";
        public const string AndhraPradeshIntermediateYear2Vocational_APINTR2VOC = "APINTR2VOC";
        public const string AndhraPradeshEAPCETEngineering_APEAPENG = "APEAPENG";
        public const string AndhraPradeshEAPCETAgricultureMedical_APEAPAM = "APEAPAM";
        public const string TelanganaEAMCETEngineering_TSEAMENG = "TSEAMENG";
        public const string TelanganaEAMCETAgricultureMedical_TSEAMAM = "TSEAMAM";
        public const string APPOLYCET_APPOLYCET = "APPOLYCET";
        public const string TSPOLYCET_TSPOLYCET = "TSPOLYCET";
        public const string APICET_APICET = "APICET";
        public const string TSICET_TSICET = "TSICET";
    }

    public static class Constants_States
    {
        public static string Telangana = "Telangana";
        public static string AndhraPradesh = "Andhra Pradesh";
    }

    public static class Constants_EducationTypes
    {
        public static string BoardExamination = "Board Examination";
        public static string UniversityExaminationUG = "University Examination - UG";
        public static string UniversityExaminationPG = "University Examination - PG";
        public static string EntranceExamination = "Entrance Examination";
    }

    public static class Constants_SearchTypes
    {
        public static string HallTicket = "HallTicket";
        public static string Name = "Name";
    }

    public static class Constants_FileUploadStatus
    {
        public static string New = "NEW";
        public static string Done = "DONE";
        public static string Incomplete = "INCOMPLETE";
        public static string Old = "OLD";
    }

    public static class Constants_FileFields
    {
        #region SSC 

        public static string SSC_HallTicket_AP = "HTNO";
        public static string SSC_FullName_AP = "CNAME";

        public static string SSC_HallTicket_TS = "HTNO";
        public static string SSC_FullName_TS = "CNAME";

        #endregion


        #region EAMCET/EAPCET

        public static string EamcetEng_HallTicket_TS = "HTNO";
        public static string EamcetEng_FullName_TS = "CNAME";

        public static string EamcetAM_HallTicket_TS = "HTNO";
        public static string EamcetAM_FullName_TS = "CNAME";

        public static string EapcetEng_HallTicket_AP = "HTNO";
        public static string EapcetEng_FullName_AP = "CNAME";

        public static string EapcetAM_HallTicket_AP = "HTNO";
        public static string EapcetAM_FullName_AP = "CNAME";

        #endregion


        #region POLYCET

        public static string Polycet_HallTicket_AP = "HTNO";
        public static string Polycet_FullName_AP = "CNAME";

        public static string Polycet_HallTicket_TS = "HTNO";
        public static string Polycet_FullName_TS = "CNAME";

        #endregion


        #region ICET

        public static string Icet_HallTicket_AP = "HTNO";
        public static string Icet_FullName_AP = "CNAME";

        public static string Icet_HallTicket_TS = "HTNO";
        public static string Icet_FullName_TS = "CNAME";

        #endregion
    }

    public static class Constants_ConfigVars
    {
        public static string ConfigTableSheetName = ConfigurationManager.AppSettings["TableSheetName"];
        public static string ConfigResultsDoNotDeletePath = ConfigurationManager.AppSettings["ResultsDoNotDeletePath"];
        public static string ConfigResultsStartYear = ConfigurationManager.AppSettings["ResultsStartYear"];
        public static int ConfigMaximumResultsCount = int.Parse(ConfigurationManager.AppSettings["MaximumResultsCount"]);
    }

    
    #region ------------------------------------------------------------------------------------------- USERS



    #endregion
}