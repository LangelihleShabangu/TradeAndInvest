using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientManagement.Interface.Models
{
    public class UserProfileModel
    {   
        [DisplayName("Windows Authentication ?")]
        public bool WindowsAuthentication { get; set; }
        public bool IsDeleted { get; set; }
        public bool PasswordChangeRequired { get; set; }
        public DateTime LastLockedOutDate { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime LastPasswordChangedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool AdminEdit { get; set; }
        [Required]
        [StringLength(10)]
        public string Telephone { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [Required]
        [StringLength(100)]
        public string Email { get; set; }        
        public string Password { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        public int UserID { get; set; }
        public int UserProfileID { get; set; }
        [Required]
        [StringLength(10)]
        public string Cellphone { get; set; }
        public string ProfilePic { get; set; }
        public string Category { get; set; }
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string AddressLineCity { get; set; }

        public string AddressLineState_Province { get; set; }

        public string AddressLinePostalCode { get; set; }

        public string AddressLineCountry { get; set; }
    }
    public class CompanyProfileModel
    {
        [DisplayName("Company Name")]
        public string CompanyName{ get; set; }
        [DisplayName("Country/Region")]
        public string CountryRegion	{ get; set; }

        [DisplayName("Number of Employees")]
        public string NumberofEmployees { get; set; }

        [DisplayName("Company Contact Phone")]
        public string CompanyContactPhone { get; set; }
        
        public string CompanyContactPerson { get; set; }

        [DisplayName("Sector")]
        public string Sector { get; set; }
        [DisplayName("Main Company")]
        public string MainCompany { get; set; }

        [DisplayName("Summary Project")]
        public string SummaryProject { get; set; }

        [DisplayName("Request Information")]
        public string RequestInformation { get; set; }
        
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        [DisplayName("Email Address")]
        public string EmailAddress{ get; set; }
       
        public string Category { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string AddressLineCity { get; set; }

        public string AddressLineState_Province { get; set; }

        public string AddressLinePostalCode { get; set; }

        public string AddressLineCountry { get; set; }
    }
}