
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Interface.Core;
using ConnmIntel.Shared.Core.Dto;
using ConnmIntel.Shared.WarehouseManagement.Dto.Auxiliary;
using ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnmIntel.Interface.WarehouseManagement
{
    public interface IAuxiliaryService : INamedEntityService<AuxiliaryDto, AuxiliarySearchPagedDto, CreateAuxiliaryDto>
    {
        Task<ReturnTemplate> ExcelImport(IFormFile file);
        Task BulkUpdateAsync(List<Auxiliary> auxiliaryNewList);
        Task<List<AuxiliaryInventory>> AuxiliaryExcelImport(IFormFile file);
     


    }
}