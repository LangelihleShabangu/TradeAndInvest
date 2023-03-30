namespace ClientManagement.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema; 

    public class AddressModel
    {
        public AddressModel()
        {
            AddressModelList = new List<AddressModel>();
        }

        public int AddressId { get; set; }

        public int AreaId { get; set; }
        

        [Required(ErrorMessage = "Address Line 1 is required!")]
        private string _AddressLine1;
        public string AddressLine1
        {
            get
            {
                if (string.IsNullOrEmpty(_AddressLine1))
                    return "None";
                else
                    return _AddressLine1;
            }
            set { _AddressLine1 = value; }
        }

        [Required(ErrorMessage = "Address Line 2 is required!")]
        private string _AddressLine2;
        public string AddressLine2
        {
            get
            {
                if (string.IsNullOrEmpty(_AddressLine2))
                    return "None";
                else
                    return _AddressLine2;
            }
            set { _AddressLine2 = value; }
        }

        [Required(ErrorMessage = "Address Line 3 is required!")]
        private string _AddressLine3;
        public string AddressLine3
        {
            get
            {
                if (string.IsNullOrEmpty(_AddressLine3))
                    return "None";
                else
                    return _AddressLine3;
            }
            set { _AddressLine3 = value; }
        }

        private string _AddressLine4;
        public string AddressLine4
        {
            get
            {
                if (string.IsNullOrEmpty(_AddressLine4))
                    return "None";
                else
                    return _AddressLine4;
            }
            set { _AddressLine4 = value; }
        }        
       
        public bool IsDeleted { get; set; }

        public List<AddressModel> AddressModelList { get; set; }
    }
}
