using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels
{
    public class VMResultsTsInterMaster
    {
        public VMResultsTsInterMaster()
        {
            ResultsExamTypesList = new List<rslt_CourseTypes>();
            InterYear2RegularResults = new List<rslt_IntermediateYear2Regular>();
            TSYr2RegResult = new TSYr2RegResult();
            TSYr2VocResult = new TSYr2VocResult();
            TSEamcetAMResult = new TSEamcetAM();
            TSEamcetEngResult = new TSEamcetEng();
            TSPolycetResult = new TSPolycet();
            TSIcetResult = new TSIcet();
            TSSSCResults = new TSSSCResults();
        }

        public bool IsPostRequest { get; set; }
        public List<rslt_IntermediateYear2Regular> InterYear2RegularResults { get; set; }
        public List<rslt_CourseTypes> ResultsExamTypesList { get; set; }
        public TSYr2RegResult TSYr2RegResult { get; set; }
        public TSYr2VocResult TSYr2VocResult { get; set; }
        public TSEamcetEng TSEamcetEngResult { get; set; }
        public TSEamcetAM TSEamcetAMResult { get; set; }
        public TSPolycet TSPolycetResult { get; set; }
        public TSIcet TSIcetResult { get; set; }
        public TSSSCResults TSSSCResults { get; set; }

    }
    public class VMResultsApInterMaster
    {
        public VMResultsApInterMaster()
        {
            ResultsExamTypesList = new List<rslt_CourseTypes>();
            APRegResult = new TSYr2RegResult();
            APVocResult = new TSYr2VocResult();
            APSSCResult = new APSSCResults();
            APEapcetAMResult = new APEapcetAM();
            APEapcetEngResult = new APEapcetEng();
            APPolycetResult = new APPolycet();
            APIcetResult = new APIcet();
        }

        public bool IsPostRequest { get; set; }
        public List<rslt_CourseTypes> ResultsExamTypesList { get; set; }
        public TSYr2RegResult APRegResult { get; set; }
        public TSYr2VocResult APVocResult { get; set; }
        public APSSCResults APSSCResult { get; set; }
        public APEapcetEng APEapcetEngResult { get; set; }
        public APEapcetAM APEapcetAMResult { get; set; }
        public APPolycet APPolycetResult { get; set; }
        public APIcet APIcetResult { get; set; }

    }

    public class TSYr2RegResult
    {
        public string Exam { get; set; }
        public bool IsResultSet { get; set; }
        public string HallTicketNumber { get; set; }
        public string District { get; set; }
        public string FullName { get; set; }
        public string Yr1Subject1 { get; set; }
        public string Yr1Subject1_Marks { get; set; }
        public string Yr1Subject1_Indicator { get; set; }
        public string Yr1Subject1_Result { get; set; }
        public string Yr1Subject2 { get; set; }
        public string Yr1Subject2_Marks { get; set; }
        public string Yr1Subject2_Indicator { get; set; }
        public string Yr1Subject2_Result { get; set; }
        public string Yr1Subject3 { get; set; }
        public string Yr1Subject3_Marks { get; set; }
        public string Yr1Subject3_Indicator { get; set; }
        public string Yr1Subject3_Result { get; set; }
        public string Yr1Subject4 { get; set; }
        public string Yr1Subject4_Marks { get; set; }
        public string Yr1Subject4_Indicator { get; set; }
        public string Yr1Subject4_Result { get; set; }
        public string Yr1Subject5 { get; set; }
        public string Yr1Subject5_Marks { get; set; }
        public string Yr1Subject5_Indicator { get; set; }
        public string Yr1Subject5_Result { get; set; }
        public string Yr1Subject6 { get; set; }
        public string Yr1Subject6_Marks { get; set; }
        public string Yr1Subject6_Indicator { get; set; }
        public string Yr1Subject6_Result { get; set; }
        public string Yr2Subject1 { get; set; }
        public string Yr2Subject1_Marks { get; set; }
        public string Yr2Subject1_Indicator { get; set; }
        public string Yr2Subject1_Result { get; set; }
        public string Yr2Subject2 { get; set; }
        public string Yr2Subject2_Marks { get; set; }
        public string Yr2Subject2_Indicator { get; set; }
        public string Yr2Subject2_Result { get; set; }
        public string Yr2Subject3 { get; set; }
        public string Yr2Subject3_Marks { get; set; }
        public string Yr2Subject3_Indicator { get; set; }
        public string Yr2Subject3_Result { get; set; }
        public string Yr2Subject4 { get; set; }
        public string Yr2Subject4_Marks { get; set; }
        public string Yr2Subject4_Indicator { get; set; }
        public string Yr2Subject4_Result { get; set; }
        public string Yr2Subject5 { get; set; }
        public string Yr2Subject5_Marks { get; set; }
        public string Yr2Subject5_Indicator { get; set; }
        public string Yr2Subject5_Result { get; set; }
        public string Yr2Subject6 { get; set; }
        public string Yr2Subject6_Marks { get; set; }
        public string Yr2Subject6_Indicator { get; set; }
        public string Yr2Subject6_Result { get; set; }
        public string Yr2Subject7 { get; set; }
        public string Yr2Subject7_Marks { get; set; }
        public string Yr2Subject7_Indicator { get; set; }
        public string Yr2Subject7_Result { get; set; }
        public string Yr2Subject8 { get; set; }
        public string Yr2Subject8_Marks { get; set; }
        public string Yr2Subject8_Indicator { get; set; }
        public string Yr2Subject8_Result { get; set; }
        public string Yr2Subject9 { get; set; }
        public string Yr2Subject9_Marks { get; set; }
        public string Yr2Subject9_Indicator { get; set; }
        public string Yr2Subject9_Result { get; set; }
        public string Yr2Subject10 { get; set; }
        public string Yr2Subject10_Marks { get; set; }
        public string Yr2Subject10_Indicator { get; set; }
        public string Yr2Subject10_Result { get; set; }
        public string Total { get; set; }
        public string Result { get; set; }
        public string AdditionalFlag { get; set; }
    }

    public class TSYr2VocResult
    {
        public string Exam { get; set; }
        public bool IsResultSet { get; set; }
        public string HallTicketNumber { get; set; }
        public string District { get; set; }
        public string FullName { get; set; }
        public string Course { get; set; }
        public string Year1Subject1 { get; set; }
        public string Year1Subject1_Marks { get; set; }
        public string Year1Subject1_Indicator { get; set; }
        public string Year1Subject1_Result { get; set; }
        public string Year1Subject2 { get; set; }
        public string Year1Subject2_Marks { get; set; }
        public string Year1Subject2_Indicator { get; set; }
        public string Year1Subject2_Result { get; set; }
        public string Year1Subject3 { get; set; }
        public string Year1Subject3_Marks { get; set; }
        public string Year1Subject3_Indicator { get; set; }
        public string Year1Subject3_Result { get; set; }
        public string Year1Subject4 { get; set; }
        public string Year1Subject4_Marks { get; set; }
        public string Year1Subject4_Indicator { get; set; }
        public string Year1Subject4_Result { get; set; }
        public string Year1Subject5 { get; set; }
        public string Year1Subject5_Marks { get; set; }
        public string Year1Subject5_Indicator { get; set; }
        public string Year1Subject5_Result { get; set; }
        public string Year1Subject6 { get; set; }
        public string Year1Subject6_Marks { get; set; }
        public string Year1Subject6_Indicator { get; set; }
        public string Year1Subject6_Result { get; set; }
        public string Year1Subject7 { get; set; }
        public string Year1Subject7_Marks { get; set; }
        public string Year1Subject7_Indicator { get; set; }
        public string Year1Subject7_Result { get; set; }
        public string Year1Subject8 { get; set; }
        public string Year1Subject8_Marks { get; set; }
        public string Year1Subject8_Indicator { get; set; }
        public string Year1Subject8_Result { get; set; }
        public string Year1Subject9 { get; set; }
        public string Year1Subject9_Marks { get; set; }
        public string Year1Subject9_Indicator { get; set; }
        public string Year1Subject9_Result { get; set; }
        public string Year2Subject1 { get; set; }
        public string Year2Subject1_Marks { get; set; }
        public string Year2Subject1_Indicator { get; set; }
        public string Year2Subject1_Result { get; set; }
        public string Year2Subject2 { get; set; }
        public string Year2Subject2_Marks { get; set; }
        public string Year2Subject2_Indicator { get; set; }
        public string Year2Subject2_Result { get; set; }
        public string Year2Subject3 { get; set; }
        public string Year2Subject3_Marks { get; set; }
        public string Year2Subject3_Indicator { get; set; }
        public string Year2Subject3_Result { get; set; }
        public string Year2Subject4 { get; set; }
        public string Year2Subject4_Marks { get; set; }
        public string Year2Subject4_Indicator { get; set; }
        public string Year2Subject4_Result { get; set; }
        public string Year2Subject5 { get; set; }
        public string Year2Subject5_Marks { get; set; }
        public string Year2Subject5_Indicator { get; set; }
        public string Year2Subject5_Result { get; set; }
        public string Year2Subject6 { get; set; }
        public string Year2Subject6_Marks { get; set; }
        public string Year2Subject6_Indicator { get; set; }
        public string Year2Subject6_Result { get; set; }
        public string Year2Subject7 { get; set; }
        public string Year2Subject7_Marks { get; set; }
        public string Year2Subject7_Indicator { get; set; }
        public string Year2Subject7_Result { get; set; }
        public string Year2Subject8 { get; set; }
        public string Year2Subject8_Marks { get; set; }
        public string Year2Subject8_Indicator { get; set; }
        public string Year2Subject8_Result { get; set; }
        public string Year2Subject9 { get; set; }
        public string Year2Subject9_Marks { get; set; }
        public string Year2Subject9_Indicator { get; set; }
        public string Year2Subject9_Result { get; set; }
        public string Total { get; set; }
        public string Result { get; set; }

        //public string Year1Subject2 { get; set; }
        //public string inter2voc_YR1FMKS2 { get; set; }
        //public string inter2voc_YR1FIND2 { get; set; }
        //public string inter2voc_YR1FRES2 { get; set; }
        //public string inter2voc_YR1TPC1 { get; set; }
        //public string Yr1Subject3_Yr1TSubject1 { get; set; }
        //public string inter2voc_YR1TMKS1 { get; set; }
        //public string inter2voc_YR1TIND1 { get; set; }
        //public string inter2voc_YR1TRES1 { get; set; }
        //public string inter2voc_YR1TPC2 { get; set; }
        //public string Yr1Subject4_Yr1TSubject2 { get; set; }
        //public string inter2voc_YR1TMKS2 { get; set; }
        //public string inter2voc_YR1TIND2 { get; set; }
        //public string inter2voc_YR1TRES2 { get; set; }
        //public string inter2voc_YR1TPC3 { get; set; }
        //public string Yr1Subject5_Yr1TSubject3 { get; set; }
        //public string inter2voc_YR1TMKS3 { get; set; }
        //public string inter2voc_YR1TIND3 { get; set; }
        //public string inter2voc_YR1TRES3 { get; set; }
        //public string inter2voc_YR1PPC1 { get; set; }
        //public string Yr1Subject6_Yr1PSubject1 { get; set; }
        //public string inter2voc_YR1PMKS1 { get; set; }
        //public string inter2voc_YR1PIND1 { get; set; }
        //public string inter2voc_YR1PRES1 { get; set; }
        //public string inter2voc_YR1PPC2 { get; set; }
        //public string Yr1Subject7_Yr1PSubject2 { get; set; }
        //public string inter2voc_YR1PMKS2 { get; set; }
        //public string inter2voc_YR1PIND2 { get; set; }
        //public string inter2voc_YR1PRES2 { get; set; }
        //public string inter2voc_YR1PPC3 { get; set; }
        //public string Yr1Subject8_Yr1PSubject3 { get; set; }
        //public string inter2voc_YR1PMKS3 { get; set; }
        //public string inter2voc_YR1PIND3 { get; set; }
        //public string inter2voc_YR1PRES3 { get; set; }
        //public string inter2voc_YR1PPC4 { get; set; }
        //public string Yr1Subject9_Yr1PSubject4 { get; set; }
        //public string inter2voc_YR1PMKS4 { get; set; }
        //public string inter2voc_YR1PIND4 { get; set; }
        //public string inter2voc_YR1PRES4 { get; set; }
        //public string inter2voc_YR2FPC1 { get; set; }
        //public string Yr2Subject1_Yr2FSubject1 { get; set; }
        //public string inter2voc_YR2FMKS1 { get; set; }
        //public string inter2voc_YR2FIND1 { get; set; }
        //public string inter2voc_YR2FRES1 { get; set; }
        //public string inter2voc_YR2FPC2 { get; set; }
        //public string Yr2Subject2_Yr2FSubject2 { get; set; }
        //public string inter2voc_YR2FMKS2 { get; set; }
        //public string inter2voc_YR2FIND2 { get; set; }
        //public string inter2voc_YR2FRES2 { get; set; }
        //public string inter2voc_YR2TPC1 { get; set; }
        //public string Yr2Subject3_Yr2TSubject1 { get; set; }
        //public string inter2voc_YR2TMKS1 { get; set; }
        //public string inter2voc_YR2TIND1 { get; set; }
        //public string inter2voc_YR2TRES1 { get; set; }
        //public string inter2voc_YR2TPC2 { get; set; }
        //public string Yr2Subject4_Yr2TSubject2 { get; set; }
        //public string inter2voc_YR2TMKS2 { get; set; }
        //public string inter2voc_YR2TIND2 { get; set; }
        //public string inter2voc_YR2TRES2 { get; set; }
        //public string inter2voc_YR2TPC3 { get; set; }
        //public string Yr2Subject5_Yr2TSubject3 { get; set; }
        //public string inter2voc_YR2TMKS3 { get; set; }
        //public string inter2voc_YR2TIND3 { get; set; }
        //public string inter2voc_YR2TRES3 { get; set; }
        //public string inter2voc_YR2PPC1 { get; set; }
        //public string Yr2Subject6_Yr2PSubject1 { get; set; }
        //public string inter2voc_YR2PMKS1 { get; set; }
        //public string inter2voc_YR2PIND1 { get; set; }
        //public string inter2voc_YR2PRES1 { get; set; }
        //public string inter2voc_YR2PPC2 { get; set; }
        //public string Yr2Subject7_Yr2PSubject2 { get; set; }
        //public string inter2voc_YR2PMKS2 { get; set; }
        //public string inter2voc_YR2PIND2 { get; set; }
        //public string inter2voc_YR2PRES2 { get; set; }
        //public string inter2voc_YR2PPC3 { get; set; }
        //public string Yr2Subject8_Yr2PSubject3 { get; set; }
        //public string inter2voc_YR2PMKS3 { get; set; }
        //public string inter2voc_YR2PIND3 { get; set; }
        //public string inter2voc_YR2PRES3 { get; set; }
        //public string inter2voc_YR2PPC4 { get; set; }
        //public string Yr2Subject9_Yr2PSubject4 { get; set; }
        //public string inter2voc_YR2PMKS4 { get; set; }
        //public string inter2voc_YR2PIND4 { get; set; }
        //public string inter2voc_YR2PRES4 { get; set; }
        //public string inter2voc_TOTAL { get; set; }
        //public string inter2voc_RESULT { get; set; }
    }

    public class ResultsSubjectsMasterMini
    {
        public string FullID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class UploadedResults
    {
        public string ExamType { get; set; }
        public string ExamYear { get; set; }
        public string State { get; set; }
        public string EducationType { get; set; }
    }

    public class APSSCResults
    {
        public string Exam { get; set; }
        public bool IsResultSet { get; set; }
        public string District { get; set; }
        public string SchoolCode { get; set; }
        public string RollNo { get; set; }
        public string FullName { get; set; }
        public string PHDescription { get; set; }
        public string Language1Flag { get; set; }
        public string Language1InternalGrade { get; set; }
        public string Language1Grade1 { get; set; }
        public string Language1FinalGrade { get; set; }
        public string Language1Points { get; set; }
        public string Language2Flag { get; set; }
        public string Language2InternalGrade { get; set; }
        public string Language2Grade1 { get; set; }
        public string Language2FinalGrade { get; set; }
        public string Language2Points { get; set; }
        public string Language3Flag { get; set; }
        public string Language3InternalGrade { get; set; }
        public string Language3Grade1 { get; set; }
        public string Language3FinalGrade { get; set; }
        public string Language3Points { get; set; }
        public string MathsFlag { get; set; }
        public string MathsInternalGrade { get; set; }
        public string MathsGrade1 { get; set; }
        public string MathsFinalGrade { get; set; }
        public string MathsPoints { get; set; }
        public string ScienceFlag { get; set; }
        public string ScienceInternalGrade { get; set; }
        public string ScienceGrade1 { get; set; }
        public string ScienceFinalGrade { get; set; }
        public string SciencePoints { get; set; }
        public string SocialFlag { get; set; }
        public string SocialInternalGrade { get; set; }
        public string SocialGrade1 { get; set; }
        public string SocialFinalGrade { get; set; }
        public string SocialPoints { get; set; }
        public string GPA { get; set; }
        public string Result { get; set; }
        public string OptionalLanguageFlag { get; set; }
        public string OptionalLanguageInternalGrade { get; set; }
        public string OptionalLanguageGrade1 { get; set; }
        public string OptionalLanguageFinalGrade { get; set; }
        public string OptionalLanguagePoints { get; set; }
        public string ValueEducationLifeSkillsGrade { get; set; }
        public string ArtCulturalEducationGrade { get; set; }
        public string WorkComputerEducationGrade { get; set; }
        public string PhysicalHealthEducationGrade { get; set; }
        public string DistrictName { get; set; }
        public string Message { get; set; }

    }

    public class TSSSCResults
    {
        public string Exam { get; set; }
        public bool IsResultSet { get; set; }
        public string District { get; set; }
        public string SchoolCode { get; set; }
        public string RollNo { get; set; }
        public string FullName { get; set; }
        public string PHDescription { get; set; }
        public string Language1Flag { get; set; }
        public string Language1InternalGrade { get; set; }
        public string Language1Grade1 { get; set; }
        public string Language1FinalGrade { get; set; }
        public string Language1Points { get; set; }
        public string Language2Flag { get; set; }
        public string Language2InternalGrade { get; set; }
        public string Language2Grade1 { get; set; }
        public string Language2FinalGrade { get; set; }
        public string Language2Points { get; set; }
        public string Language3Flag { get; set; }
        public string Language3InternalGrade { get; set; }
        public string Language3Grade1 { get; set; }
        public string Language3FinalGrade { get; set; }
        public string Language3Points { get; set; }
        public string MathsFlag { get; set; }
        public string MathsInternalGrade { get; set; }
        public string MathsGrade1 { get; set; }
        public string MathsFinalGrade { get; set; }
        public string MathsPoints { get; set; }
        public string ScienceFlag { get; set; }
        public string ScienceInternalGrade { get; set; }
        public string ScienceGrade1 { get; set; }
        public string ScienceFinalGrade { get; set; }
        public string SciencePoints { get; set; }
        public string SocialFlag { get; set; }
        public string SocialInternalGrade { get; set; }
        public string SocialGrade1 { get; set; }
        public string SocialFinalGrade { get; set; }
        public string SocialPoints { get; set; }
        public string GPA { get; set; }
        public string Result { get; set; }
        public string OptionalLanguageFlag { get; set; }
        public string OptionalLanguageInternalGrade { get; set; }
        public string OptionalLanguageGrade1 { get; set; }
        public string OptionalLanguageFinalGrade { get; set; }
        public string OptionalLanguagePoints { get; set; }
        public string ValueEducationLifeSkillsGrade { get; set; }
        public string ValueEducationLifeSkillsPoints { get; set; }
        public string ArtCulturalEducationGrade { get; set; }
        public string ArtCulturalEducationPoints { get; set; }
        public string WorkComputerEducationGrade { get; set; }
        public string WorkComputerEducationPoints { get; set; }
        public string PhysicalHealthEducationGrade { get; set; }
        public string PhysicalHealthEducationPoints { get; set; }
        public string DistrictName { get; set; }
        public string Message { get; set; }
    }

    #region EAMCET

    public class TSEamcetEng
    {
        public string Exam { get; set; }
        public bool IsResultSet { get; set; }
        public string HallTicketNumber { get; set; } = "";
        public string FullName { get; set; } = "";
        public string FathersName { get; set; } = "";
        public string MothersName { get; set; } = "";
        public string Gender { get; set; } = "";
        public string LocalArea { get; set; } = "";
        public string Category { get; set; } = "";
        public string Maths { get; set; } = "";
        public string Physics { get; set; } = "";
        public string Chemistry { get; set; } = "";
        public string TotalMarks { get; set; } = "";
        public string Rank { get; set; } = "";
        public string Result { get; set; } = "";
    }

    public class TSEamcetAM // agriculture/medical
    {
        public string Exam { get; set; }
        public bool IsResultSet { get; set; }
        public string HallTicketNumber { get; set; } = "";
        public string FullName { get; set; } = "";
        public string FathersName { get; set; } = "";
        public string MothersName { get; set; } = "";
        public string Gender { get; set; } = "";
        public string LocalArea { get; set; } = "";
        public string Category { get; set; } = "";
        public string Botany { get; set; } = "";
        public string Zoology { get; set; } = "";
        public string Physics { get; set; } = "";
        public string Chemistry { get; set; } = "";
        public string TotalMarks { get; set; } = "";
        public string Rank { get; set; } = "";
        public string Result { get; set; } = "";
    }

    public class APEapcetEng
    {
        public string Exam { get; set; }
        public bool IsResultSet { get; set; }
        public string HallTicketNumber { get; set; } = "";
        public string FullName { get; set; } = "";
        public string FathersName { get; set; } = "";
        public string MothersName { get; set; } = "";
        public string Mathematics { get; set; } = "";
        public string Physics { get; set; } = "";
        public string Chemistry { get; set; } = "";
        public string Total { get; set; } = "";
        public string Rank { get; set; } = "";
        public string Status { get; set; } = "";
    }

    public class APEapcetAM // agriculture/medical
    {
        public string Exam { get; set; }
        public bool IsResultSet { get; set; }
        public string HallTicketNumber { get; set; } = "";
        public string FullName { get; set; } = "";
        public string FathersName { get; set; } = "";
        public string MothersName { get; set; } = "";
        public string Botany { get; set; } = "";
        public string Zoology { get; set; } = "";
        public string Physics { get; set; } = "";
        public string Chemistry { get; set; } = "";
        public string Total { get; set; } = "";
        public string Rank { get; set; } = "";
        public string Status { get; set; } = "";
    }

    #endregion

    #region WEB API

    public class ExamWebApi
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ExamTypeName { get; set; }
        public string State { get; set; }
        public string EducationType { get; set; }
    }

    public class ExamResultsAvailable
    {
        public string ExamName { get; set; }
        public string ExamYear { get; set; }
        public string State { get; set; }
        public string EducationType { get; set; }
        public string ApiReferenceUrl { get; set; }
        public string Description { get; set; }
    }

    #endregion
}