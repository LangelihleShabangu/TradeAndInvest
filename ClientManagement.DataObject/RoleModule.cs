namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoleModule")]
    public partial class RoleModule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoleModule()
        {
            ModulePermission = new HashSet<ModulePermission>();
        }

        public int RoleModuleID { get; set; }

        public int ModuleID { get; set; }

        public int RoleID { get; set; }

        public bool Active { get; set; }

        public virtual Module Module { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModulePermission> ModulePermission { get; set; }

        public virtual Role Role { get; set; }
    }
}
