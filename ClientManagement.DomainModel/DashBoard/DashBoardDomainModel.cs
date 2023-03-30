using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.DomainModel.DashBoard
{
    public class DashBoardDomainModel 
    {

        public DashBoardDomainModel()
        {
            LocationModel = new List<LocationDomainModel>();
            //DashBoardGraphList = new List<DashBoardDomainModel>();
            //DashBoardGraphList = GetData();
        }
        public object[][] DynamicReport { get; set; }
        public int Number_of_Clients { get; set; }
        public int Number_of_Vehicles_Not_Paid { get; set; }
        public int OutStandingAmount { get; set; }
        public int ClientsBuying { get; set; }
        public int Number_Clients_Discontinued { get; set; }
        public decimal MaterialAmount { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal Amount { get; set; }

        public decimal TotalInvoice { get; set; }
        public decimal TotalExpense { get; set; }
        public int TotalCount { get; set; }
        
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public List<LocationDomainModel> LocationModel { get; set; }
        public List<DashBoardDomainModel> DashBoardGraphList { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Enddate { get; set; }

        public DateTime CashPaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal PaymentAmounts { get; set; } 

        public DashBoardDomainModel(string label, double value1)
        {
            this.Label = label;
            this.Value1 = value1;
        }

        public DashBoardDomainModel(string label, double value1, double value2)
        {
            this.Label = label;
            this.Value1 = value1;
            this.Value2 = value2;
        }

        public string Label { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
    }

    public class LocationDomainModel
    {
        public int LocationId {get;set;}      
        public string LocationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }       
    }

    public class NewDashboardDomainModel
    {
        public int TOTAL_TESTED { get; set; }
        public int TOTAL_REACH { get; set; }
        public int TOTAL_MSMTG { get; set; }
        public int PATIENTS_IN_CLUBS { get; set; }

        public List<NewDashboardDomainModel> NewDashboardModel { get; set; }
    }

    public class Company
    {
        public long Id { get; set; }
        public string Year { get; set; }
        public decimal Salary { get; set; }
        public decimal Expense { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
    }
}

