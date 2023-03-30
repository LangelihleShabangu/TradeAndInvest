namespace ClientManagement.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DocumentsModel
    {
        public DocumentsModel()
        {
            DocumentsList = new List<DocumentsModel>();
            DocumentTypeList = new List<DocumentTypeModel>();
            DocumentStatusList = new List<DocumentStatusModel>();
        }

        public int DocumentsId { get; set; }

        public int DocumentStatusId { get; set; }

        public int DocumentTypeId { get; set; }       

        public string DocumentStatus { get; set; }

        public string DocumentType { get; set; }

        public string DocumentName { get; set; }
       
        public string DocumentDescription { get; set; }

        public byte[] DocumentImage { get; set; }

        public DateTime DateUploaded { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsExpired { get; set; }

        public List<DocumentsModel> DocumentsList { get; set; }

        public List<DocumentStatusModel> DocumentStatusList { get; set; }

        public List<DocumentTypeModel> DocumentTypeList { get; set; }
    }
}
