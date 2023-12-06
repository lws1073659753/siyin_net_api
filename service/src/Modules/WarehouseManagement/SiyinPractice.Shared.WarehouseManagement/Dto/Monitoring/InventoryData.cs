using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.Monitoring
{
    public class InventoryData
    {
        public List<InventoryByProject> ListInventoryProject { get; set; }
        public List<InventoryByPiNum> ListInventoryPiNum { get; set; }
    }
}
