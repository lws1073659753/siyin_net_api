using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Interface.Core;
using ConnmIntel.Shared.Core.Dto;
using ConnmIntel.Shared.Maintenance.Dto;
using ConnmIntel.Shared.WarehouseManagement.Dto.AtchNo;
using ConnmIntel.Shared.WarehouseManagement.Dto.Inventory;
using ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnmIntel.Interface.WarehouseManagement
{
    public interface IPrimaryDataService : INamedEntityService<PrimaryDataDto, PrimaryDataSearchPagedDto, CreatePrimaryDataDto>
    {
        Task<ReturnTemplate> ExcelImport(IFormFile file);
        Task<List<InventoryDto>> GetPrimaryDataDtoBySn(string ScanSn, string oldSn, string BoxName, string ScanPallet, string projectName);
        Task<List<DictDto>> GetTableLabelDynamic();
        Task<ReturnTemplate> ConfirmExcelImport(IFormFile file);
        Task BulkUpdateAsync(List<PrimaryData> improtNewList);
        Task BulkInsertAndUpdata(List<PrimaryData> improtNewList, AtchNoDto createAtchNoDto, List<string> listBins, List<string> listLocations, List<string> listProjects, List<PrimaryData> updteList = null);
        Task<PageModelDto<PrimaryDataDto>> GetPagedAsyncByExport(PrimaryDataSearchPagedDto search);
    }
}