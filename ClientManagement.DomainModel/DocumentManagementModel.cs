using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.DomainModel
{
    public class DocumentManagementModel
    {
        public DocumentManagementModel()
        {
            DocumentManagementList = new List<DocumentManagementModel>();
            DocumentImage = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            DocumentTypeList = new List<DocumentTypeModel>();
        }
        
        public List<DocumentTypeModel> DocumentTypeList { get; set; }
        public int DocumentManagementId { get; set; }

        public int FacilitationId { get; set; }
        public int DocumentTypeId { get; set; }

        public string DocumentPath { get; set; }

        public string DocumentType { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }      

        public string DocumentName { get; set; }

        public Guid CRMDocumentManagementId { get; set; }

        public DateTime CreatedDate { get; set; }        

        public byte[] DocumentImage { get; set; }

        public string ContentType { get; set; }

        public List<DocumentManagementModel> DocumentManagementList { get; set; }
        public Guid CRMAccountId { get; set; }
        public Guid CRMContactId { get; set; }
        public string DocumentLocationURLs { get; set; }
        public Guid CRMLeadId { get; set; }
    }
}
