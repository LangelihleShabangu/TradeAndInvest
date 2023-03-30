namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subscriptions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subscriptions()
        {
            Assessment = new HashSet<Assessment>();
        }

        public int SubscriptionsId { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string Surname { get; set; }

        [Required]
        [StringLength(255)]
        public string Woman_Owned_Business { get; set; }

        [Required]
        [StringLength(255)]
        public string CompanyName { get; set; }

        public int Years_in_Operation { get; set; }

        [Required]
        [StringLength(255)]
        public string Exporter { get; set; }

        [Required]
        [StringLength(255)]
        public string Planning_to_Export { get; set; }

        [Required]
        [StringLength(255)]
        public string Which_Region { get; set; }

        [Required]
        [StringLength(800)]
        public string Country { get; set; }

        [Required]
        [StringLength(50)]
        public string Contact_Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone_Number { get; set; }

        [Required]
        [StringLength(50)]
        public string Email_Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Address_Line1 { get; set; }

        [Required]
        [StringLength(50)]
        public string Address_Line2 { get; set; }

        [Required]
        [StringLength(50)]
        public string Address_Line3 { get; set; }

        [Required]
        [StringLength(50)]
        public string Address_Line4 { get; set; }

        [Required]
        [StringLength(50)]
        public string Company_Based { get; set; }

        public byte[] Company_Profile { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assessment> Assessment { get; set; }
    }
}
