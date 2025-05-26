using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace StudentHubMVC
{
    [MetadataType(typeof(MetaCollege))]
    public partial class Colleges
    { }

    public class MetaCollege
    {
        public long coll_ID { get; set; }
        [DisplayName("College Name")]
        [Required]
        public string coll_Name { get; set; }
        [DisplayName("URL Keyword")]
        public string coll_UrlKeyword { get; set; }
        [DisplayName("College Type")]
        public long coll_Type { get; set; }
        [DisplayName("Description")]
        [AllowHtml]
        public string coll_Description { get; set; }
        [DisplayName("Logo")]
        public string coll_Logo { get; set; }
        [DisplayName("Cover Pic")]
        public string coll_CoverPic { get; set; }

        [DisplayName("College Address")]
        public string coll_Address { get; set; }

        [DisplayName("Also Known as")]
        public string coll_Knownas { get; set; }
        [DisplayName("Academic Departments")]
        public string coll_AcademicDepartments { get; set; }
        [DisplayName("Academic Centres")]
        public string coll_AcademicCentres { get; set; }
        [DisplayName("No of Faculties")]
        public string coll_FacultyNo { get; set; }
        [DisplayName("Student Intake")]
        public string coll_StudentsIntake { get; set; }
        [DisplayName("Student Diversity")]
        public string coll_StudentsDiversity { get; set; }
        [DisplayName("Faculty to Student Ratio")]
        public string coll_FacultytoStudentRatio { get; set; }
        [DisplayName("Annual Expenditure")]
        public string coll_AnnualExpenditure { get; set; }
        [DisplayName("Research Details")]
        public string coll_ResearchDetails { get; set; }

        [DisplayName("Sub-Type")]
        public string coll_SubType { get; set; }
        [DisplayName("Type of Institute")]
        public string coll_TypeOfInstitute { get; set; }
        [DisplayName("4 Years Degrees")]
        [AllowHtml]
        public string coll_4YearsDegree { get; set; }
        [DisplayName("5 Years Degrees")]
        [AllowHtml]
        public string coll_5YearsDegree { get; set; }
        [DisplayName("3 Years Degrees")]
        [AllowHtml]
        public string coll_3YearsDegree { get; set; }
        [DisplayName("Mode of Admission")]
        [AllowHtml]
        public string coll_ModeOfAdmission { get; set; }
        [DisplayName("Fee Structure")]
        [AllowHtml]
        public string coll_FeesStructure { get; set; }
        [DisplayName("Hostel Fee")]
        [AllowHtml]
        public string coll_HostelFee { get; set; }
        [DisplayName("Fee Waivers")]
        [AllowHtml]
        public string coll_FeeWaivers { get; set; }
        [DisplayName("Scholarships")]
        [AllowHtml]
        public string coll_Scholarships { get; set; }
        [DisplayName("Campus Facilities")]
        [AllowHtml]
        public string coll_CampusFacilities { get; set; }
        [DisplayName("Rankings")]
        [AllowHtml]
        public string coll_Rankings { get; set; }
        [DisplayName("Connectivity")]
        [AllowHtml]
        public string coll_Connectivity { get; set; }

        [DisplayName("Address Line 1")]
        public string coll_AddressLine1 { get; set; }
        [DisplayName("Address Line 2")]
        public string coll_AddressLine2 { get; set; }
        [DisplayName("Address Line 3")]
        public string coll_AddressLine3 { get; set; }
        [DisplayName("City")]
        public string coll_City { get; set; }
        [DisplayName("State")]
        public string coll_State { get; set; }
        [DisplayName("Pincode")]
        public string coll_Pincode { get; set; }
        [DisplayName("Country")]
        public string coll_Country { get; set; }
        [DisplayName("Map Location")]
        public string coll_Location { get; set; }
        [DisplayName("Approved by")]
        public string coll_Approval { get; set; }
        [DisplayName("Accreditation")]
        public string coll_Accreditation { get; set; }
        [DisplayName("Ownership")]
        public string coll_Ownership { get; set; }
        [DisplayName("Genders Accepted")]
        public string coll_EducationStatus { get; set; }
        [DisplayName("Website")]
        public string coll_Website { get; set; }
        [DisplayName("Phone 1")]
        public string coll_Phone1 { get; set; }
        [DisplayName("Phone 2")]
        public string coll_Phone2 { get; set; }
        [DisplayName("Email 1")]
        public string coll_Email1 { get; set; }
        [DisplayName("Email 2")]
        public string coll_Email2 { get; set; }
        [DisplayName("Facebook")]
        public string coll_Facebook { get; set; }
        [DisplayName("Twitter")]
        public string coll_Twitter { get; set; }
        [DisplayName("Youtube")]
        public string coll_Youtube { get; set; }
        [DisplayName("Highest Package")]
        public string coll_HighestPackage { get; set; }
        [DisplayName("Average Package")]
        public string coll_AveragePackage { get; set; }
        [DisplayName("Lowest Package")]
        public string coll_LowestPackage { get; set; }
        [DisplayName("International Package")]
        public string coll_InternationalPackage { get; set; }
        [DisplayName("Recruiters")]
        [AllowHtml]
        public string coll_Recruiters { get; set; }
        [DisplayName("Status")]
        public string coll_Status { get; set; }
        [DisplayName("Year of Establishment")]
        public string coll_EstablishedYear { get; set; }
        [DisplayName("Campus Area")]
        public string coll_CampusSize { get; set; }
        [DisplayName("Trending")]
        public string coll_IsTrending { get; set; }
        [DisplayName("Featured")]
        public string coll_IsFeatured { get; set; }
        [DisplayName("Top")]
        public string coll_IsTop { get; set; }

    }

    public partial class Colleges
    {
        [NotMapped]
        [DisplayName("College Courses")]
        public int[] SelectedCourses { get; set; }
        [NotMapped]
        public string CoursesIds { get; set; }
        [NotMapped]
        [DisplayName("College Facilites")]
        public int[] SelectedFacilites { get; set; }
        [NotMapped]
        public string FacilitiesIds { get; set; }
    }
}