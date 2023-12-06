using ConnmIntel.Interface.Core;
using ConnmIntel.Shared.WarehouseManagement.Dto.Inventory;
using ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Interface.WarehouseManagement
{
    public interface IInventoryService : INamedEntityService<InventoryDto, InventorySearchPagedDto, CreateInventoryDto>
    {
        Task<ReturnTemplate> CreateInventory(InventoryListDto inventoryListDto);
        Task<int> GetPalletQty(string pallet);
    }
}
