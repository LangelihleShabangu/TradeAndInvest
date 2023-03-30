namespace ClientManagement.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ClientManagement.DomainModel;

    public class ContactInformationDetailsModel
    {
        public ContactInformationDetailsModel()
        {
            ContactInformationModelList = new List<ContactInformationDetailsModel>();
           
            AddressDetails = new AddressModel();
        }

        public int ContactInformationId { get; set; }

        public int GenderId { get; set; }

        public int TitleId { get; set; }

        public int CompanyId { get; set; }         

        [Required(ErrorMessage = "CellNumber is required!")]
        [StringLength(20)]
        public string CellNumber { get; set; }   

        [Required(ErrorMessage = "FirstName is required!")]
        [StringLength(255)]
        public string FirstName { get; set; }
             
        private string _Surname;
        [Required(ErrorMessage = "Surname is required!")]
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
        
        public bool IsDeleted { get; set; }

        
        public List<ContactInformationDetailsModel> ContactInformationModelList { get; set; }

        public AddressModel AddressDetails { get; set; }

        public AddressModel AddressDomicile { get; set; }

        public int BankId { get; set; }

        public int BankAccountTypeId { get; set; }
    }
}
