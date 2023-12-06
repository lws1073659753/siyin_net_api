using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Interface.Core;
using ConnmIntel.Shared.Core.Dto;
using ConnmIntel.Shared.WarehouseManagement.Dto.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Interface.WarehouseManagement
{
    public interface IInventoryMonitoringService : IAppService
    {
        Task<InventoryData> GetInventoryMonitoring();
        Task<List<InventoryByLocation>> GetInventoryMonitoringByLocation();
        Task<PageModelDto<PrimaryData>> GetInventoryMonitoringByDifference(MonitoringByDifferenceSearchPagedDto inventoryByLocation);
        Task<PageModelDto<PrimaryData>> GetInventoryMonitoringByDifferenceByExcel(MonitoringByDifferenceSearchPagedDto inventoryByLocation);
    }
}
