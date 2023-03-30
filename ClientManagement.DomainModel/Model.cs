using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.DomainModel
{
    public class AreaModel
    {
        public AreaModel()
        {
            AreaList = new List<AreaModel>();
        }
        public int AreaId { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        public List<AreaModel> AreaList { get; set; }
    }
}
