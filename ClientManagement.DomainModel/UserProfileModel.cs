namespace ClientManagement.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class UserProfileModel
    {
        public UserProfileModel()
        {
            UserProfileList = new List<UserProfileModel>();            
        }

        public int UserProfileId { get; set; }

        public int UserId { get; set; }

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

        public List<UserProfileModel> UserProfileList { get; set; }  
       
    }
}
