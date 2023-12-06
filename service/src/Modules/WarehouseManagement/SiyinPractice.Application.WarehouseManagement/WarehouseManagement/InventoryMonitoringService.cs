using ConnmIntel.Application.Core;
using ConnmIntel.Domain.BasicData;
using ConnmIntel.Domain.Core;
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Infrastructure.EntityFramework.Repositories;
using ConnmIntel.Interface.AccessControl;
using ConnmIntel.Interface.WarehouseManagement;
using ConnmIntel.Shared.Core.Dto;
using ConnmIntel.Shared.Core.Exceptions;
using ConnmIntel.Shared.WarehouseManagement.Dto.Monitoring;
using ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConnmIntel.Application.WarehouseManagement.WarehouseManagement
{
    public class InventoryMonitoringService : AppService, IInventoryMonitoringService
    {
        private readonly IUserAppService _userAppService;
        private readonly IEfRepository<InventoryHistory> _inventoryHistory;
        private readonly IEfRepository<Inventory> _inventory;
        private readonly IEfRepository<PrimaryData> _primaryData;
        private readonly IEfRepository<Warehouse> _warehouse;
        private readonly IEfRepository<Project> _project;

        public InventoryMonitoringService(IEfRepository<InventoryHistory> inventoryHistory, IEfRepository<Inventory> inventory, IEfRepository<Warehouse> warehouse, IEfRepository<PrimaryData> primaryData, IUserAppService userAppService, IEfRepository<Project> project)
        {
            _inventoryHistory = inventoryHistory;
            _warehouse = warehouse;
            _inventory = inventory;
            _primaryData = primaryData;
            _userAppService = userAppService;
            _project = project;
        }

        public async Task<InventoryData> GetInventoryMonitoring()
        {
            InventoryData inventoryData = new InventoryData();
            inventoryData.ListInventoryProject = new List<InventoryByProject>();
            inventoryData.ListInventoryPiNum = new List<InventoryByPiNum>();
            var begintime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //今天 00:00:00
            DateTime endtime = begintime.AddDays(1);
            var userinfo = await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId);
            var deptFullName = userinfo?.Profile?.DeptFullName;
            //object[] parameters=new object[begintime, endtime];
            var inventoryHistories = _inventoryHistory.Where(x => x.CreateTime >= begintime && x.CreateTime <= endtime && x.SnState == "已盘点"&&x.PiDpt== deptFullName).ToList();
            var inventoryHistorieByProject = inventoryHistories.GroupBy(x => x.ProjectName);
            var inventoryHistorieByPiNum = inventoryHistories.GroupBy(x => x.PiNum);
            var defaultTimeSpan = 1;
            var tempStartTime = begintime;
            var tempEndDateTime = endtime;
            var timeSpans = new List<KeyValuePair<DateTime, DateTime>>();
            do
            {
                tempEndDateTime = tempStartTime.AddHours(defaultTimeSpan);
                if (tempEndDateTime >= endtime)
                {
                    timeSpans.Add(new KeyValuePair<DateTime, DateTime>(tempStartTime, endtime));
                    break;
                }
                else
                {
                    timeSpans.Add(new KeyValuePair<DateTime, DateTime>(tempStartTime, tempEndDateTime));
                    tempStartTime = tempEndDateTime;
                }
            } while (true);

            foreach (var item in inventoryHistorieByProject)
            {
                InventoryByProject inventoryByProject = new InventoryByProject();
                inventoryByProject.ProjectName = item.Key;
                inventoryByProject.InventoryHoursCounts = new List<InventoryHoursCount>();
                foreach (var timeSpan in timeSpans)
                {
                    var inventoryHistoriesByProject = item.Where(x =>
                                                x.CreateTime > timeSpan.Key && x.CreateTime <= timeSpan.Value);
                    InventoryHoursCount inventoryHoursCount = new InventoryHoursCount();
                    inventoryHoursCount.GroupHours = timeSpan.Key.ToString("yyyy-MM-dd HH:00");
                    inventoryHoursCount.GroupDay = timeSpan.Key.ToString("HH:00");
                    inventoryHoursCount.CountNum = inventoryHistoriesByProject.Count();
                    inventoryByProject.InventoryHoursCounts.Add(inventoryHoursCount);
                }
                inventoryData.ListInventoryProject.Add(inventoryByProject);
            }
            foreach (var item in inventoryHistorieByPiNum)
            {
                InventoryByPiNum inventoryByPiNum = new InventoryByPiNum();
                inventoryByPiNum.PiNum = item.Key;
                inventoryByPiNum.InventoryHoursCounts = new List<InventoryHoursCount>();

                foreach (var timeSpan in timeSpans)
                {
                    var inventoryHistoriesByProject = item.Where(x =>
                                                x.CreateTime > timeSpan.Key && x.CreateTime <= timeSpan.Value);
                    InventoryHoursCount inventoryHoursCount = new InventoryHoursCount();
                    inventoryHoursCount.GroupHours = timeSpan.Key.ToString("yyyy-MM-dd HH:00");
                    inventoryHoursCount.GroupDay = timeSpan.Key.ToString("HH:00");
                    inventoryHoursCount.CountNum = inventoryHistoriesByProject.Count();
                    inventoryByPiNum.InventoryHoursCounts.Add(inventoryHoursCount);
                }
                inventoryData.ListInventoryPiNum.Add(inventoryByPiNum);
            }
            if (!inventoryHistorieByProject.Any())
            {
                InventoryByProject inventoryByProject = new InventoryByProject();
                inventoryByProject.ProjectName = "暂无数据";
                inventoryByProject.InventoryHoursCounts = new List<InventoryHoursCount>();
                foreach (var timeSpan in timeSpans)
                {
                    InventoryHoursCount inventoryHoursCount = new InventoryHoursCount();
                    inventoryHoursCount.GroupHours = timeSpan.Key.ToString("yyyy-MM-dd HH:00");
                    inventoryHoursCount.GroupDay = timeSpan.Key.ToString("HH:00");
                    inventoryHoursCount.CountNum = 0;
                    inventoryByProject.InventoryHoursCounts.Add(inventoryHoursCount);
                }
                inventoryData.ListInventoryProject.Add(inventoryByProject);
            }
            if (!inventoryHistorieByPiNum.Any())
            {
                InventoryByPiNum inventoryByPiNum = new InventoryByPiNum();
                inventoryByPiNum.PiNum = "暂无数据";
                inventoryByPiNum.InventoryHoursCounts = new List<InventoryHoursCount>();

                foreach (var timeSpan in timeSpans)
                {
                    InventoryHoursCount inventoryHoursCount = new InventoryHoursCount();
                    inventoryHoursCount.GroupHours = timeSpan.Key.ToString("yyyy-MM-dd HH:00");
                    inventoryHoursCount.GroupDay = timeSpan.Key.ToString("HH:00");
                    inventoryHoursCount.CountNum = 0;
                    inventoryByPiNum.InventoryHoursCounts.Add(inventoryHoursCount);
                }
                inventoryData.ListInventoryPiNum.Add(inventoryByPiNum);
            }
            return inventoryData;
        }

        public async Task<List<InventoryByLocation>> GetInventoryMonitoringByLocation()
        {
            List<InventoryByLocation> inventoryByLocations = new List<InventoryByLocation>();
            var warehouses = _warehouse.GetAll().OrderByDescending(x => x.CreateTime).ToList();
            var userinfo = await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId);
            var deptFullName = userinfo?.Profile?.DeptFullName;
            foreach (var item in warehouses)
            {
                InventoryByLocation inventoryByLocation = new InventoryByLocation();
                inventoryByLocation.Name = "Bin";
                inventoryByLocation.Bin = item.Name;
                var exits = await _primaryData.AnyAsync(x => x.SysBin == item.Name &&  x.PiDpt == deptFullName);
                var exits2 = await _inventoryHistory.AnyAsync(x => x.SysBin == item.Name && x.SnState == "已盘点" && x.PiDpt == deptFullName);
                var binNum = exits ? await _primaryData.CountAsync(x => x.SysBin == item.Name && x.PiDpt == deptFullName) : 0;
                var inventoryNum = exits2 ? await _inventoryHistory.CountAsync(x => x.SysBin == item.Name && x.SnState == "已盘点" && x.PiDpt == deptFullName) : 0;
                inventoryByLocation.BinNum = binNum;
                inventoryByLocation.InventoryNum = inventoryNum;
                inventoryByLocation.DifferenceNum = inventoryByLocation.BinNum - inventoryByLocation.InventoryNum;
                inventoryByLocations.Add(inventoryByLocation);
            }
            var project = _project.Where(x=>x.CreateDept== deptFullName).OrderByDescending(x => x.CreateTime).ToList();
            foreach (var item in project)
            {
                InventoryByLocation inventoryByLocation = new InventoryByLocation();
                inventoryByLocation.Name = "Project";
                inventoryByLocation.Bin = item.Name;
                var exits = await _primaryData.AnyAsync(x => x.SysBin == item.Name && x.PiDpt == deptFullName);
                var exits2 = await _inventoryHistory.AnyAsync(x => x.ProjectName == item.Name && x.SnState == "已盘点" && x.PiDpt == deptFullName);
                var binNum = exits ? await _primaryData.CountAsync(x => x.SysBin == item.Name && x.PiDpt == deptFullName) : 0;
                var inventoryNum = exits2 ? await _inventoryHistory.CountAsync(x => x.ProjectName == item.Name && x.SnState == "已盘点" && x.PiDpt == deptFullName) : 0;
                inventoryByLocation.BinNum = binNum;
                inventoryByLocation.InventoryNum = inventoryNum;
                inventoryByLocation.DifferenceNum = inventoryByLocation.BinNum - inventoryByLocation.InventoryNum;
                inventoryByLocations.Add(inventoryByLocation);
            }
            return inventoryByLocations;
        }
        public async Task<PageModelDto<PrimaryData>> GetInventoryMonitoringByDifference(MonitoringByDifferenceSearchPagedDto inventoryByLocation)
        {
            var userinfo = await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId);
            var deptFullName = userinfo?.Profile?.DeptFullName;
            if (inventoryByLocation.TypeName== "Bin")
            {
                var primaryData = _primaryData.Where(x => x.SysBin == inventoryByLocation.Bin && x.PiDpt == deptFullName).ToList();
                var inventooyData = _inventoryHistory.Where(x => x.SysBin == inventoryByLocation.Bin && x.SnState == "已盘点" && x.PiDpt == deptFullName).ToList();
               var primaryDataSelect = primaryData.Select(p => new { p.PiNum,  p.PiDpt, p.SysSn, p.SysOrgSn,  p.SysBin,});
               var inventooyDataSeclet= inventooyData.Select(i => new { i.PiNum, i.PiDpt, i.SysSn, i.SysOrgSn,  i.SysBin});
               var difference = primaryDataSelect
                            .Except(inventooyDataSeclet).ToList();
                var difference2= inventooyDataSeclet
                            .Except(primaryDataSelect).ToList();
                var noInvern = primaryData.Where(x => difference.FirstOrDefault(s=>s.SysSn== x.SysSn&&s.PiNum==x.PiNum)!=null).ToList();
                var inventoryForce = inventooyData.Where(x => difference2.FirstOrDefault(s => s.SysSn == x.SysSn && s.PiNum == x.PiNum) != null).Select(x => new PrimaryData
                {
                    SnState = "强制录入Sn",
                    SysSn = x.ScanSn,
                    SysOrgSn = x.ScanSn,
                    SysOrgPn = x.ScanPn,
                    SysPn = x.ScanPn,
                    SysLocation = x.SysLocation,
                    SysBin = x.SysBin,
                    Source = x.Source,
                    PiDpt = x.PiDpt,
                    PiProject = x.PiProject,
                    PiNum = x.PiNum,
                    AccountBook = x.AccountBook,
                    CreateDept = x.CreateDept,
                    Creator = x.Creator,
                    Name = x.Name,
                    FilingNo = x.FilingNo,
                    CreateTime = x.CreateTime,
                });
                noInvern.AddRange(inventoryForce);
                var entities = noInvern
                                    .Skip(inventoryByLocation.SkipRows())
                                    .Take(inventoryByLocation.PageSize)
                                    .ToList();
                return new PageModelDto<PrimaryData>(inventoryByLocation, entities.ToList(), noInvern.Count());

            }
            else
            {
                return new PageModelDto<PrimaryData>(inventoryByLocation, new List<PrimaryData>(), 0);
            }
        }
        public async Task<PageModelDto<PrimaryData>> GetInventoryMonitoringByDifferenceByExcel(MonitoringByDifferenceSearchPagedDto inventoryByLocation)
        {
            var userinfo = await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId);
            var deptFullName = userinfo?.Profile?.DeptFullName;
            if (inventoryByLocation.TypeName == "Bin")
            {
                var primaryData = _primaryData.Where(x => x.SysBin == inventoryByLocation.Bin && x.PiDpt == deptFullName).ToList();
                var inventooyData = _inventoryHistory.Where(x => x.SysBin == inventoryByLocation.Bin && x.SnState == "已盘点" && x.PiDpt == deptFullName).ToList();
                var primaryDataSelect = primaryData.Select(p => new { p.PiNum, p.PiDpt, p.SysSn, p.SysOrgSn, p.SysPn, p.SysOrgPn, p.SysBin, });
                var inventooyDataSeclet = inventooyData.Select(i => new { i.PiNum, i.PiDpt, i.SysSn, i.SysOrgSn, i.SysPn, i.SysOrgPn, i.SysBin });
                var difference = primaryDataSelect
                                  .Except(inventooyDataSeclet).ToList();
                var difference2 = inventooyDataSeclet
                            .Except(primaryDataSelect).ToList();
                var noInvern = primaryData.Where(x => difference.FirstOrDefault(s => s.SysSn == x.SysSn && s.PiNum == x.PiNum) != null).ToList();
                var inventoryForce = inventooyData.Where(x => difference2.FirstOrDefault(s => s.SysSn == x.SysSn && s.PiNum == x.PiNum) != null).Select(x => new PrimaryData
                {
                    SnState = "强制录入Sn",
                    SysSn = x.ScanSn,
                    SysOrgSn = x.ScanSn,
                    SysOrgPn = x.ScanPn,
                    SysPn = x.ScanPn,
                    SysLocation = x.SysLocation,
                    SysBin = x.SysBin,
                    Source = x.Source,
                    PiDpt = x.PiDpt,
                    PiProject = x.PiProject,
                    PiNum = x.PiNum,
                    AccountBook = x.AccountBook,
                    CreateDept = x.CreateDept,
                    Creator = x.Creator,
                    Name = x.Name,
                    FilingNo = x.FilingNo,
                    CreateTime = x.CreateTime,
                });
                noInvern.AddRange(inventoryForce);
                var entities = noInvern
                                    .ToList();
                return new PageModelDto<PrimaryData>(inventoryByLocation, entities.ToList(), noInvern.Count());

            }
            else
            {
                return new PageModelDto<PrimaryData>(inventoryByLocation, new List<PrimaryData>(), 0);
            }
        }
    }
}