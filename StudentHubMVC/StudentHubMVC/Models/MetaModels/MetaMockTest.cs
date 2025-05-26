using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentHubMVC
{
    // ------------------------------------------------------ MockTest
    [MetadataType(typeof(MetaMockTest))]
    public partial class MockTests
    {
    }

    public class MetaMockTest
    {
        //public string Price { get; set; }
        [Display(Name = "Offer Price")]
        public string DiscountOfferPrice { get; set; }
        [Display(Name = "Offer Code")]
        public string CouponCodeOffer { get; set; }
        [Display(Name = "Free/Paid")]
        public string FreePaidTest { get; set; }
        [Display(Name = "Govt/Private")]
        public string GovtPrivateExam { get; set; }
        //public string Language { get; set; }
    }
}