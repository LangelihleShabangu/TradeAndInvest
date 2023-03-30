using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.DomainModel
{
    public class IndustryModel
    {
        public IndustryModel()
        {
            IndustryList = new List<IndustryModel>();
        }
        public int IndustryId { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<IndustryModel> IndustryList { get; set; }
    }
}
