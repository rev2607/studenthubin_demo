using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Areas.student.Models
{
    public class VM_MockTestTypesList
    {
        public long CourseId { get; set; }
        public string CourseName { get; set; }
        public List<string> DifficultyTypes { get; set; }
        public List<MockTestDetails> MockTestDetails { get; set; }

    }

    public class MockTestDetails
    {
        public long MockTestId { get; set; }
        public string Difficulty { get; set; }
        public int PercentCompleted { get; set; }
        public string DateStarted { get; set; }
        public string Status { get; set; }
        public int TotalQuestions { get; set; }
        public int AnsweredQuestions { get; set; }
    }

    public class VM_MockTestQuestion
    {
        public long CourseId { get; set; }
        public long MockTestId { get; set; }
        public long QuestionId { get; set; }
        public string CourseName { get; set; }
        public string Difficulty { get; set; }
        public string QuestionSNo { get; set; }
        public string TotalQuestions { get; set; }
        public MockTestQuestions QuestionDetails { get; set; }
        public long AnswerSelected { get; set; }
        public string TimerSelected { get; set; }
        public string TimerAnswered { get; set; }
    }
}