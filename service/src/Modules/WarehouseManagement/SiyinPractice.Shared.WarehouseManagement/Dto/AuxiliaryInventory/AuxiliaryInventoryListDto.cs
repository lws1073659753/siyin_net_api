using ConnmIntel.Shared.WarehouseManagement.Dto.Auxiliary;
using ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas;
using System.Collections.Generic;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.AuxiliaryInventory
{
    public class AuxiliaryInventoryListDto
    {
        public string SysPn { get; set; }
        public int PnQty { get; set; }
        public List<AuxiliaryDto> auxiliaryDtos { get; set; }//old
    }
}