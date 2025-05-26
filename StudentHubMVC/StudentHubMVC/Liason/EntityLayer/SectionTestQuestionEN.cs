using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason.EntityLayer
{
    public class SectionTestQuestionEN
    {
        public string TestId { get; set; }
        public string Question { get; set; }

        [DisplayName("Option A Text")]
        public string OptionA { get; set; }

        [DisplayName("Option B Text")]
        public string OptionB { get; set; }

        [DisplayName("Option C Text")]
        public string OptionC { get; set; }

        [DisplayName("Option D Text")]
        public string OptionD { get; set; }

        public string Answer { get; set; }

        public string Sno { get; set; }
        public string UserId { get; set; }
    }
}