using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.DomainModel
{
    public class DocumentStatusModel
    {
        public DocumentStatusModel()
        {
            DocumentStatusList = new List<DocumentStatusModel>();
        }
        public int DocumentStatusId { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        public List<DocumentStatusModel> DocumentStatusList { get; set; }
    }
}
