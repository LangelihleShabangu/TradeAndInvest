using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.DomainModel
{
    public class GenderModel
    {
        public GenderModel()
        {
            GenderList = new List<GenderModel>();
        }
        public int GenderId { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        public List<GenderModel> GenderList { get; set; }
    }
}
