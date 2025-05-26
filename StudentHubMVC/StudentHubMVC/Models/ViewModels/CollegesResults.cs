using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentHubMVC.Models.ViewModels
{
    public class CollegesResultsFilters
    {
        public List<StateCities> StatesAndCities { get; set; }

        public List<string> CollegeTypes;

        public List<string> CollegeSubTypes;

    }

    public class StateCities
    {
        public string State;
        public List<string> Cities;

        public StateCities(string state, List<string> cities)
        {
            State = state;
            Cities = cities;
        }

    }

    public class CollegeResults
    {
        public List<BasicCollegeDetails> colleges;
        public int collegesCount;
    }

    public class BasicCollegeDetails
    {
        public string Name { get; set; }
        public long ID { get; set; }
        public string State { get; set; }

        public string City { get; set; }

        public string CoverPic { get; set; }
        public string Logo { get; set; }
        public string Type { get; set; }

        public string SubType { get; set; }

    }

    public class CollegeDetails
    {
        public long ID { get; set; }
        
        public string Name { get; set; }
   
        public string UrlKeyword { get; set; }
      
        public long Type { get; set; }
       
        public string Description { get; set; }
     
        public string Logo { get; set; }
       
        public string CoverPic { get; set; }
       
        public string Address { get; set; }

        public string Knownas { get; set; }
        
        public string AcademicDepartments { get; set; }

        public string AcademicCentres { get; set; }
      
        public string FacultyNo { get; set; }

        public string StudentsIntake { get; set; }
      
        public string StudentsDiversity { get; set; }
     
        public string FacultytoStudentRatio { get; set; }
    
        public string AnnualExpenditure { get; set; }
        
        public string ResearchDetails { get; set; }

        public string TypeOfInstitute { get; set; }

        public string SubType { get; set; }

        public string Degree_4Years { get; set; }
     
        public string Degree_5Years { get; set; }
        
        public string Degree_3Years { get; set; }
      
        public string ModeOfAdmission { get; set; }
       
        public string FeesStructure { get; set; }
       
        public string HostelFee { get; set; }
       
        public string FeeWaivers { get; set; }
       
        public string Scholarships { get; set; }
       
        public string CampusFacilities { get; set; }
        
        public string Rankings { get; set; }
       
        public string Connectivity { get; set; }

        //public string AddressLine1 { get; set; }

        //public string AddressLine2 { get; set; }

        //public string AddressLine3 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Pincode { get; set; }

        public string Country { get; set; }

        public string Location { get; set; }

        public string Approval { get; set; }

        public string Accreditation { get; set; }

        public string Ownership { get; set; }

        public string EducationStatus { get; set; }

        public string Website { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Email1 { get; set; }

        public string Email2 { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Youtube { get; set; }

        public string HighestPackage { get; set; }
      
        public string AveragePackage { get; set; }
      
        public string LowestPackage { get; set; }
      
        public string InternationalPackage { get; set; }
  
        public string Recruiters { get; set; }

        public string Status { get; set; }
     
        public string EstablishedYear { get; set; }
      
        public string CampusSize { get; set; }
     
        public string IsTrending { get; set; }

        public string IsFeatured { get; set; }
      
        public string IsTop { get; set; }
    }

}