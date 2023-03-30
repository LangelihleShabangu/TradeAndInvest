namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AuditLog")]
    public partial class AuditLog
    {
        public int AuditLogId { get; set; }

        public DateTime EventDate { get; set; }

        public int PrimaryKeyID { get; set; }

        [Required]
        public string EventData { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Table { get; set; }

        [Required]
        [StringLength(5000)]
        public string Columns { get; set; }

        [Required]
        [StringLength(600)]
        public string Action { get; set; }
    }
}
