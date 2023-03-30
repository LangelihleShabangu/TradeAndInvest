namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ModulePermissionItem")]
    public partial class ModulePermissionItem
    {
        public int ModulePermissionItemID { get; set; }

        public int ModuleObjectItemId { get; set; }

        public int ModulePermissionID { get; set; }

        public bool Active { get; set; }

        public virtual ModuleObjectItem ModuleObjectItem { get; set; }

        public virtual ModulePermission ModulePermission { get; set; }
    }
}
