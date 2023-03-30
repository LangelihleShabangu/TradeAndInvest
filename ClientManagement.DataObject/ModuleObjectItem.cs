namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ModuleObjectItem")]
    public partial class ModuleObjectItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ModuleObjectItem()
        {
            ModulePermissionItem = new HashSet<ModulePermissionItem>();
        }

        public int ModuleObjectItemId { get; set; }

        public int ModuleObjectID { get; set; }

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

        public virtual ModuleObject ModuleObject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModulePermissionItem> ModulePermissionItem { get; set; }
    }
}
