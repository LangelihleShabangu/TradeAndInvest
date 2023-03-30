namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Facilitation")]
    public partial class Facilitation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Facilitation()
        {
            FacilitationRequest = new HashSet<FacilitationRequest>();
        }

        public int FacilitationId { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string ProjectDescription { get; set; }

        [Required]
        public string ProjectChallangesConstraints { get; set; }

        [Required]
        public string Expectations { get; set; }

        [Required]
        public string ProjectStatus { get; set; }

        [Required]
        public string RelevantDepartments { get; set; }

        [Required]
        public string FuturePlans { get; set; }

        [Required]
        public string ProjectManagerName { get; set; }

        [Required]
        public string CellNumber { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public decimal InvestmentValue { get; set; }

        public int JobsOpportunities { get; set; }

        public int Permanent { get; set; }

        public int Temporary { get; set; }

        public int Totals { get; set; }

        public Guid? CRMLeadId { get; set; }

        public Guid? CRMOpportunityId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacilitationRequest> FacilitationRequest { get; set; }
    }
}
