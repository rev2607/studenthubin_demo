using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason.EntityLayer
{
    public class VoucherEN
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Notes { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }

        public string Sno { get; set; }
        public string UserId { get; set; }
    }
}