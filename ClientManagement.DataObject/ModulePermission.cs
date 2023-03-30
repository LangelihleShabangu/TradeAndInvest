namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ModulePermission")]
    public partial class ModulePermission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ModulePermission()
        {
            ModulePermissionItem = new HashSet<ModulePermissionItem>();
        }

        public int ModulePermissionID { get; set; }

        public bool Active { get; set; }

        public int RoleModuleID { get; set; }

        public int ModuleObjectID { get; set; }

        public virtual ModuleObject ModuleObject { get; set; }

        public virtual RoleModule RoleModule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModulePermissionItem> ModulePermissionItem { get; set; }
    }
}
