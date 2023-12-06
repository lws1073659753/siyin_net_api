using ConnmIntel.Application.Core;
using ConnmIntel.Domain.Core;
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Framework.Mapper;
using ConnmIntel.Interface.WarehouseManagement;
using ConnmIntel.Shared.WarehouseManagement.Dto.AuxiliaryInventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnmIntel.Application.WarehouseManagement.WarehouseManagement
{
    public class AuxiliaryInventoryHistoryService : NamedEntityService<AuxiliaryInventoryHistory, AuxiliaryInventoryHistoryDto, AuxiliaryInventoryHistorySearchPagedDto, CreateAuxiliaryInventoryHistoryDto>, IAuxiliaryInventoryHistoryService
    {
        public AuxiliaryInventoryHistoryService(IEfRepository<AuxiliaryInventoryHistory> repository, IObjectMapper objectMapper) : base(repository, objectMapper)
        {
        }

        public async Task BulkInsertAsync(List<AuxiliaryInventoryHistory> auxiliaryInventoryHistory)
        {
            await Repository.BulkInsertAsync(auxiliaryInventoryHistory);
        }

    }
}
