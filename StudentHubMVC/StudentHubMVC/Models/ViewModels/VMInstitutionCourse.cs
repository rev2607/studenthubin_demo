using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels
{
    public class VMInstitutionCourse
    {
        public long InstitutionCourseId { get; set; }
        public long InstitutionId { get; set; }
        public long CourseId { get; set; }
        public string CourseFee { get; set; }
        public string Duration { get; set; }

        public IList<string> TrainingModes { get; set; }
        public List<InstitutionCourseTrainingModes> SavedTrainingModes { get; set; }
    }
}