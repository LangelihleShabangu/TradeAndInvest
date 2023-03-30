using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.DomainModel
{
    public class LeadsModel
    {
        public LeadsModel()
        {
            LeadsList = new List<LeadsModel>();
        }

        public int LeadsId	{get;set;}		
        public int CreatedBy {get;set;}		
        public DateTime Created {get;set;}		
        public int LastModifiedBy {get;set;}		
        public int LastModified	{get;set;}		
        public int CrmLeadId {get;set;}
        public string Topic { get; set; }
        public string VendorNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }		
        public int DwellingTypeId {get;set;}
        public string EstateName { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }		
        public int PostalCode {get;set;}
        public string Status { get; set; }
        public string Comments { get; set; }		
        public int ProvinceId{get;set;}
        public string CrmUri { get; set; }		
        public int AccountId {get;set;}
        public string ReferenceNumber { get; set; }

        public List<LeadsModel> LeadsList { get; set; }
    }
}
