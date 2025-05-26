using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels
{
    public class VMResultsSearch
    {
        public VMResultsSearch()
        {
            States = new List<string>();
            ExamTypes = new List<string>();
            Exams = new List<SearchExamSmall>();
            ExamsDetails = new List<SearchExam>();
            Years = new Liason.DropdownTypes().GetExamYearsList();
            SearchType = new List<string> { Constants_SearchTypes.HallTicket, Constants_SearchTypes.Name };
        }

        public List<string> States { get; set; }
        public List<string> ExamTypes { get; set; }
        public List<SearchExamSmall> Exams { get; set; }
        public List<SearchExam> ExamsDetails { get; set; }
        public List<string> Years { get; set; }
        public List<string> SearchType { get; set; }
    }

    public class ResultsSearchCriteria
    {
        public string State { get; set; }
        public string ExamType { get; set; }
        public string ExamId { get; set; }
        public string Year { get; set; }
    }

    public class ResultsSearchParamsPost
    {
        public string RequestType { get; set; }
        public string Value { get; set; }
        public string ExamId { get; set; }
        public string Year { get; set; }
        public string ApiKey { get; set; }
    }

    public class SearchExam
    {
        public string ExamName { get; set; }
        public string ExamId { get; set; }
        public string ExamType { get; set; }
        public string State { get; set; }
    }

    public class SearchExamSmall
    {
        public string ExamName { get; set; }
        public string ExamId { get; set; }
    }

    public class ExamResultsSearchResult
    {
        public string ExamName { get; set; }
        public string ExamId { get; set; }
        public string ExamType { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string ReleaseDate { get; set; }
        public string University { get; set; }
        public string State { get; set; }

    }

    public class ExamResultsNameSearchResult
    {
        public string HallTicket { get; set; }
        public string FullName { get; set; }
        //public string ExamName { get; set; }
        public string Year { get; set; }
        //public string ExamType { get; set; }
        public string ExamId { get; set; }
    }
}