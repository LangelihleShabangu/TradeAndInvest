using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.DomainModel
{
    
    public class DocumentModel
    {
        public DocumentModel()
        {
            DocumentModelList = new List<DocumentModel>();
            //ContactInformationDataList = new List<ClientList>();
        }

        public int DocumentId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public byte[] CurrentDocument { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public List<DocumentModel> DocumentModelList { get; set; }

        public int ClientId { get; set; }

        public string Client { get; set; }

        //public List<ClientList> ContactInformationDataList { get; set; }
    }
}
