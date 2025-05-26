using StudentHubMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StudentHubMVC.Repositories
{
    public class RepResultSearch
    {
        readonly Entities db = new Entities();

        public VMResultsSearch GetResultSearchFilters()
        {
            var courseTypes = db.rslt_CourseTypes.ToList();
            var vmResultsSearch = new VMResultsSearch()
            {
                States = courseTypes.Select(_ => _.corseTyp_State).Distinct().ToList(),
                ExamTypes = courseTypes.Select(_ => _.corseTyp_ExamType).Distinct().ToList(),
                Exams = courseTypes.Select(_ => new SearchExamSmall { ExamName = _.corseTyp_Name, ExamId = _.corseTyp_ID }).ToList(),
                ExamsDetails = courseTypes.Select(_ => new SearchExam { ExamName = _.corseTyp_Name, ExamId = _.corseTyp_ID, ExamType = _.corseTyp_ExamType, State = _.corseTyp_State }).ToList()
            };

            return vmResultsSearch;
        }

        public List<ExamResultsSearchResult> GetResultsSearchResults(ResultsSearchCriteria searchCriteria)
        {
            var dbResults = GetViewSearchResults();

            dbResults = dbResults
                .Where(_ => _.State == (!string.IsNullOrEmpty(searchCriteria.State) ? searchCriteria.State : _.State) &&
                        _.ExamType == (!string.IsNullOrEmpty(searchCriteria.ExamType) ? searchCriteria.ExamType : _.ExamType) &&
                        _.ExamId == (!string.IsNullOrEmpty(searchCriteria.ExamId) ? searchCriteria.ExamId : _.ExamId) &&
                        _.Year == (!string.IsNullOrEmpty(searchCriteria.Year) ? searchCriteria.Year : _.Year));

            return dbResults.ToList();
        }

        public IQueryable<ExamResultsSearchResult> GetViewSearchResults()
        {
            return
                from crs in db.rslt_CourseTypes
                join rslt in db.rslt_UploadedResults on crs.corseTyp_Sno equals rslt.upld_ExamType
                where rslt.upld_Status == Constants_FileUploadStatus.Done
                orderby rslt.upld_ResultReleaseDate, rslt.upld_ExamMonth, rslt.upld_ExamYear descending
                select new ExamResultsSearchResult 
                    { ExamId = crs.corseTyp_ID, 
                    ExamName = crs.corseTyp_Name,
                    ExamType = crs.corseTyp_ExamType,
                    ReleaseDate = rslt.upld_ResultReleaseDate, 
                    Month = rslt.upld_ExamMonth, 
                    Year = rslt.upld_ExamYear, 
                    State = crs.corseTyp_State, 
                    University = crs.corseTyp_University };
        }
    }
}