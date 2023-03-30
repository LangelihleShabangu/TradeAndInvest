using ClientManagement.DomainModel;
using EmployeesManagement.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagement.DomainModel
{
    public class RegistrationModel 
    {
        public RegistrationModel()
        {            
            AddressDetails = new AddressModel();                
            GenderList = new List<GenderModel>();
            TitleList = new List<TitleModel>();           
            ContactInformationDetails = new ContactInformationModel();           
        }
              
        public string Name { get; set; }
        public bool IsExporter { get; set; }
        public bool IsplanningExport { get; set; }
        public int RegistrationId { get; set; }        
        public int ContactInformationId { get; set; }
        public int ProductId { get; set; }        
        public int CompanyId { get; set; }
        public DateTime InceptionDate { get; set; }         
        public List<TitleModel> TitleList { get; set; }
        public List<GenderModel> GenderList { get; set; }
        public ContactInformationModel ContactInformationDetails { get; set; }
        public AddressModel AddressDetails { get; set; }         
        public DateTime StartDate { get; set; }        
        public string Email { get; set; }

        public string AreaId { get; set; }

        public int GenderId { get; set; }

        public string FirstName { get; set; }

        public string PhoneNumber { get; set; }

        public string CellNumber { get; set; }

        public string Surname { get; set; }

        public string IdNumber { get; set; }

        public int AddressId { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string AddressLine4 { get; set; }
        public int EmployeeLeaveId { get; set; }
        public int LeaveTypeId { get; set; } 
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; }
        public int Days { get; set; }
        public int IsApproved { get; set; }

        public byte[] Documentation { get; set; } 
    }
}
