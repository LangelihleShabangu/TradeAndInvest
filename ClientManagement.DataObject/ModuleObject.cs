namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ModuleObject")]
    public partial class ModuleObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ModuleObject()
        {
            ModuleObjectItem = new HashSet<ModuleObjectItem>();
            ModulePermission = new HashSet<ModulePermission>();
        }

        public int ModuleObjectID { get; set; }

        public int ModuleID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool Active { get; set; }

        public bool IsMenuItem { get; set; }

        [Required]
        [StringLength(50)]
        public string Action { get; set; }

        [Required]
        [StringLength(50)]
        public string Controller { get; set; }

        [Required]
        [StringLength(500)]
        public string CssClass { get; set; }

        public int OrderId { get; set; }

        public virtual Module Module { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModuleObjectItem> ModuleObjectItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModulePermission> ModulePermission { get; set; }
    }
}
