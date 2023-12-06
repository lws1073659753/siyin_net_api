using ConnmIntel.Application.Core;
using ConnmIntel.Domain.Core;
using ConnmIntel.Domain.Maintenance;
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Framework.Extensions;
using ConnmIntel.Framework.Mapper;
using ConnmIntel.Interface.BasicData.BasicData;
using ConnmIntel.Interface.BasicData;
using ConnmIntel.Interface.WarehouseManagement;
using ConnmIntel.Shared.Core.Utility;
using ConnmIntel.Shared.Maintenance.Dto;
using ConnmIntel.Shared.WarehouseManagement.Dto.AtchNo;
using ConnmIntel.Shared.WarehouseManagement.Dto.Auxiliary;
using ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnmIntel.Interface.AccessControl;
using NPOI.OpenXml4Net.OPC;
using Motor.Extensions.ContentEncoding.Abstractions;
using System.Linq.Expressions;
using ConnmIntel.Shared.Core.Dto;

namespace ConnmIntel.Application.WarehouseManagement.WarehouseManagement
{
    public class AuxiliaryService : NamedEntityService<Auxiliary, AuxiliaryDto, AuxiliarySearchPagedDto, CreateAuxiliaryDto>, IAuxiliaryService
    {
        private string TABLELABELDYNAMIC = "TableLabelDynamic";
        private readonly IEfRepository<SysDict> _dictionaryRepository;
        private readonly IUserAppService _userAppService;
        private readonly IAuxiliaryAtchService _AuxiliaryAtchService;
        private readonly IAuxiliaryInventoryService _AuxiliaryInventoryService;
        private readonly IAuxiliaryInventoryHistoryService _auxiliaryInventoryHistoryService;
        public AuxiliaryService(IEfRepository<Auxiliary> repository, IObjectMapper objectMapper,IAuxiliaryAtchService AuxiliaryAtchService, IUserAppService userAppService, IAuxiliaryInventoryHistoryService auxiliaryInventoryHistoryService,IAuxiliaryInventoryService auxiliaryInventoryService) : base(repository, objectMapper)
        {
            _userAppService = userAppService;
            _AuxiliaryAtchService = AuxiliaryAtchService;
            _auxiliaryInventoryHistoryService = auxiliaryInventoryHistoryService;
            _AuxiliaryInventoryService = auxiliaryInventoryService;
        }

        protected override Expression<Func<Auxiliary, bool>> BuildWhereExpression(Expression<Func<Auxiliary, bool>> whereExpression, AuxiliarySearchPagedDto search)
        {
            return base.BuildWhereExpression(whereExpression, search)
                 .AndIf(search.BeginTime.IsNotNullOrWhiteSpace(), x => x.CreateTime >= Convert.ToDateTime(search.BeginTime))
                  .AndIf(search.EndTime.IsNotNullOrWhiteSpace(), x => x.CreateTime <= Convert.ToDateTime(search.EndTime))
                  .AndIf(search.SysPn.IsNotNullOrWhiteSpace(), x => x.SysPn.Contains($"{search.SysPn}"))
                  .AndIf(search.Name.IsNotNullOrWhiteSpace(), x => x.Name.Contains($"{search.Name}"));
                 
        }

        ///<summary>
        ///导入Excel
        /// </summary>
        public async Task<ReturnTemplate> ExcelImport(IFormFile file)
        {
            ReturnTemplate returnTemplate = new ReturnTemplate();
            Validate.Assert(file == null || file.Length == 0, "上传文件为空");
            var tableByImprot = FormFileExtension.ExcelToDataTableBySecondary(file);
            var seroalno = "";
                
                var maxcode = await _AuxiliaryAtchService.GetLastAuxiliaryAtch();
                seroalno = SerialNoHelper.GetSerialno(maxcode?.Name, "PI");

           
            if (tableByImprot.ListIndex.Any())//返回里面空的数据return
            {
                returnTemplate.State = -2;
                returnTemplate.Message = "导入数据中有空值请检查Excel以下列的值";
                returnTemplate.ListStrings = tableByImprot.ListIndex;
                return returnTemplate;
            }
            

            var userinfo = await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId);
            List<Auxiliary> list = FormFileExtension.ConvertDataTableBySecondary<Auxiliary>(tableByImprot.dataTable, userinfo?.Profile?.DeptFullName, seroalno);
        
                bool isPn=list.GroupBy(p=>p.SysPn).Any(g =>g.Count()>1);
                Console.WriteLine($"PN 字段是否重复：{isPn}");
            if (isPn)
            {
                returnTemplate.State = -2;
                returnTemplate.Message = "导入数据中存在重复的PN";
                return returnTemplate;
            }

            var createAuxiliaryAtchDto = new AuxiliaryAtchDto()
            {
                Name = seroalno,
                State = "导入",
                DeptName = userinfo?.Profile?.DeptFullName,
             
            };

            await BulkInsertAndUpdata(list,createAuxiliaryAtchDto, tableByImprot.ListDept);

            returnTemplate.State = 0;
            return returnTemplate;

        }

        /// <summary>
        /// 批量保存数据
        /// </summary>
        /// <param name="improtNewList"></param>
        /// <param name="updteList"></param>
        /// <param name="createAtchNoDto"></param>
        public async Task BulkInsertAndUpdata(List<Auxiliary> auxiliaryNewList, AuxiliaryAtchDto createAuxiliaryAtchDto, List<string> listDept, List<Auxiliary> updteList = null)
        {

            await Repository.BulkInsertAsync(auxiliaryNewList);
            await _AuxiliaryAtchService.IsCreateAtch(createAuxiliaryAtchDto);
            if (updteList != null)
                await Repository.BulkUpdateAsync(updteList);
          

        }
        public async Task BulkUpdateAsync(List<Auxiliary> auxiliaryNewList)
        {
            await Repository.BulkUpdateAsync(auxiliaryNewList);
        }


        ///<summary>
        ///辅料导入Excel
        /// </summary>
        public async Task<List<AuxiliaryInventory>> AuxiliaryExcelImport(IFormFile file)
        {
            Validate.Assert(file == null || file.Length == 0, "上传文件为空");
            var tableByImprot = FormFileExtension.ExcelToDataTableBySecondary(file);
            var userinfo = await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId);
            List<Auxiliary> list = FormFileExtension.ConvertInventoryDataTableBySecondary<Auxiliary>(tableByImprot.dataTable, userinfo?.Profile?.DeptFullName);
            List<AuxiliaryInventory> sss = new List<AuxiliaryInventory>();
            List<AuxiliaryInventory> ai = new List<AuxiliaryInventory>();
            List<AuxiliaryInventoryHistory> aih = new List<AuxiliaryInventoryHistory>();
            List<AuxiliaryInventoryHistory> aihwrong = new List<AuxiliaryInventoryHistory>();
            var isNum=   await _AuxiliaryInventoryService.IsRrepeatName(list);
            if (isNum == null)
            {
                return null;
            }
            else
            {
                foreach (var item in list)
                {
                    AuxiliaryInventoryHistory auxiliaryInventoryHistory = new AuxiliaryInventoryHistory();
                    AuxiliaryInventory auxiliaryInventory = new AuxiliaryInventory();
                    var result1 = await Repository.FindAsync(x => x.Name == item.Name);
                    if (result1 != null)
                    {
                        var result2 = await Repository.FindAsync(x => x.SysPn == item.SysPn&& x.SysPn !=null);
                        if (result2 != null)
                        {
                            var result3 = await Repository.FindAsync(x => x.PnQty == item.PnQty);
                            if (result3 != null)
                            {
                                auxiliaryInventory.Id = Guid.NewGuid();
                                auxiliaryInventory.Name = item.Name;
                                auxiliaryInventory.SysPn = item.SysPn;
                                auxiliaryInventory.PnQty = item.PnQty;
                                auxiliaryInventory.CreateTime = DateTime.Now;
                                auxiliaryInventory.CreateDept = item.CreateDept;
                                sss.Add(auxiliaryInventory);
                             /*   auxiliaryInventoryHistory.Id = Guid.NewGuid();
                                auxiliaryInventoryHistory.SysPn = item.SysPn;
                                auxiliaryInventoryHistory.PnQty = item.PnQty;
                                auxiliaryInventoryHistory.Name = item.Name;
                                auxiliaryInventoryHistory.CreateTime = DateTime.Now;
                                auxiliaryInventory.CreateDept = item.CreateDept;
                                aih.Add(auxiliaryInventoryHistory);*/
                            }
                            else
                            {
                                auxiliaryInventory.Id = Guid.NewGuid();
                                auxiliaryInventory.SysPn = item.SysPn;
                                auxiliaryInventory.Description = "辅料PN的数量不同或为空";
                                auxiliaryInventory.PnQty = item.PnQty;
                                auxiliaryInventory.Name = item.Name;
                                auxiliaryInventory.CreateTime = DateTime.Now;
                                auxiliaryInventory.CreateDept = item.CreateDept;
                                sss.Add(auxiliaryInventory);
                            }
                        }
                        else
                        {
                            auxiliaryInventory.Id = Guid.NewGuid();
                            auxiliaryInventory.SysPn = item.SysPn;
                            auxiliaryInventory.Description = "辅料PN不同或为空";
                            auxiliaryInventory.PnQty = item.PnQty;
                            auxiliaryInventory.Name = item.Name;
                            auxiliaryInventory.CreateTime = DateTime.Now;
                            auxiliaryInventory.CreateDept = item.CreateDept;
                            sss.Add(auxiliaryInventory);
                        }
                    }
                    else
                    {
                        auxiliaryInventory.Id = Guid.NewGuid();
                        auxiliaryInventory.SysPn = item.SysPn;
                        auxiliaryInventory.Description = "辅料批次号不同或PN为空";
                        auxiliaryInventory.PnQty = item.PnQty;
                        auxiliaryInventory.Name = item.Name;
                        auxiliaryInventory.CreateTime = DateTime.Now;
                        auxiliaryInventory.CreateDept = item.CreateDept;
                        sss.Add(auxiliaryInventory);
                    }
                }
            }
            
            if (aihwrong.Count == 0)
            {
                await AuxiliaryBulkInsertAndUpdata(list, sss, aih);
                return sss;
            }
            else
            {

                await HistoryBulkInsertAndUpdata(aih, aihwrong);

                return null ;
            }

        }

        /// <summary>
        /// 批量保存数据
        /// </summary>
        /// <param name="improtNewList"></param>
        /// <param name="updteList"></param>
        /// <param name="createAtchNoDto"></param>
        public async Task AuxiliaryBulkInsertAndUpdata(List<Auxiliary> auxiliaryNewList, List<AuxiliaryInventory> auxiliaryInventorys, List<AuxiliaryInventoryHistory> auxiliaryInventories)
        {

            await _AuxiliaryInventoryService.BulkInsertAsync(auxiliaryInventorys);
            await _auxiliaryInventoryHistoryService.BulkInsertAsync(auxiliaryInventories);
            /*await *//*_auxiliaryInventory*//*.BulkUpdateAsync(auxiliaryInventorys);*/


        }
        ///<summary>
        ///历史表批量保存
        /// </summary>
        public async Task HistoryBulkInsertAndUpdata(List<AuxiliaryInventoryHistory> auxiliaryInventories, List<AuxiliaryInventoryHistory> auxiliaryInventoriesWrong)
        {
            await _auxiliaryInventoryHistoryService.BulkInsertAsync(auxiliaryInventories);
            await _auxiliaryInventoryHistoryService.BulkInsertAsync(auxiliaryInventoriesWrong);
            /*await *//*_auxiliaryInventory*//*.BulkUpdateAsync(auxiliaryInventorys);*/


        }

      
    }
}