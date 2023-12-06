using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.Monitoring
{
    public class InventoryByPiNum
    {
        public string PiNum { get; set; }
        public List<InventoryHoursCount> InventoryHoursCounts { get; set; }
    }
}
