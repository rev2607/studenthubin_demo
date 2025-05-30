//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentHubMVC
{
    using System;
    using System.Collections.Generic;
    
    public partial class NewsAlerts
    {
        public int NewsId { get; set; }
        public long NewsTypeId { get; set; }
        public string Title { get; set; }
        public string UrlKeyword { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public Nullable<long> CourseId { get; set; }
        public Nullable<long> InstitutionId { get; set; }
        public string Keywords { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
    
        public virtual DropdownTypes DropdownTypes { get; set; }
        public virtual Institutions Insitutions { get; set; }
        public virtual MainCourses Courses { get; set; }
    }
}
