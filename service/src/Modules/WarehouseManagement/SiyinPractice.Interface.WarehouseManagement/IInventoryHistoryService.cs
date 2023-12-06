using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Interface.Core;
using ConnmIntel.Shared.Core.Dto;
using ConnmIntel.Shared.Maintenance.Dto;
using ConnmIntel.Shared.WarehouseManagement.Dto.Inventory;
using ConnmIntel.Shared.WarehouseManagement.Dto.InventoryHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Interface.WarehouseManagement
{
    public interface IInventoryHistoryService : INamedEntityService<InventoryHistoryDto, InventoryHistorySearchPagedDto, CreateInventoryHistoryDto>
    {
        Task<InventoryHistoryDto> CreateErrorSnHistory(CreateInventoryHistoryDto createInput);
        Task<int> CreateHistory(CreateInventoryHistoryDto createInput);
        Task BulkInsertAsync(List<InventoryHistory> inventoryHistories);
        Task<List<DictDto>> GetDataQueryTableLabel();
        Task<List<DictDto>> GetDataQueryTableLabelByAuxiliary();
        Task<PageModelDto<InventoryHistoryDto>> GetPagedAsyncByExport(InventoryHistorySearchPagedDto search);
    }
}
