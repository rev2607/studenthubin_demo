using StudentHubMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Repositories
{
    public class RepResultInter
    {
        readonly RepResults _repResults = new RepResults();
        readonly Entities db = new Entities();


        #region -------------------------------------------------------------------------- HALLTICKET SEARCH

        public TSYr2RegResult GetTSInterYr1RegularResult(string HallTicketNumber, string ExamYear)
        {
            var result = _repResults.GetTSYr2RegResult(HallTicketNumber, (int)Enums_ExamTypes.Telangana_Intermediate_Year_1_Regular, ExamYear);

            return result;
        }
        
        public TSYr2VocResult GetTSInterYr1VocationalResult(string HallTicketNumber, string ExamYear)
        {
            var result = _repResults.GetTSYr2VocResult(HallTicketNumber, (int)Enums_ExamTypes.Telangana_Intermediate_Year_1_Vocational, ExamYear);

            return result;
        }
        
        public TSYr2RegResult GetTSInterYr2RegularResult(string HallTicketNumber, string ExamYear)
        {
            var result = _repResults.GetTSYr2RegResult(HallTicketNumber, (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Regular, ExamYear);

            return result;
        }
        
        public TSYr2VocResult GetTSInterYr2VocationalResult(string HallTicketNumber, string ExamYear)
        {
            var result = _repResults.GetTSYr2VocResult(HallTicketNumber, (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Vocational, ExamYear);

            return result;
        }

        public TSYr2RegResult GetAPInterYr1RegularResult(string HallTicketNumber, string ExamYear)
        {
            var result = _repResults.GetAPRegResult(HallTicketNumber, (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_1_Regular, ExamYear);

            return result;
        }
        
        public TSYr2VocResult GetAPInterYr1VocationalResult(string HallTicketNumber, string ExamYear)
        {
            var result = _repResults.GetAPVocResult(HallTicketNumber, (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_1_Vocational, ExamYear);

            return result;
        }
        
        public TSYr2RegResult GetAPInterYr2RegularResult(string HallTicketNumber, string ExamYear)
        {
            var result = _repResults.GetAPRegResult(HallTicketNumber, (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_2_Regular, ExamYear);

            return result;
        }
        
        public TSYr2VocResult GetAPInterYr2VocationalResult(string HallTicketNumber, string ExamYear)
        {
            var result = _repResults.GetAPVocResult(HallTicketNumber, (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_2_Vocational, ExamYear);

            return result;
        }


        #endregion

        #region -------------------------------------------------------------------------- NAME SEARCH

        public List<ExamResultsNameSearchResult> GetTSInterYr1RegularResultNameSearch(string FullName, string ExamYear)
        {
            int ExamType = (int)Enums_ExamTypes.Telangana_Intermediate_Year_1_Regular;
            string ExamId = Constants_ExamIds.TelanganaIntermediateYear1Regular_TSINTR1REG;

            var result = GetNameResultsRegular(ExamId, ExamType, FullName, ExamYear);

            //var result = db.rslt_IntermediateYear2Regular
            //    .Where(_ => _.inter2reg_ExamType == ExamType && _.inter2reg_ExamYear == ExamYear && 
            //            (_.inter2reg_CNAME == FullName)) //  || _.inter2reg_CNAME.StartsWith(FullName) || _.inter2reg_CNAME.Contains(FullName)
            //    .Select(_ => new ExamResultsNameSearchResult { ExamId = ExamId, FullName = _.inter2reg_CNAME.Trim(), HallTicket = _.inter2reg_ROLLNO, Year = ExamYear })
            //    .Take(Constants_ConfigVars.ConfigMaximumResultsCount).ToList();

            return result;
        }

        public List<ExamResultsNameSearchResult> GetTSInterYr1VocationalResultNameSearch(string FullName, string ExamYear)
        {
            int ExamType = (int)Enums_ExamTypes.Telangana_Intermediate_Year_1_Vocational;
            string ExamId = Constants_ExamIds.TelanganaIntermediateYear1Vocational_TSINTR1VOC;

            var result = GetNameResultsVocational(ExamId, ExamType, FullName, ExamYear);

            //var result = db.rslt_IntermediateYear2Voc
            //    .Where(_ => _.inter2voc_ExamType == ExamType && _.inter2voc_ExamYear == ExamYear &&
            //            (_.inter2voc_CNAME == FullName)) //  || _.inter2voc_CNAME.StartsWith(FullName) || _.inter2voc_CNAME.Contains(FullName)
            //    .Select(_ => new ExamResultsNameSearchResult { ExamId = ExamId, FullName = _.inter2voc_CNAME.Trim(), HallTicket = _.inter2voc_ROLLNO, Year = ExamYear })
            //    .Take(Constants_ConfigVars.ConfigMaximumResultsCount).ToList();

            return result;
        }

        public List<ExamResultsNameSearchResult> GetTSInterYr2RegularResultNameSearch(string FullName, string ExamYear)
        {
            int ExamType = (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Regular;
            string ExamId = Constants_ExamIds.TelanganaIntermediateYear2Regular_TSINTR2REG;

            var result = GetNameResultsRegular(ExamId, ExamType, FullName, ExamYear);

            //var result = db.rslt_IntermediateYear2Regular
            //    .Where(_ => _.inter2reg_ExamType == ExamType && _.inter2reg_ExamYear == ExamYear &&
            //            (_.inter2reg_CNAME == FullName)) //  || _.inter2reg_CNAME.StartsWith(FullName) || _.inter2reg_CNAME.Contains(FullName)
            //    .Select(_ => new ExamResultsNameSearchResult { ExamId = ExamId, FullName = _.inter2reg_CNAME.Trim(), HallTicket = _.inter2reg_ROLLNO, Year = ExamYear })
            //    .Take(Constants_ConfigVars.ConfigMaximumResultsCount).ToList();

            return result;
        }

        public List<ExamResultsNameSearchResult> GetTSInterYr2VocationalResultNameSearch(string FullName, string ExamYear)
        {
            int ExamType = (int)Enums_ExamTypes.Telangana_Intermediate_Year_2_Vocational;
            string ExamId = Constants_ExamIds.TelanganaIntermediateYear2Vocational_TSINTR2VOC;

            var result = GetNameResultsVocational(ExamId, ExamType, FullName, ExamYear);

            //var result = db.rslt_IntermediateYear2Voc
            //    .Where(_ => _.inter2voc_ExamType == ExamType && _.inter2voc_ExamYear == ExamYear &&
            //            (_.inter2voc_CNAME == FullName)) //  || _.inter2voc_CNAME.StartsWith(FullName) || _.inter2voc_CNAME.Contains(FullName)
            //    .Select(_ => new ExamResultsNameSearchResult { ExamId = ExamId, FullName = _.inter2voc_CNAME.Trim(), HallTicket = _.inter2voc_ROLLNO, Year = ExamYear })
            //    .Take(Constants_ConfigVars.ConfigMaximumResultsCount).ToList();

            return result;
        }

        public List<ExamResultsNameSearchResult> GetAPInterYr1RegularResultNameSearch(string FullName, string ExamYear)
        {
            int ExamType = (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_1_Regular;
            string ExamId = Constants_ExamIds.AndhraPradeshIntermediateYear1Regular_APINTR1REG;

            var result = GetNameResultsRegular(ExamId, ExamType, FullName, ExamYear);

            //var result = db.rslt_IntermediateYear2Regular.AsNoTracking()
            //    .Where(_ => _.inter2reg_ExamType == ExamType && _.inter2reg_ExamYear == ExamYear &&
            //            (_.inter2reg_CNAME == FullName || _.inter2reg_CNAME.StartsWith(FullName))) // || _.inter2reg_CNAME.Contains(FullName)
            //    .Select(_ => new ExamResultsNameSearchResult { ExamId = ExamId, FullName = _.inter2reg_CNAME.Trim(), HallTicket = _.inter2reg_ROLLNO, Year = ExamYear })
            //    .Take(Constants_ConfigVars.ConfigMaximumResultsCount).ToList();

            return result;
        }

        public List<ExamResultsNameSearchResult> GetAPInterYr1VocationalResultNameSearch(string FullName, string ExamYear)
        {
            int ExamType = (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_1_Vocational;
            string ExamId = Constants_ExamIds.AndhraPradeshIntermediateYear1Vocational_APINTR1VOC;

            var result = GetNameResultsVocational(ExamId, ExamType, FullName, ExamYear);

            //var result = db.rslt_IntermediateYear2Voc
            //    .Where(_ => _.inter2voc_ExamType == ExamType && _.inter2voc_ExamYear == ExamYear &&
            //            (_.inter2voc_CNAME == FullName)) //  || _.inter2voc_CNAME.StartsWith(FullName) || _.inter2voc_CNAME.Contains(FullName)
            //    .Select(_ => new ExamResultsNameSearchResult { ExamId = ExamId, FullName = _.inter2voc_CNAME.Trim(), HallTicket = _.inter2voc_ROLLNO, Year = ExamYear })
            //    .Take(Constants_ConfigVars.ConfigMaximumResultsCount).ToList();

            return result;
        }

        public List<ExamResultsNameSearchResult> GetAPInterYr2RegularResultNameSearch(string FullName, string ExamYear)
        {
            int ExamType = (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_2_Regular;
            string ExamId = Constants_ExamIds.AndhraPradeshIntermediateYear2Regular_APINTR2REG;

            var result = GetNameResultsRegular(ExamId, ExamType, FullName, ExamYear);

            //var result = db.rslt_IntermediateYear2Regular
            //    .Where(_ => _.inter2reg_ExamType == ExamType && _.inter2reg_ExamYear == ExamYear &&
            //            (_.inter2reg_CNAME == FullName)) //  || _.inter2reg_CNAME.StartsWith(FullName) || _.inter2reg_CNAME.Contains(FullName)
            //    .Select(_ => new ExamResultsNameSearchResult { ExamId = ExamId, FullName = _.inter2reg_CNAME.Trim(), HallTicket = _.inter2reg_ROLLNO, Year = ExamYear })
            //    .Take(Constants_ConfigVars.ConfigMaximumResultsCount).ToList();

            return result;
        }

        public List<ExamResultsNameSearchResult> GetAPInterYr2VocationalResultNameSearch(string FullName, string ExamYear)
        {
            int ExamType = (int)Enums_ExamTypes.Andhra_Pradesh_Intermediate_Year_2_Vocational;
            string ExamId = Constants_ExamIds.AndhraPradeshIntermediateYear2Vocational_APINTR2VOC;

            var result = GetNameResultsVocational(ExamId, ExamType, FullName, ExamYear);

            //var result = db.rslt_IntermediateYear2Voc
            //    .Where(_ => _.inter2voc_ExamType == ExamType && _.inter2voc_ExamYear == ExamYear &&
            //            (_.inter2voc_CNAME == FullName)) //  || _.inter2voc_CNAME.StartsWith(FullName) || _.inter2voc_CNAME.Contains(FullName)
            //    .Select(_ => new ExamResultsNameSearchResult { ExamId = ExamId, FullName = _.inter2voc_CNAME.Trim(), HallTicket = _.inter2voc_ROLLNO, Year = ExamYear })
            //    .Take(Constants_ConfigVars.ConfigMaximumResultsCount).ToList();

            return result;
        }


        #endregion

        private List<ExamResultsNameSearchResult>  GetNameResultsRegular(string ExamId, int ExamType, string FullName, string ExamYear)
        {
            string SQLInterRegResults = "SELECT '{0}' AS ExamId, TRIM(inter2reg_CNAME) AS FullName, inter2reg_ROLLNO AS HallTicket, '{1}' AS Year FROM rslt_IntermediateYear2Regular WHERE inter2reg_ExamType = '{2}' AND inter2reg_ExamYear = '{1}' AND (inter2reg_CNAME = '{3}' OR inter2reg_CNAME LIKE '{3}%') LIMIT {4}";

            return db.Database.SqlQuery<ExamResultsNameSearchResult>(string.Format(SQLInterRegResults, ExamId, ExamYear, ExamType, FullName, Constants_ConfigVars.ConfigMaximumResultsCount)).ToList();
        }

        private List<ExamResultsNameSearchResult> GetNameResultsVocational(string ExamId, int ExamType, string FullName, string ExamYear)
        {
            string SQLInterVocResults = "SELECT '{0}' AS ExamId, TRIM(inter2voc_CNAME) AS FullName, inter2voc_ROLLNO AS HallTicket, '{1}' AS Year FROM rslt_IntermediateYear2Voc WHERE inter2voc_ExamType = '{2}' AND inter2voc_ExamYear = '{1}' AND (inter2voc_CNAME = '{3}' OR inter2voc_CNAME LIKE '{3}%') LIMIT {4}";

            return db.Database.SqlQuery<ExamResultsNameSearchResult>(string.Format(SQLInterVocResults, ExamId, ExamYear, ExamType, FullName, Constants_ConfigVars.ConfigMaximumResultsCount)).ToList();
        }
    }
}