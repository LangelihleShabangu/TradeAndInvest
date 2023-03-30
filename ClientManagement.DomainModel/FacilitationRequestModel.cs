using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.DomainModel
{
    public class FacilitationRequestModel
    {
        public FacilitationRequestModel()
        {
            FacilitationRequestList = new List<FacilitationRequestModel>();
        }

        public int FacilitationId { get; set; }

        public int FacilitationRequestId { get; set; } 

        public string Comments { get; set; }        

        public string NeededAssistance { get; set; }

        public List<FacilitationRequestModel> FacilitationRequestList { get; set; }
        public string Notes { get; set; }
        public Guid CRMFacilitationRequestId { get; set; }
    }
}
