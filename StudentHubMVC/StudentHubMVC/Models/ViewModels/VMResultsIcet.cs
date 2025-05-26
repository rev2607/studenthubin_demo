using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels
{
    #region ICET

    public class TSIcet
    {
        public string Exam { get; set; }
        public bool IsResultSet { get; set; }
        public string HallTicketNumber { get; set; } = "";
        public string FullName { get; set; } = "";
        public string FatherName { get; set; } = "";
        public string Category { get; set; } = "";
        public string Total { get; set; } = "";
        public string SecA { get; set; } = "";
        public string SecB { get; set; } = "";
        public string SecC { get; set; } = "";
        public string Rank { get; set; } = "";
        public string Status { get; set; } = "";
    }

    public class APIcet
    {
        public string Exam { get; set; }
        public bool IsResultSet { get; set; }
        public string HallTicketNumber { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Total { get; set; } = "";
        public string SecA { get; set; } = "";
        public string SecB { get; set; } = "";
        public string SecC { get; set; } = "";
        public string Rank { get; set; } = "";
    }

    #endregion
}