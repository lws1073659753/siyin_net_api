using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.Monitoring
{
    public class InventoryByLocation
    {
        public string Name { get; set; }
        public string Bin { get; set; }
        public int BinNum { get; set; }
        public int InventoryNum { get; set; }
        public int DifferenceNum { get; set; }
    }
}
