using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels
{
    #region POLYCET

    public class APPolycet
    {
        public string Exam { get; set; }
        public bool IsResultSet { get; set; }
        public string HallTicketNumber { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Maths { get; set; } = "";
        public string Physics { get; set; } = "";
        public string Chemistry { get; set; } = "";
        public string Total { get; set; } = "";
        public string Status { get; set; } = "";
        public string Rank { get; set; } = "";
    }

    public class TSPolycet
    {
        public string Exam { get; set; }
        public bool IsResultSet { get; set; }
        public string HallTicketNumber { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Maths { get; set; } = "";
        public string Physics { get; set; } = "";
        public string Chemistry { get; set; } = "";
        public string Biology { get; set; } = "";
        public string MPCTotal { get; set; } = "";
        public string MPCStatus { get; set; } = "";
        public string MPCRank { get; set; } = "";
        public string MBiPCTotal { get; set; } = "";
        public string MBiPCStatus { get; set; } = "";
        public string MBiPCRank { get; set; } = "";
    }

    #endregion
}