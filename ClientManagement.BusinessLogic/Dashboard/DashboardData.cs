using ClientManagement.DataObject;
using ClientManagement.DomainModel;
using ClientManagement.DomainModel.DashBoard;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Techhill.Framework.BusinessProcess;

namespace ClientManagement.BusinessLogic
{
    public class DashboardData : BaseBusinessObject<ClientManagementContext>
    {
        public DashboardData(string connection)
            : base(connection)
        {
        }

        public string GetProfileDetails()
        {
            return ContextDatabase.UserProfile.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserId).Notes;
        }

        public DashBoardDomainModel GetDashBoardDetails()
        {            
            DashBoardDomainModel model = new DashBoardDomainModel();

            var dashBoardDomainModel = ContextDatabase.Database.SqlQuery<DashBoardDomainModel>(string.Format("{0} {1}", "usp_GetDashboardInformationData", this.CurrentUser.UserId)).ToList();
            
            foreach (var item in dashBoardDomainModel)
            {
                model.Number_of_Clients = item.Number_of_Clients;
                model.ClientsBuying = item.ClientsBuying;
                model.OutStandingAmount = item.OutStandingAmount;
                model.Number_Clients_Discontinued = item.Number_Clients_Discontinued;
                model.Number_of_Vehicles_Not_Paid = item.Number_of_Vehicles_Not_Paid;
                model.TotalInvoice = item.TotalInvoice;
                model.TotalExpense = item.TotalExpense;
                model.TotalCount = item.TotalCount;
                model.PaymentAmounts = item.PaymentAmounts;
            }
            return model;
        }

        public object[] DynamicReportDetailsListOne()
        {
            List<object> items = new List<object>();
            ContextDatabase.Database.Connection.Open();
            using (var cmd = ContextDatabase.Database.Connection.CreateCommand())
            {
                cmd.CommandText = string.Format("{0} {1} ", "usp_ListofInvoiceFilter", this.CurrentUser.UserId);

                using (var reader = cmd.ExecuteReader())
                {
                    var headers = GetColumnHeadersFromReader(reader);
                    items.Add(headers);
                }
            }

            var x = items.ToArray();

            return items.ToArray();
        }

        public object[] DynamicReportDetailsListOneOne()
        {
            List<object> items = new List<object>();
            ContextDatabase.Database.Connection.Open();
            using (var cmd = ContextDatabase.Database.Connection.CreateCommand())
            {
                cmd.CommandText = string.Format("{0} {1} ", "usp_ListofInvoiceFilter", this.CurrentUser.UserId);

                using (var reader = cmd.ExecuteReader())
                {
                    var headers = GetColumnHeadersFromReader(reader);
                    while (reader.Read())
                    {
                        items.Add(ReadDataFromSqlReader(reader, headers));
                    }
                }
            }
            return items.ToArray();
        }

        public object[][] DynamicReportDetails()
        {
            List<object[]> items = new List<object[]>();
            ContextDatabase.Database.Connection.Open();
            using (var cmd = ContextDatabase.Database.Connection.CreateCommand())
            {
                cmd.CommandText = string.Format("{0} {1} ", "usp_ListofInvoiceFilter", this.CurrentUser.UserId);

                using (var reader = cmd.ExecuteReader())
                {
                    var headers = GetColumnHeadersFromReader(reader);

                    items.Add(headers);
                    while (reader.Read())
                    {
                        items.Add(ReadDataFromSqlReader(reader, headers));
                    }
                }
            }

            return items.ToArray();
        }

        private string[] GetColumnHeadersFromReader(DbDataReader reader)
        {
            var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
            return columns.ToArray();
        }

        private object[] ReadDataFromSqlReader(DbDataReader reader, string[] columnNames)
        {
            var dataArray = new object[columnNames.Length];
            var index = 0;
            foreach (var column in columnNames)
            {
                dataArray[index] = reader[column];
                index++;
            }

            return dataArray;
        }            

    }
}