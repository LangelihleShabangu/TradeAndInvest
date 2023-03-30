using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.DomainModel
{
    public class InventoryModel
    {
        public InventoryModel()
        {
            InventoryList = new List<InventoryModel>();
        }

        public int InventoryId { get; set; }
        public int WeekNumber { get; set; }
        public int Actual { get; set; }
        public int Proposed { get; set; }
        public bool IsDeleted { get; set; }
       
        public DateTime StartDate { get; set; }
        public DateTime Enddate { get; set; }
        public int ProductsId { get; set; }
        public int Quantity { get; set; }
        public string Products { get; set; }
        public List<InventoryModel> InventoryList { get; set; }
    }
}
