namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemParameterType")]
    public partial class SystemParameterType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SystemParameterType()
        {
            SystemParameter = new HashSet<SystemParameter>();
        }

        public int SystemParameterTypeId { get; set; }

        [Required]
        [StringLength(5000)]
        public string SystemParameterTypeName { get; set; }

        [Required]
        [StringLength(5000)]
        public string SystemParameterTypeCShardDataType { get; set; }

        [Required]
        [StringLength(5000)]
        public string SystemParameterTypeSQLDataType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SystemParameter> SystemParameter { get; set; }
    }
}
