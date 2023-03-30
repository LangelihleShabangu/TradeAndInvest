using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.DomainModel
{
    public class AssessmentModel : SubscriptionsModel
    {
        public AssessmentModel()
        {
            AssessmentList = new List<AssessmentModel>();           
        }

       public int AssessmentId { get; set; }         
       public string Specify_Company_Location { get; set; }
       public string Exporter_Region { get; set; }
       public string Exporter_Country { get; set; }
       public int Number_of_years_you_have_exported { get; set; }
       public int Number_of_Company_employees { get; set; }
       public decimal Companys_annual_turnover { get; set; }
       public string Industry_Sector { get; set; }
       public string Export_products { get; set; }
       public string Export_markets { get; set; }
       public string Regional_export_markets_future_expand { get; set; }
       public string Country_Expand { get; set; }
       public string Government_export_assistance { get; set; }
       public string specify_location_Assistance { get; set; }
       public string Technical_Support { get; set; } 
       public bool chkExporter_Education_Seminar { get; set; }
       public bool chkParticipate_at_international_Trade { get; set; }
       public bool chkMarket_Research { get; set; }
       public bool chkGuidance_on_export { get; set; }
       public bool chkFinancial_Assistance { get; set; }
       public bool chkConsultation_with_trade_specialist { get; set; }
       public bool chkInternational_Trade_Missions { get; set; }
       public bool chkTrade_Leads { get; set; }
       public bool chkShipping_Logistics_consulting { get; set; }
       public bool chkOther { get; set; }
       public string Additional_Comments { get; set; }
       public string Challenges_facing_Exporter { get; set; }
       public string Other_issues { get; set; }
       public List<AssessmentModel> AssessmentList { get; set; }
       public string Special_technical_support { get; set; }
        public Guid CRMContactId { get; internal set; }
        public string AddressLineCity { get; internal set; }
        public string AddressLineState_Province { get; internal set; }
        public string AddressLinePostalCode { get; internal set; }
        public string AddressLineCountry { get; internal set; }
        public int UserProfileId { get; internal set; }
        public string DocumentLocationURL { get; internal set; }
        public Guid CRMAccountId { get; internal set; }
    }
}
