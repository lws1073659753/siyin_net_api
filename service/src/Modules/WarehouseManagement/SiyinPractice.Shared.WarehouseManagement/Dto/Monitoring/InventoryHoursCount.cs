using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.Monitoring
{
    public class InventoryHoursCount
    {
        public string GroupDay { get; set; }
        public string GroupHours { get; set; }
        public int CountNum { get; set; }
    }
}
