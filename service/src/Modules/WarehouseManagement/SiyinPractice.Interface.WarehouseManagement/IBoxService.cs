using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Interface.Core;
using ConnmIntel.Shared.WarehouseManagement.Dto.Box;
using ConnmIntel.Shared.WarehouseManagement.Dto.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnmIntel.Interface.WarehouseManagement
{
    public interface IBoxService : INamedEntityService<BoxDto, BoxSearchPagedDto, CreateBoxDto>
    {
        Task<string> GetBoxName(string boxPrefix, string bin, string pallet);
        Task<List<InventoryDto>> AnyAsyncBoxName(string boxName);
        Task<BoxDto> IsCreateBox(CreateBoxDto createBoxDto);
        Task<int> UpdateBoxCount(string boxName);
    }
}