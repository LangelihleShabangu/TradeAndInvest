namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Leads
    {
        public int LeadsId { get; set; }

        [Required]
        public string CrmLeadId { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        public string VendorNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string EstateName { get; set; }

        [Required]
        public string CellNumber { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string CrmUri { get; set; }

        [Required]
        public string RejectionComment { get; set; }

        public int AddressId { get; set; }

        public int DwellingTypeId { get; set; }

        public int ProvinceId { get; set; }

        public int StatusId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Province Province { get; set; }

        public virtual Status Status { get; set; }
    }
}
