using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Interface.Core;
using ConnmIntel.Shared.WarehouseManagement.Dto.Pallet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnmIntel.Interface.WarehouseManagement
{
    public interface IPalletService : INamedEntityService<PalletDto, PalletSearchPagedDto, CreatePalletDto>
    {
        string GetPalletName(string palletPrefix);
        Task<PalletDto> PrintInventorySheet(CreatePalletDto createInput);
        Task<PalletDto> PrintInventorySheetByBox(CreatePalletDto createInput);
    }
}