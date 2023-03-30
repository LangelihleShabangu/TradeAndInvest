namespace ClientManagement.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ClientManagement.DomainModel;

    public class ContactInformationModel
    {
        public ContactInformationModel()
        {
            ContactInformationModelList = new List<ContactInformationModel>();
            //BusinessTypeList = new List<BusinessTypeModel>();
            AddressDetails = new AddressModel();
        }

        public int ContactInformationId { get; set; }

        public int GenderId { get; set; }

        public int TitleId { get; set; }

        public int AddressId { get; set; }        

        private string _CellNumber;
        public string CellNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_CellNumber))
                    return "None";
                else
                    return _CellNumber;
            }
            set { _CellNumber = value; }
        }

        [Required(ErrorMessage = "EmailAddress is required!")]
        [StringLength(220)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required!")]
        [StringLength(220)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "FirstName is required!")]
        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        private string _KnownAs;
        public string KnownAs
        {
            get
            {
                if (string.IsNullOrEmpty(_KnownAs))
                    return "None";
                else
                    return _KnownAs;
            }
            set { _KnownAs = value; }
        }

        private string _Surname;
        //[Required(ErrorMessage = "Surname is required!")]
        public string Surname
        {
            get
            {
                if (string.IsNullOrEmpty(_Surname))
                    return "None";
                else
                    return _Surname;
            }
            set { _Surname = value; }
        }

        private string _IdNumber;
        [Required(ErrorMessage = "IdNumber is required!")]
        public string IdNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_IdNumber))
                    return "0000000000000";
                else
                    return _IdNumber;
            }
            set { _IdNumber = value; }
        }

        private string _PassportNumber;
        [Required(ErrorMessage = "PassportNumber is required!")]
        public string PassportNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_PassportNumber))
                    return "0000000000000";
                else
                    return _PassportNumber;
            }
            set { _PassportNumber = value; }
        }

        //public DateTime BirthDate { get; set; }

        [StringLength(255)]
        private DateTime _BirthDate;
        public DateTime BirthDate
        {
            get
            {               
                return DateTime.Now;               
            }
            set { _BirthDate = value; }
        }

        public int PostalAddressID { get; set; }

        public bool IsDeleted { get; set; }

        //public List<BusinessTypeModel> BusinessTypeList { get; set; }

        public List<ContactInformationModel> ContactInformationModelList { get; set; }

        public AddressModel AddressDetails { get; set; }
    }
}
