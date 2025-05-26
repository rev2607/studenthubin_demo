using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StudentHubMVC
{

    // ------------------------------------------------------------------- MOCK TEST QUESTIONS
    [MetadataType(typeof(MetaMockTestQuestions))]
    public partial class MockTestQuestions
    {
    }

    public class MetaMockTestQuestions
    {
        [Required]
        public long MockTestId { get; set; }
        [Required]
        [Display(Name = "Question Text")]
        [AllowHtml]
        public string QuestionText { get; set; }
        [Required]
        [Display(Name = "Correct Answer Option")]
        [AllowHtml]
        public string AnswerOption { get; set; }
        [Display(Name = "Option A")]
        [AllowHtml]
        public string OptionA { get; set; }
        [Display(Name = "Option B")]
        [AllowHtml]
        public string OptionB { get; set; }
        [Display(Name = "Option C")]
        [AllowHtml]
        public string OptionC { get; set; }
        [Display(Name = "Option D")]
        [AllowHtml]
        public string OptionD { get; set; }
        [AllowHtml]
        public string Explanation { get; set; }
        [Display(Name ="Option E")]
        [AllowHtml]
        public string OptionE { get; set; }
    }
}