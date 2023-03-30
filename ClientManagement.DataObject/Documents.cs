namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Documents
    {
        public int DocumentsId { get; set; }

        public int DocumentStatusId { get; set; }

        public int DocumentTypeId { get; set; }

        [Required]
        public string DocumentName { get; set; }

        [Required]
        public string DocumentDescription { get; set; }

        public byte[] DocumentImage { get; set; }

        public DateTime DateUploaded { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual DocumentType DocumentType { get; set; }
    }
}
