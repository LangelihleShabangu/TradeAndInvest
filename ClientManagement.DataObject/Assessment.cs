namespace ClientManagement.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Assessment")]
    public partial class Assessment
    {
        public int AssessmentId { get; set; }

        public int SubscriptionsId { get; set; }

        [Required]
        [StringLength(550)]
        public string Specify_Company_Location { get; set; }

        [Required]
        [StringLength(200)]
        public string Exporter_Region { get; set; }

        [Required]
        [StringLength(550)]
        public string Exporter_Country { get; set; }

        public int Number_of_years_you_have_exported { get; set; }

        public int Number_of_Company_employees { get; set; }

        public decimal Companys_annual_turnover { get; set; }

        [Required]
        [StringLength(550)]
        public string Industry_Sector { get; set; }

        [Required]
        [StringLength(550)]
        public string Export_products { get; set; }

        [Required]
        [StringLength(550)]
        public string Export_markets { get; set; }

        [Required]
        [StringLength(550)]
        public string Regional_export_markets_future_expand { get; set; }

        [Required]
        [StringLength(800)]
        public string Special_technical_support { get; set; }

        [Required]
        [StringLength(155)]
        public string Country_Expand { get; set; }

        [Required]
        [StringLength(155)]
        public string Government_export_assistance { get; set; }

        [Required]
        [StringLength(255)]
        public string specify_location_Assistance { get; set; }

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

        [Required]
        [StringLength(950)]
        public string Additional_Comments { get; set; }

        [Required]
        [StringLength(950)]
        public string Challenges_facing_Exporter { get; set; }

        [Required]
        [StringLength(550)]
        public string Other_issues { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Subscriptions Subscriptions { get; set; }
    }
}
