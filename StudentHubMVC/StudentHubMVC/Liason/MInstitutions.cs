using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason
{
    public class MInstitutions
    {
        Entities db = new Entities();

        // ----------------------------------------------------------- INSTITUTION AREAS
        public string[] GetInstitutionAreas()
        {
            return db.Institutions.Where(n => n.Area != null && n.Area != "").OrderBy(n => n.Area).GroupBy(c => c.Area)
                .Select(group => new { Areas = group.Key, data = group.ToList() }).Select(a => a.Areas).ToArray();
        }

        // ----------------------------------------------------------- INSTITUTION CITIES
        public string[] GetInstitutionCities()
        {
            return db.Institutions.OrderBy(n => n.City).GroupBy(c => c.City)
                .Select(group => new { Cities = group.Key, data = group.ToList() }).Select(a => a.Cities).ToArray();
        }

        // ----------------------------------------------------------- INSTITUTION COURSES
        public Dictionary<int, string> GetInstitutionCourses()
        {
            return db.MainCourses.OrderBy(n => n.Name)
                .Select(c => new { CourseId = c.MainCourseId, CourseName = c.Name }).ToDictionary(c => (int)c.CourseId, c => c.CourseName);
        }

    }
}