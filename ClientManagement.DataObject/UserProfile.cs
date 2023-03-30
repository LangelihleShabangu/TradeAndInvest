namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserProfile")]
    public partial class UserProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserProfile()
        {
            Account = new HashSet<Account>();
            UserProfileUserProfileAddressDetails = new HashSet<UserProfileUserProfileAddressDetails>();
            UserRole = new HashSet<UserRole>();
        }

        public int UserProfileId { get; set; }

        [Required]
        [StringLength(300)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(13)]
        public string Cellphone { get; set; }

        [Required]
        [StringLength(13)]
        public string Telephone { get; set; }

        public bool SignupComplete { get; set; }

        public DateTime LastLoginDate { get; set; }

        public DateTime LastPasswordChangedDate { get; set; }

        public bool IsLockedOut { get; set; }

        public DateTime LastLockedOutDate { get; set; }

        public bool PasswordChangeRequired { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public bool Approved { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileUserProfileAddressDetails> UserProfileUserProfileAddressDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
