using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.DomainModel
{
    public class TitleModel
    {
        public TitleModel()
        {
            TitleList = new List<TitleModel>();
        }
        public int TitleId { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        public List<TitleModel> TitleList { get; set; }
    }
}
