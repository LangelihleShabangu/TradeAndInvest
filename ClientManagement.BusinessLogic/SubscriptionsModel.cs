using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.DomainModel
{
    public class SubscriptionsModel
    {       
        public string AddressLineCity { get; set; }
        public SubscriptionsModel()
        {
            SubscriptionsList = new List<SubscriptionsModel>();
            AreaList = new List<AreaModel>();           
        }
        
       public string Notes { get; set; }

       public int IndustryId { get; set; }
        
       public List<IndustryModel> IndustryList { get; set; }

       public List<AreaModel> AreaList { get; set; }
        public int AreaId { get; set; }  
       public int SubscriptionsId{ get ; set ;}
       public string FirstName{ get ; set ;}
       public string Surname{ get ; set ;}
       public string Woman_Owned_Business{ get ; set ;}
       public string CompanyName{ get ; set ;}
       public int Years_in_Operation{ get ; set ;}
       public string Exporter{ get ; set ;}
       public string Planning_to_Export{ get ; set ;}
       public string Product_Services { get; set; }
       public string Which_Region { get; set; }
       public string Country{ get ; set ;}
       public string Contact_Phone{ get ; set ;}
       public string Phone_Number{ get ; set ;}
       public string Email_Address{ get ; set ;}
       public string Address_Line1{ get ; set ;}
       public string Address_Line2{ get ; set ;}
       public string Address_Line3{ get ; set ;}
       public string Address_Line4{ get ; set ;}
       public string Company_Based{ get ; set ;}
       public string Company_Profile	{ get ; set ;}
       public byte[] Documentation { get; set; }
       
       public List<SubscriptionsModel> SubscriptionsList { get; set; }
        public string AddressLineState_Province { get; set; }
        public string AddressLinePostalCode { get; set; }
        public string AddressLineCountry { get; set; }
        public Guid CRMAccountId { get; set; }
        public Guid CRMContactId { get; set; }
        public int UserProfileId { get; set; }
        public string DocumentLocationURL { get; set; }
    }
}
