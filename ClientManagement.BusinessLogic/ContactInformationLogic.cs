using System;
using System.Collections.Generic;
using System.Linq;
using ClientManagement.DomainModel;
using ClientManagement.DataObject;
using Techhill.Framework.BusinessProcess;

namespace ClientManagement.BusinessLogic
{ 
    public class ContactInformationLogic : BaseBusinessObject<ClientManagementContext> 
    {
        public ContactInformationLogic(string connection)
            : base(connection)
        {                                                                
        }

        public List<ContactInformationModel> GetContactInformationDetails(int ContactInformationId)
        {
            var model = new List<ContactInformationModel>();
            var contactInformationDetails = ContextDatabase.ContactInformation.Where(f => f.ContactInformationId == ContactInformationId).ToList();
            Mapper.MapObjects<ContactInformation, ContactInformationModel>(contactInformationDetails, ref model);
            return model;
        }

        public int SaveContactInformation(ContactInformationModel contactInformationModel)
        {
            var ContactInformationData = ContextDatabase.ContactInformation.FirstOrDefault(f => !f.IsDeleted && f.ContactInformationId == contactInformationModel.ContactInformationId);
            if (ContactInformationData == null)
            {
                ContactInformationData = new ContactInformation();
                ContextDatabase.ContactInformation.Add(ContactInformationData);
                ContactInformationData.CreatedDate = System.DateTime.Now;
                ContactInformationData.CreatedBy = this.CurrentUser.UserId;
                ContactInformationData.ModifiedBy = this.CurrentUser.UserId;
                ContactInformationData.ModifiedDate = System.DateTime.Now;
            }
            else
            {
                ContactInformationData.ModifiedBy = this.CurrentUser.UserId;
                ContactInformationData.ModifiedDate = System.DateTime.Now;
            }

            ContactInformationData.CellNumber = string.IsNullOrEmpty(contactInformationModel.CellNumber) ? "0000000000" : contactInformationModel.CellNumber;
            ContactInformationData.Surname = contactInformationModel.Surname;
            ContactInformationData.IdNumber = contactInformationModel.IdNumber;
            ContactInformationData.FirstName = contactInformationModel.FirstName;

            ContactInformationData.TitleId = contactInformationModel.TitleId == 0 ? 1 : contactInformationModel.TitleId;
            ContactInformationData.GenderId = contactInformationModel.GenderId == 0 ? 1 : contactInformationModel.GenderId;
            ContactInformationData.BirthDate = contactInformationModel.BirthDate.Year == 0001 ? System.DateTime.Now : contactInformationModel.BirthDate;
            ContactInformationData.PassportNumber = string.IsNullOrEmpty(contactInformationModel.PassportNumber) ? "None" : contactInformationModel.PassportNumber;
            ContactInformationData.EmailAddress = string.IsNullOrEmpty(contactInformationModel.EmailAddress) ? "None" : contactInformationModel.EmailAddress;
            ContactInformationData.KnownAs = string.IsNullOrEmpty(contactInformationModel.KnownAs) ? "None" : contactInformationModel.KnownAs;
            ContactInformationData.PhoneNumber = string.IsNullOrEmpty(contactInformationModel.PhoneNumber) ? "00000000000" : contactInformationModel.PhoneNumber;

            contactInformationModel.AddressDetails.AddressId = contactInformationModel.AddressId;
            ContactInformationData.IsDeleted = false;
            ContactInformationData.AddressId = new AddressLogic(ConnectionString).SaveAddress(contactInformationModel.AddressDetails);

            ContextDatabase.SaveChanges();
            return ContactInformationData.ContactInformationId;
        }

        public int SaveContactInformation(ContactInformationDetailsModel ContactInformationModel)
        {
            var ContactInformationData = ContextDatabase.ContactInformation.FirstOrDefault(f => !f.IsDeleted && f.ContactInformationId == ContactInformationModel.ContactInformationId);
            if (ContactInformationData == null)
            {
                ContactInformationData = new ContactInformation();
                ContextDatabase.ContactInformation.Add(ContactInformationData);
            }
            ContactInformationData.CellNumber = ContactInformationModel.CellNumber;
            ContactInformationData.Surname = ContactInformationModel.Surname;
            ContactInformationData.IdNumber = ContactInformationModel.IdNumber;
            ContactInformationData.FirstName = ContactInformationModel.FirstName;

            ContactInformationData.TitleId = 1;
            ContactInformationData.GenderId = 1;
            ContactInformationData.BirthDate = DateTime.Now;
            ContactInformationData.PassportNumber = ContactInformationModel.PassportNumber;
            ContactInformationData.EmailAddress = "None";
            ContactInformationData.KnownAs = "None";
            ContactInformationData.PhoneNumber = "None";            

            ContactInformationData.FirstName = ContactInformationModel.FirstName;
            ContactInformationData.IsDeleted = false;
            ContactInformationData.AddressId = new AddressLogic(ConnectionString).SaveAddress(ContactInformationModel.AddressDetails);
            
            ContextDatabase.SaveChanges();
            return ContactInformationData.ContactInformationId;
        }
    }
}
