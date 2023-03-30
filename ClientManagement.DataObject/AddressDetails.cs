namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AddressDetails
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AddressDetails()
        {
            UserProfileUserProfileAddressDetails = new HashSet<UserProfileUserProfileAddressDetails>();
        }

        public int AddressDetailsId { get; set; }

        [Required]
        [StringLength(220)]
        public string AddressLine1 { get; set; }

        [Required]
        [StringLength(220)]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(220)]
        public string AddressLine3 { get; set; }

        [Required]
        [StringLength(220)]
        public string AddressLineCity { get; set; }

        [Required]
        [StringLength(220)]
        public string AddressLineState_Province { get; set; }

        [Required]
        [StringLength(20)]
        public string AddressLinePostalCode { get; set; }

        [Required]
        [StringLength(20)]
        public string AddressLineCountry { get; set; }

        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileUserProfileAddressDetails> UserProfileUserProfileAddressDetails { get; set; }
    }
}
