namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemParameter")]
    public partial class SystemParameter
    {
        public int SystemParameterId { get; set; }

        public int SystemParameterTypeId { get; set; }

        [Required]
        [StringLength(5000)]
        public string SystemParameterName { get; set; }

        [Required]
        [StringLength(5000)]
        public string SystemParameterDescription { get; set; }

        [Required]
        public string SystemParameterValue { get; set; }

        public virtual SystemParameterType SystemParameterType { get; set; }
    }
}
