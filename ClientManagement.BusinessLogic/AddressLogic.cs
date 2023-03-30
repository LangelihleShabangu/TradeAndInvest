using System.Collections.Generic;
using System.Linq;
using ClientManagement.DomainModel;
using ClientManagement.DataObject;
using Techhill.Framework.BusinessProcess;

namespace ClientManagement.BusinessLogic
{
    public class AddressLogic : BaseBusinessObject<ClientManagementContext> 
    {
        public AddressLogic(string connection)
            : base(connection)
        {                                                                
        }

        public List<AddressModel> GetAddressDetails(int AddressId)
        {
            var model = new List<AddressModel>();
            var AddressDetails = ContextDatabase.Address.Where(f => f.AddressId == AddressId).ToList();
            Mapper.MapObjects<Address, AddressModel>(AddressDetails,ref model);
            return model;
        }

        public int SaveAddress(AddressModel AddressModel)
        {
            var AddressData = ContextDatabase.Address.FirstOrDefault(f => !f.IsDeleted && f.AddressId == AddressModel.AddressId);
            if (AddressData == null)
            {
                AddressData = new Address();
                ContextDatabase.Address.Add(AddressData);                
            }            
           
            AddressData.AddressLine1 = AddressModel.AddressLine1;
            AddressData.AddressLine2 = AddressModel.AddressLine2;
            AddressData.AddressLine3 = AddressModel.AddressLine3;
            AddressData.AreaId = AddressData.AreaId == 0 ? ContextDatabase.Area.FirstOrDefault(f => !f.IsDeleted).AreaId : AddressData.AreaId;
            AddressData.AddressLine4 = AddressModel.AddressLine4;
            AddressData.IsDeleted = false;            
            ContextDatabase.SaveChanges();
            return AddressData.AddressId;
        }
    }
}
