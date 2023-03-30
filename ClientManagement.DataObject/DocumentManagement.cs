namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DocumentManagement")]
    public partial class DocumentManagement
    {
        public int DocumentManagementId { get; set; }

        public int DocumentTypeId { get; set; }

        public int FacilitationId { get; set; }

        [Required]
        [StringLength(500)]
        public string DocumentName { get; set; }

        [Required]
        [StringLength(500)]
        public string DocumentDescription { get; set; }

        public byte[] DocumentImage { get; set; }

        [Required]
        [StringLength(550)]
        public string DocumentPath { get; set; }

        [Required]
        [StringLength(150)]
        public string ContentType { get; set; }

        public Guid? CRMDocumentManagementId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual DocumentType DocumentType { get; set; }
    }
}
