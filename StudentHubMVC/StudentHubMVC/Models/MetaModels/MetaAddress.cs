using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentHubMVC
{
    [MetadataType(typeof(MetaAddress))]
    public partial class Addresses
    {

    }

    public class MetaAddress
    {
        [Key]
        public long AddressId { get; set; }
        public long InstitutionId { get; set; }

        [DisplayName("Branch Name")]
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [DisplayName("Address Line 1")]
        [MaxLength(50)]
        [Required]
        public string Line1 { get; set; }

        [DisplayName("Address Line 2")]
        [MaxLength(50)]
        [Required]
        public string Line2 { get; set; }

        [DisplayName("Address Line 3")]
        [MaxLength(50)]
        public string Line3 { get; set; }

        [MaxLength(30)]
        [Required]
        public string Area { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Pincode { get; set; }
        [Required]
        public string State { get; set; }
        public string Country { get; set; }
        public string MapLocation { get; set; }
        public string Status { get; set; }

        [DisplayName("Phone 1")]
        [MaxLength(20)]
        public string Phone1 { get; set; }

        [DisplayName("Phone 2")]
        [MaxLength(20)]
        public string Phone2 { get; set; }

        [DisplayName("Mobile 1")]
        [MaxLength(20)]
        public string Mobile1 { get; set; }

        [DisplayName("Mobile 2")]
        [MaxLength(20)]
        public string Mobile2 { get; set; }

        [DisplayName("Contact Person")]
        [MaxLength(50)]
        public string ContactPerson { get; set; }

        [DisplayName("Email 1")]
        [MaxLength(150)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email1 { get; set; }

        [DisplayName("Email 2")]
        [MaxLength(150)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email2 { get; set; }
    }
}