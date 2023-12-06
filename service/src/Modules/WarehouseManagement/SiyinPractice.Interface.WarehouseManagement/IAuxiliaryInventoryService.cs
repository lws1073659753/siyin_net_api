
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Interface.Core;
using ConnmIntel.Shared.Core.Dto;
using ConnmIntel.Shared.WarehouseManagement.Dto.AuxiliaryInventory;
using ConnmIntel.Shared.WarehouseManagement.Dto.Inventory;
using ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnmIntel.Interface.WarehouseManagement
{
    public interface IAuxiliaryInventoryService : INamedEntityService<AuxiliaryInventoryDto, AuxiliaryInventorySearchPagedDto, CreateAuxiliaryInventoryDto>
    {

        /*  Task<List<AuxiliaryInventory>> ExcelImport(IFormFile file);*/
        Task BulkInsertAsync(List<AuxiliaryInventory> auxiliaryNewList);
        Task<List<Auxiliary>> IsRrepeatName(List<Auxiliary> auxiliary);
        Task<PageModelDto<AuxiliaryInventoryDto>> GetPagedAsyncByExport(AuxiliaryInventorySearchPagedDto search);
    }
}