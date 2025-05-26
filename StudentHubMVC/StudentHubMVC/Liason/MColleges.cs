using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason
{
    public class MColleges
    {
        Entities db = new Entities();

        // ----------------------------------------------------------- FEATRUED COLLEGES
        public List<StudentHubMVC.Colleges> GetFeaturedColleges()
        {
            return db.Colleges.Where(c => c.coll_IsFeatured == "true" && c.coll_Status == Constants.Active)
                .OrderByDescending(n => n.coll_ID).Take(8).ToList();
        }

        // ----------------------------------------------------------- TOP COLLEGES
        public List<StudentHubMVC.Colleges> GetTopColleges()
        {
            return db.Colleges.Where(c => c.coll_IsTop == "true" && c.coll_Status == Constants.Active)
                .OrderByDescending(n => n.coll_ID).Take(8).ToList();
        }

        // ----------------------------------------------------------- COLLEGE STATES
        public string[] GetCollegeStates()
        {
            return db.Colleges.OrderBy(n => n.coll_State).GroupBy(c => c.coll_State)
                .Select(group => new { States = group.Key, data = group.ToList() }).Select(a => a.States).ToArray();
        }

        // ----------------------------------------------------------- COLLEGE CITIES
        public string[] GetCollegeCities()
        {
            return db.Colleges.OrderBy(n => n.coll_City).GroupBy(c => c.coll_City)
                .Select(group => new { Cities = group.Key, data = group.ToList() }).Select(a => a.Cities).ToArray();
        }

        // ----------------------------------------------------------- COLLEGE COURSES
        public Dictionary<int, string> GetCollegeCourses()
        {
            return db.CollegeCourses.OrderBy(n => n.colcrs_Name)
                .Select(c => new {CourseId = c.colcrs_Id, CourseName = c.colcrs_Name }).ToDictionary(c => (int)c.CourseId, c=> c.CourseName);
        }

        // ----------------------------------------------------------- COLLEGE TYPES
        public Dictionary<int, string> GetCollegeTypes()
        {
            return db.DropdownTypes.Where(c => c.GroupName == "COLLEGETYPE").OrderBy(n => n.TypeValue)
                .Select(c => new { TypeId = c.DropdownTypeId, Type = c.TypeValue }).ToDictionary(c => (int)c.TypeId, c => c.Type);
        }

    }
}