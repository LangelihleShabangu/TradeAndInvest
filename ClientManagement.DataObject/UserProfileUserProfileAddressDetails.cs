namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserProfileUserProfileAddressDetails
    {
        [Key]
        public int UserProfileAddressDetailsId { get; set; }

        public int UserProfileId { get; set; }

        public int AddressDetailsId { get; set; }

        public Guid CRMAccountId { get; set; }

        public Guid CRMContactId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual AddressDetails AddressDetails { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
