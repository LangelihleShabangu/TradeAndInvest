using ClientManagement.DataObject;
using ClientManagement.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Techhill.Framework.BusinessProcess;

namespace ClientManagement.BusinessLogic
{
    public class DocumentsLogic : BaseBusinessObject<ClientManagementContext>
    {
        public DocumentsLogic(string connection)
            : base(connection)
        {
        }

        public DocumentsModel GetDocuments()
        {
            DocumentsModel model = new DocumentsModel();
            model.DocumentsList = ContextDatabase.Database.SqlQuery<DocumentsModel>(string.Format("{0} {1}", "usp_DocumentsFilter", this.CurrentUser.UserId)).ToList();

            List<DocumentTypeModel> DocumentTypeList = new List<DocumentTypeModel>();
            var CompanysDetailList = ContextDatabase.DocumentType.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.DocumentType, DocumentTypeModel>(CompanysDetailList, ref DocumentTypeList);

            model.DocumentTypeList = DocumentTypeList.ToList();

            return model;
        }

        public DocumentsModel AddDocuments()
        {
            DocumentsModel model = new DocumentsModel();
            model.DocumentsList = ContextDatabase.Database.SqlQuery<DocumentsModel>("usp_GetClientInformation").ToList();            
            return model;
        }

        public int AddDocuments(DocumentsModel model)
        {
            var DocumentType = ContextDatabase.DocumentType.FirstOrDefault(f => f.DocumentTypeId == model.DocumentTypeId);

            var DocumentInfo = ContextDatabase.Documents.FirstOrDefault(f => f.DocumentsId == model.DocumentsId);
            if (DocumentInfo == null)
            {
                DocumentInfo = new Documents();
                ContextDatabase.Documents.Add(DocumentInfo);

                DocumentInfo.CreatedDate = System.DateTime.Now;
                DocumentInfo.CreatedBy = this.CurrentUser.UserId;
                DocumentInfo.ModifiedBy = this.CurrentUser.UserId;
                DocumentInfo.ModifiedDate = System.DateTime.Now;
            }
            else
            {
                DocumentInfo.ModifiedBy = this.CurrentUser.UserId;
                DocumentInfo.ModifiedDate = System.DateTime.Now;
            }

            DocumentInfo.DocumentName = model.DocumentName;
            DocumentInfo.DocumentDescription = model.DocumentDescription;
            DocumentInfo.DocumentStatusId = ContextDatabase.DocumentStatus.FirstOrDefault().DocumentStatusId;
            DocumentInfo.DocumentTypeId = model.DocumentTypeId;
            DocumentInfo.DateUploaded = DateTime.Now;
            DocumentInfo.ExpirationDate = DocumentType.CreatedBy == 0 ? Convert.ToDateTime("10 Jan 1900") : DateTime.Now.AddDays(DocumentType.CreatedBy);
            DocumentInfo.DocumentImage = model.DocumentImage;

            DocumentInfo.CreatedDate = DateTime.Now;
            DocumentInfo.IsDeleted = false;
            ContextDatabase.SaveChanges();
            return DocumentInfo.DocumentsId;
        }

        public DocumentsModel GetDocumentDetail(int DocumentId)
        {
            DocumentsModel model = new DocumentsModel();
            var DocumentDetail = ContextDatabase.Documents.FirstOrDefault(f => f.DocumentsId == DocumentId);
            Mapper.MapObjects(DocumentDetail, ref model);            

            return model;
        }

        public DocumentsModel GetDocumentDetails()
        {
            DocumentsModel model = new DocumentsModel();

            //model.DocumentsModelList = ContextDatabase.Database.SqlQuery<DocumentsModel>("usp_GetDocumentInformation").ToList(); 
            return model;
        }        
    }
}
