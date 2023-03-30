namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        public int AccountId { get; set; }

        public int UserProfileId { get; set; }

        [Required]
        [StringLength(255)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(255)]
        public string CountryRegion { get; set; }

        public int NumberofEmployees { get; set; }

        [Required]
        [StringLength(255)]
        public string CompanyContactPhone { get; set; }

        [Required]
        [StringLength(255)]
        public string Sector { get; set; }

        [Required]
        [StringLength(255)]
        public string MainCompany { get; set; }

        [Required]
        [StringLength(255)]
        public string SummaryProject { get; set; }

        [Required]
        [StringLength(255)]
        public string RequestInformation { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        public string EmailAddress { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
