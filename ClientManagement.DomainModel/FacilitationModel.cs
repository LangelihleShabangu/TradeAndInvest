using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.DomainModel
{
    public class FacilitationModel 
    {
        //public FacilitationModel()
        //{
        //    Facilitation = new FacilitationModel();
        //    FacilitationsList = new List<FacilitationModel>();          
        //}

        public FacilitationModel Facilitation { get; set; }

        public List<FacilitationModel> FacilitationsList { get; set; }      

        public string InvestmentIndustries { get; set; }

        public int FacilitationId { get; set; }

        public DateTime InceptionDate { get; set; }        

        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public string ProjectChallangesConstraints { get; set; }

        public string Expectations { get; set; }

        public decimal InvestmentValue { get; set; }

        public int JobsOpportunities { get; set; }

        public int Permanent { get; set; }

        public int Temporary { get; set; }

        public int Totals { get; set; }

        public string ProjectStatus { get; set; }

        public string RelevantDepartments { get; set; }

        public string FuturePlans { get; set; }

        public string ProjectManagerName { get; set; }

        public string CellNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Comments { get; set; }

        public string NeededAssistance { get; set; } 

        public DateTime CreatedDate { get; set; }
        public string Notes { get; set; }
        public Guid CRMOpportunityId { get; set; }
        public Guid CRMAccountId { get; set; }
        public Guid CRMContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cellphone { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLineCity { get; set; }
        public string AddressLineState_Province { get; set; }
        public string AddressLinePostalCode { get; set; }
        public string AddressLineCountry { get; set; }
    }
}
