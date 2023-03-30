namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContactInformation")]
    public partial class ContactInformation
    {
        public int ContactInformationId { get; set; }

        public int AddressId { get; set; }

        public int TitleId { get; set; }

        public int GenderId { get; set; }

        [Required]
        [StringLength(20)]
        public string IdNumber { get; set; }

        [Required]
        [StringLength(120)]
        public string PassportNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string CellNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string Surname { get; set; }

        [Required]
        [StringLength(255)]
        public string KnownAs { get; set; }

        [Required]
        [StringLength(255)]
        public string EmailAddress { get; set; }

        public DateTime BirthDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Address Address { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual Title Title { get; set; }
    }
}
