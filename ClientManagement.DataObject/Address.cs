namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Address()
        {
            ContactInformation = new HashSet<ContactInformation>();
        }

        public int AddressId { get; set; }

        public int AreaId { get; set; }

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
        [StringLength(20)]
        public string AddressLine4 { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Area Area { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContactInformation> ContactInformation { get; set; }
    }
}
