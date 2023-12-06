
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Interface.Core;
using ConnmIntel.Shared.WarehouseManagement.Dto.AuxiliaryInventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnmIntel.Interface.WarehouseManagement
{
    public interface IAuxiliaryInventoryHistoryService : INamedEntityService<AuxiliaryInventoryHistoryDto, AuxiliaryInventoryHistorySearchPagedDto, CreateAuxiliaryInventoryHistoryDto>
    {
        Task BulkInsertAsync(List<AuxiliaryInventoryHistory> auxiliaryInventoryHistories);
    }
}