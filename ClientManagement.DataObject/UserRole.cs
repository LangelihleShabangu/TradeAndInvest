namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserRole")]
    public partial class UserRole
    {
        public int UserRoleID { get; set; }

        public int UserProfileID { get; set; }

        public int RoleID { get; set; }

        public bool Active { get; set; }

        public virtual Role Role { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
