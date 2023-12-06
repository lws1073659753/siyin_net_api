using System.Collections.Generic;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.Monitoring
{
    public class InventoryByProject
    {
        public string ProjectName { get; set; }
        public List<InventoryHoursCount> InventoryHoursCounts { get; set; }
    }
}