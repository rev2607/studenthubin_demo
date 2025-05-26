using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Liason
{
    public class DropdownTypes
    {
        Entities db = new Entities();


        public Dictionary<int, string> GetDropdownTypesDictionary(string dropdownType)
        {
            return db.DropdownTypes.Where(d => d.GroupName == dropdownType).ToDictionary(d => (int)d.DropdownTypeId, d => d.TypeValue);
        }

        public List<SelectListItem> GetExamYears()
        {
            List<SelectListItem> ExamYearsRef = new List<SelectListItem>();

            foreach(string examYear in GetExamYearsList())
            {
                ExamYearsRef.Add(new SelectListItem
                {
                    Text = examYear,
                    Value = examYear
                });
            }

            return ExamYearsRef;
        }

        public List<string> GetExamYearsList()
        {
            int ResultsStartYear = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ResultsStartYear"]);
            List<string> ExamYears = new List<string>();

            for (int y = ResultsStartYear; y <= DateTime.Now.Year; y++)
            {
                ExamYears.Add(y.ToString());
            }

            return ExamYears.OrderByDescending(_ => _).ToList();
        }

        public List<SelectListItem> GetMonths()
        {
            List<SelectListItem> MonthsRef = new List<SelectListItem>() 
            {
                new SelectListItem { Text = "January", Value = "January" },
                new SelectListItem { Text = "February", Value = "February" },
                new SelectListItem { Text = "March", Value = "March" },
                new SelectListItem { Text = "April", Value = "April" },
                new SelectListItem { Text = "May", Value = "May" },
                new SelectListItem { Text = "June", Value = "June" },
                new SelectListItem { Text = "July", Value = "July" },
                new SelectListItem { Text = "August", Value = "August" },
                new SelectListItem { Text = "September", Value = "September" },
                new SelectListItem { Text = "October", Value = "October" },
                new SelectListItem { Text = "November", Value = "November" },
                new SelectListItem { Text = "December", Value = "December" }
            };


            return MonthsRef;
        }
    }
}