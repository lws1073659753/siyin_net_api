using AutoMapper.Internal.Mappers;
using ConnmIntel.Application.Core;
using ConnmIntel.Domain.BasicData;
using ConnmIntel.Domain.Core;
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Framework.Mapper;
using ConnmIntel.Infrastructure.EntityFramework.Repositories;
using ConnmIntel.Interface.WarehouseManagement;
using ConnmIntel.Shared.Core.Dto;
using ConnmIntel.Shared.Core.Exceptions;
using ConnmIntel.Shared.Core.Utility;
using ConnmIntel.Shared.WarehouseManagement.Dto.AtchNo;
using ConnmIntel.Shared.WarehouseManagement.Dto.Box;
using ConnmIntel.Shared.WarehouseManagement.Dto.Inventory;
using ConnmIntel.Shared.WarehouseManagement.Dto.InventoryHistory;
using ConnmIntel.Shared.WarehouseManagement.Dto.Pallet;
using ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConnmIntel.Application.WarehouseManagement.WarehouseManagement
{
    public class InventoryService : NamedEntityService<Inventory, InventoryDto, InventorySearchPagedDto, CreateInventoryDto>, IInventoryService
    {
        private readonly IEfRepository<InventoryHistory> _inventoryHistoryService;
        private readonly IPrimaryDataService _primaryDataService;
        private readonly IBoxService _boxService;
        private readonly IPalletService _palletService;
        private readonly IEfRepository<PrimaryData> _efRepository;


        public InventoryService(IEfRepository<Inventory> repository, ConnmIntel.Framework.Mapper.IObjectMapper objectMapper, IEfRepository<InventoryHistory> inventoryHistoryService, IPrimaryDataService primaryDataService, IBoxService boxService, IPalletService palletService, IEfRepository<PrimaryData> efRepository) : base(repository, objectMapper)
        {
            _inventoryHistoryService = inventoryHistoryService;
            _primaryDataService = primaryDataService;
            _boxService = boxService;
            _palletService = palletService;
            _efRepository = efRepository;
        }

        public async Task<ReturnTemplate> CreateInventory(InventoryListDto inventoryListDto)
        {
            Validate.Assert(inventoryListDto == null, ConnmIntelMessage.DTO_IS_NULL);
            var listBoxSn= Repository.Where(x => x.BoxName == inventoryListDto.BoxName).ToList();
            ReturnTemplate returnTemplate = new ReturnTemplate();

            List<string> diffPrimaryDataDtos = new List<string>();
            List<string> inventoryDataDtos = new List<string>();
            List<PrimaryData> primaryDatas = new List<PrimaryData>();
            List<Inventory> inventorieList = new List<Inventory>();
            List<InventoryHistory> inventoryHistoriyLists = new List<InventoryHistory>();
            var creator = Framework.Security.UserTokenService.GetUserToken().UserName;
            var createTime = DateTime.Now;
            foreach (var item in inventoryListDto.primaryNewDataDtos)
            {
                if (item.Id != null)
                {
                    var entity =await _efRepository.FindAsync((Guid)item.Id);
                    if (entity != null)
                    {
                        if (entity.SnState=="已盘点")
                        {
                            inventoryDataDtos.Add(item.SysSn);
                        }
                        if (item.EditTime?.ToString("yyyy-MM-dd HH:mm:ss") != entity.EditTime?.ToString("yyyy-MM-dd HH:mm:ss"))
                        {
                            diffPrimaryDataDtos.Add(item.SysSn);
                        }
                        else
                        {
                            Inventory inventory = new Inventory();
                            inventory.SysBin = inventoryListDto.Bin;
                            inventory.SysLocation = inventoryListDto.Location;
                            inventory.PiProject = item.PiProject;
                            inventory.AutomaticTag = inventoryListDto.AutomaticTag;
                            inventory.BoxName = inventoryListDto.BoxName;
                            inventory.ScanPallet = inventoryListDto.Pallet;
                            inventory.Id = Guid.NewGuid();
                            inventory.CreateTime = createTime;
                            inventory.Creator = creator;
                            inventory.ScanSn = item.ScanSn;
                            inventory.ScanPn = item.ScanPn;
                            inventory.SysSn = item.SysSn;
                            inventory.SysPn = item.SysPn;
                            inventory.SysOrgSn = item.SysOrgSn;
                            inventory.SysOrgPn = item.SysOrgPn;
                            inventory.PiNum = entity.PiNum;
                            inventory.PiDpt = entity.PiDpt;
                            inventory.Source = entity.Source;
                            inventory.AccountBook = entity.AccountBook;
                            inventory.FilingNo = entity.FilingNo;
                            inventory.CreateDept = entity.CreateDept;
                            inventory.ProjectName = inventoryListDto.ProjectName;

                            InventoryHistory inventoryHistory = new InventoryHistory();
                            inventoryHistory.SysBin = inventoryListDto.Bin;
                            inventoryHistory.SysLocation = inventoryListDto.Location;
                            inventoryHistory.PiProject = item.PiProject;
                            inventoryHistory.ProjectName = inventoryListDto.ProjectName;
                            inventoryHistory.AutomaticTag = inventoryListDto.AutomaticTag;
                            inventoryHistory.BoxName = inventoryListDto.BoxName;
                            inventoryHistory.ScanPallet = inventoryListDto.Pallet;
                            inventoryHistory.SnRules = inventoryListDto.SnRules.ToString();
                            inventoryHistory.PnRules = inventoryListDto.PnRules.ToString();
                            inventoryHistory.SnReplace = inventoryListDto.SnReplace;
                            inventoryHistory.EndShield = inventoryListDto.EndShield;
                            inventoryHistory.BoxPrefix = inventoryListDto.BoxPrefix;
                            inventoryHistory.TagPrefix = inventoryListDto.TagPrefix;
                            inventoryHistory.IsPlantSn = inventoryListDto.IsPlantSn;
                            inventoryHistory.Id = Guid.NewGuid();
                            inventoryHistory.CreateTime = createTime;
                            inventoryHistory.Creator = creator;
                            inventoryHistory.ScanSn = item.ScanSn;
                            inventoryHistory.ScanPn = item.ScanPn;
                            inventoryHistory.SysSn = item.SysSn;
                            inventoryHistory.SysPn = item.SysPn;
                            inventoryHistory.SysOrgSn = item.SysOrgSn;
                            inventoryHistory.SysOrgPn = item.SysOrgPn;
                            inventoryHistory.PiNum = entity.PiNum;
                            inventoryHistory.PiDpt = entity.PiDpt;
                            inventoryHistory.Source = entity.Source;
                            inventoryHistory.AccountBook = entity.AccountBook;
                            inventoryHistory.FilingNo = entity.FilingNo;
                            inventoryHistory.CreateDept = entity.CreateDept;
                            inventoryHistory.SnState = "已盘点";
                            entity.SnState = "已盘点";
                            entity.EditTime = createTime;
                            entity.Editor = creator;
                            primaryDatas.Add(entity);
                            inventorieList.Add(inventory);
                            inventoryHistoriyLists.Add(inventoryHistory);
                        }
                    }

                }
                else
                {
                    Inventory inventory = new Inventory();
                    inventory.SysBin = inventoryListDto.Bin;
                    inventory.SysLocation = inventoryListDto.Location;
                    inventory.PiProject = item.PiProject;
                    inventory.ProjectName = inventoryListDto.ProjectName;
                    inventory.AutomaticTag = inventoryListDto.AutomaticTag;
                    inventory.BoxName = inventoryListDto.BoxName;
                    inventory.ScanPallet = inventoryListDto.Pallet;
                    inventory.Id = Guid.NewGuid();
                    inventory.CreateTime = createTime;
                    inventory.Creator = creator;
                    inventory.ScanSn = item.ScanSn;
                    inventory.ScanPn = item.ScanPn;
                    inventory.SysSn = item.SysSn;
                    inventory.SysPn = item.SysPn;
                    inventory.SysOrgSn = item.SysOrgSn;
                    inventory.SysOrgPn = item.SysOrgPn;
                    inventory.PiNum =   item.PiNum;
                    inventory.PiDpt = item.PiDpt;
                    inventory.Source = item.Source;
                    inventory.AccountBook = item.AccountBook;
                    inventory.FilingNo = item.FilingNo;
                    inventory.CreateDept = item.PiDpt;

                    InventoryHistory inventoryHistory = new InventoryHistory();
                    inventoryHistory.SysBin = inventoryListDto.Bin;
                    inventoryHistory.SysLocation = inventoryListDto.Location;
                    inventoryHistory.PiProject = item.PiProject;
                    inventoryHistory.ProjectName = inventoryListDto.ProjectName;
                    inventoryHistory.AutomaticTag = inventoryListDto.AutomaticTag;
                    inventoryHistory.BoxName = inventoryListDto.BoxName;
                    inventoryHistory.ScanPallet = inventoryListDto.Pallet;
                    inventoryHistory.SnRules = inventoryListDto.SnRules.ToString();
                    inventoryHistory.PnRules = inventoryListDto.PnRules.ToString();
                    inventoryHistory.SnReplace = inventoryListDto.SnReplace;
                    inventoryHistory.EndShield = inventoryListDto.EndShield;
                    inventoryHistory.BoxPrefix = inventoryListDto.BoxPrefix;
                    inventoryHistory.TagPrefix = inventoryListDto.TagPrefix;
                    inventoryHistory.IsPlantSn = inventoryListDto.IsPlantSn;
                    inventoryHistory.Id = Guid.NewGuid();
                    inventoryHistory.CreateTime = createTime;
                    inventoryHistory.Creator = creator;
                    inventoryHistory.ScanSn = item.ScanSn;
                    inventoryHistory.ScanPn = item.ScanPn;
                    inventoryHistory.SysSn = item.SysSn;
                    inventoryHistory.SysPn = item.SysPn;
                    inventoryHistory.SysOrgSn = item.SysOrgSn;
                    inventoryHistory.SysOrgPn = item.SysOrgPn;
                    inventoryHistory.PiNum = item.PiNum;
                    inventoryHistory.PiDpt = item.PiDpt;
                    inventoryHistory.Source = item.Source;
                    inventoryHistory.AccountBook = item.AccountBook;
                    inventoryHistory.FilingNo = item.FilingNo;
                    inventoryHistory.CreateDept = item.PiDpt;
                    inventoryHistory.SnState = "已盘点";
                    inventorieList.Add(inventory);
                    inventoryHistoriyLists.Add(inventoryHistory);
                }
            }
            //var list = ObjectMapper.Map<List<PrimaryDataDto>>(results).ToList();
            //var inventoryList = list.Select(x => new { Id = x.Id, EditTime = x.EditTime, SysSn = x.SysSn });
            //var isEqual = inventoryListDto.primaryNewDataDtos.Select(x => new { Id = x.Id, EditTime = x.EditTime, SysSn = x.SysSn });
            if (inventoryDataDtos.Any())
            {
                returnTemplate.State = -2;
                returnTemplate.inventoryDataDtos = inventoryDataDtos;
                return returnTemplate;
            }
            if (diffPrimaryDataDtos.Any())
            {
                returnTemplate.State = -1;
                returnTemplate.diffPrimaryDataDtos = diffPrimaryDataDtos;
                return returnTemplate;
            }
            else
            {
                CreateBoxDto createBoxDto = new()
                {
                    Name = inventoryListDto.BoxName,
                    AutomaticGeneration = inventoryListDto.IsBoxAutomatic,
                    State = "已盘点",
                    Location = inventoryListDto.Location,
                    BoxCount = inventoryListDto.primaryNewDataDtos.Count,
                    Bin = inventoryListDto.Bin,
                    Pallet = inventoryListDto.Pallet,
                };
                //results.ForEach(x => { x.SnState = "已盘点"; x.EditTime = DateTime.Now; x.Editor = Framework.Security.UserTokenService.GetUserToken().UserName; });
                await BulkInsertAndUpdata(inventorieList, inventoryHistoriyLists, primaryDatas, createBoxDto);
                returnTemplate.State = 0;
                return returnTemplate;
            }
        }
        public async Task<int> GetPalletQty(string pallet)
        {
            var isinventory = await Repository.CountAsync(x => x.ScanPallet == pallet);
            return isinventory;
        }
        public override async Task<int> RemoveAsync(Guid id)
        {
            var entity = await Repository.FindAsync(id);
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.SysSn))
                {
                    var primaryData = await _efRepository.FindAsync(x => x.SysSn == entity.SysSn && x.SysOrgSn == entity.SysOrgSn);
                    primaryData.SnState = "已导入";
                    await _efRepository.UpdateAsync(primaryData);
                }
                var inventoryHistoryService = await _inventoryHistoryService.FindAsync(x => x.SysSn == entity.SysSn && x.SysOrgSn == entity.SysOrgSn && x.ScanSn == entity.ScanSn);
                await _inventoryHistoryService.DeleteAsync(inventoryHistoryService.Id);
                await _boxService.UpdateBoxCount(entity.BoxName);
                return await Repository.DeleteAsync(id);
            }
            return 0;
            
            
        }
        public async Task BulkInsertAndUpdata(List<Inventory> inventories, List<InventoryHistory> inventoryHistories, List<PrimaryData> primaryDatas, CreateBoxDto createBoxDto)
        {

            //await _AtchNoService.AddAsync(createAtchNoDto);
            await Repository.BulkInsertAsync(inventories);
            await _inventoryHistoryService.BulkInsertAsync(inventoryHistories);
            await _primaryDataService.BulkUpdateAsync(primaryDatas);
            await _boxService.IsCreateBox(createBoxDto);
            //await _palletService.AddAsync(createPalletDto);
        }
    }
}