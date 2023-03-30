namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FacilitationRequest")]
    public partial class FacilitationRequest
    {
        public int FacilitationRequestId { get; set; }

        public int FacilitationId { get; set; }

        [Required]
        [StringLength(255)]
        public string NeededAssistance { get; set; }

        [Required]
        public string Comments { get; set; }

        public Guid? CRMFacilitationRequestId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Facilitation Facilitation { get; set; }
    }
}
