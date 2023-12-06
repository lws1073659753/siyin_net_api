using ConnmIntel.Application.Core;
using ConnmIntel.Domain.BasicData;
using ConnmIntel.Domain.Core;
using ConnmIntel.Domain.Maintenance;
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Framework.Extensions;
using ConnmIntel.Framework.Mapper;
using ConnmIntel.Interface.AccessControl;
using ConnmIntel.Interface.BasicData;
using ConnmIntel.Interface.BasicData.BasicData;
using ConnmIntel.Interface.WarehouseManagement;
using ConnmIntel.Shared.Core.Dto;
using ConnmIntel.Shared.Core.Exceptions;
using ConnmIntel.Shared.Core.Utility;
using ConnmIntel.Shared.Maintenance.Dto;
using ConnmIntel.Shared.WarehouseManagement.Dto.AtchNo;
using ConnmIntel.Shared.WarehouseManagement.Dto.Inventory;
using ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConnmIntel.Application.WarehouseManagement.WarehouseManagement
{
    public class PrimaryDataService : NamedEntityService<PrimaryData, PrimaryDataDto, PrimaryDataSearchPagedDto, CreatePrimaryDataDto>, IPrimaryDataService
    {
        private readonly IUserAppService _userAppService;
        private readonly IWarehouseService _warehouseService;
        private readonly IAreaService _areaService;
        private readonly IEfRepository<SysDict> _dictionaryRepository;
        private readonly IEfRepository<Project> _projectRepostitory;
        private readonly IAtchNoService _AtchNoService;
        private readonly IProjectService _projectService;
        private readonly IEfRepository<AtchNo> _atchNoServices;
        private string TABLELABELDYNAMIC = "TableLabelDynamic";

        public PrimaryDataService(IEfRepository<PrimaryData> repository, IObjectMapper objectMapper, IUserAppService userAppService, IEfRepository<SysDict> dictionaryRepository,
           IAtchNoService AtchNoService, IEfRepository<AtchNo> atchNoServices, IProjectService projectService, IAreaService areaService, IWarehouseService warehouseService, IEfRepository<Project> projectRepostitory) : base(repository, objectMapper)
        {
            _userAppService = userAppService;
            _dictionaryRepository = dictionaryRepository;
            _AtchNoService = AtchNoService;
            _projectRepostitory = projectRepostitory;
            _atchNoServices = atchNoServices;
            _projectService = projectService;
            _areaService = areaService;
            _warehouseService = warehouseService;
        }

        protected override Expression<Func<PrimaryData, bool>> BuildWhereExpression(Expression<Func<PrimaryData, bool>> whereExpression, PrimaryDataSearchPagedDto search)
        {
            var deptFullName = _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId)
                                              .GetAwaiter()
                                              .GetResult()?.Profile?.DeptFullName;
            return base.BuildWhereExpression(whereExpression, search)
                 .AndIf(search.BeginTime.IsNotNullOrWhiteSpace(), x => x.CreateTime >= Convert.ToDateTime(search.BeginTime))
                  .AndIf(search.EndTime.IsNotNullOrWhiteSpace(), x => x.CreateTime <= Convert.ToDateTime(search.EndTime))
                  .AndIf(search.SysSn.IsNotNullOrWhiteSpace(), x => x.SysSn.Contains($"{search.SysSn}"))
                  .AndIf(search.PiNum.IsNotNullOrWhiteSpace(), x => x.PiNum.Contains($"{search.PiNum}"))
                  .AndIf(deptFullName.IsNotNullOrWhiteSpace(), x => x.CreateDept==($"{deptFullName}"))
                  //.AndIf(search.SysSn.IsNotNullOrWhiteSpace(), x => x.SysOrgSn.Contains($"{search.SysSn}"))
                  //.AndIf(search.SysSn.IsNotNullOrWhiteSpace(), x => x.PlantSn.Contains($"{search.SysSn}"))
                  .AndIf(search.SysPn.IsNotNullOrWhiteSpace(), x => x.SysPn.Contains($"{search.SysPn}"))
                  //.AndIf(search.SysPn.IsNotNullOrWhiteSpace(), x => x.SysOrgPn.Contains($"{search.SysPn}"))
                  .AndIf(search.SysBin.IsNotNullOrWhiteSpace(), x => x.SysBin.Contains($"{search.SysBin}"));
        }

        public async Task<List<InventoryDto>> GetPrimaryDataDtoBySn(string ScanSn, string oldSn,string BoxName,string ScanPallet,string projectName)
        {
            //ScanSn= ScanSn.ToUpper();
            //oldSn = oldSn.ToUpper();
            var deptFullName = (await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId))?.Profile?.DeptFullName;
            var pro =await _projectRepostitory.FindAsync(x => x.Name == projectName);
            var primaryData =new List<PrimaryData>();
            if (!string.IsNullOrEmpty(pro?.EndShield))
            {
                primaryData = Repository.Where(x => x.SnState != "已盘点" && x.PiDpt == deptFullName.ToString() && (x.SysSn.StartsWith(ScanSn) || x.SysOrgSn.StartsWith(ScanSn) || x.SysSn.StartsWith(oldSn) || x.SysOrgSn.StartsWith(oldSn))).ToList();
                primaryData = primaryData.Where(x => (x.SysSn == ScanSn || x.SysPn == oldSn || x.SysOrgSn == ScanSn || x.SysOrgSn == oldSn) ||
                                                    (x.SysSn.StartsWith(ScanSn + "-") || x.SysPn.StartsWith(oldSn + "-") || x.SysOrgSn.StartsWith(ScanSn + "-") || x.SysOrgSn.StartsWith(oldSn + "-"))).ToList();
            }
            else
            {
                primaryData = Repository.Where(x => x.SnState != "已盘点" && x.PiDpt == deptFullName.ToString() && (x.SysSn==(ScanSn) || x.SysOrgSn == (ScanSn) || x.SysSn==(oldSn) || x.SysOrgSn==(oldSn))).ToList();
            }
           
            //primaryData = Repository.Where(x => x.SnState != "已盘点" && x.PiDpt == deptFullName.ToString() && (x.SysSn.Contains(ScanSn) || x.SysOrgSn.Contains(ScanSn) || x.SysSn.Contains(oldSn) || x.SysOrgSn.Contains(oldSn))).ToList();
            if (primaryData.Any())
            {
                var inventoryDtos= primaryData.Select(x => new InventoryDto()
                {
                    AccountBook = x.AccountBook,
                    Id = x.Id,
                    Name = x.AccountBook,
                    CreateDept = x.CreateDept,
                    CreateTime = x.CreateTime,
                    Creator = x.Creator,
                    BoxName = BoxName,
                    ScanPallet = ScanPallet,
                    ScanPn = "",
                    ScanSn = ScanSn,
                    Description = x.Description,
                    PiNum = x.PiNum,
                    PiDpt = x.PiDpt,
                    PiProject = x.PiProject,
                    Source = x.Source,
                    SysBin = x.SysBin,
                    SysLocation = x.SysLocation,
                    SysOrgPn = x.SysOrgPn,
                    SysOrgSn = x.SysOrgSn,
                    SysPn = x.SysPn,
                    SysSn = x.SysSn,
                    Editor = x.Editor,
                    EditTime = x.EditTime,
                    FilingNo = x.FilingNo,
                });
                return inventoryDtos.ToList();
            }
            else
            {
                return new List<InventoryDto> ();
            }
            
        }

        /// <summary>
        /// 表格label
        /// </summary>
        /// <returns></returns>
        public async Task<List<DictDto>> GetTableLabelDynamic()
        {
            var status = new List<DictDto>();
            var dictioary = await _dictionaryRepository.FindAsync(x => x.Name == TABLELABELDYNAMIC);
            if (dictioary != null)
            {
                var items = _dictionaryRepository.Where(x => x.Pid == dictioary.Id).ToList();
                status = ObjectMapper.Map<List<DictDto>>(items);
            }
            return status;
        }

        /// <summary>
        /// 导入excel
        /// </summary>
        /// <returns></returns>
        public async Task<ReturnTemplate> ExcelImport(IFormFile file)
        {
            //返回模板
            //1重复sn未盘点需要确认修改信息  原数据 新数据
            //2数据有空值 提示列
            //3项目中有使用厂内sn兵有空值  提示到项目
            //4导入数据中有重复的sn 厂内sn   原数据 新数据
            //5导入数据有重复sn并且已被盘点   提示到sn
            ///6批次号内有已经被已盘点的数据   提示到批次号
            ReturnTemplate returnTemplate = new ReturnTemplate();
            var seroalno = "";
            Validate.Assert(file == null || file.Length == 0, "上传文件是空");
            var tableByImprot = FormFileExtension.ExcelToDataTable(file);

            if (tableByImprot.ListNum.Any() && tableByImprot.ListNum.Count() != 1)//导入的批次号只能说一个或一种
            {
                returnTemplate.State = -2;
                returnTemplate.Message = "本次导入批次号只能说一个导入多个批次号";
                returnTemplate.ListStrings = tableByImprot.ListNum;
                return returnTemplate;
            }
            if (tableByImprot.ListIndex.Any())//返回里面空的数据return
            {
                returnTemplate.State = -2;
                returnTemplate.Message = "导入数据中有空值请检查Excel以下列的值";
                returnTemplate.ListStrings = tableByImprot.ListIndex;
                return returnTemplate;
            }
            var userinfo = await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId);
            if (tableByImprot.ListNum.Count == 1)
            {
                seroalno = tableByImprot.ListNum[0];
            }
            if (tableByImprot.ListNum.Count == 0)
            {
                var maxcode = (await _AtchNoService.GetLastAtchNo());
                seroalno = SerialNoHelper.GetSerialno(maxcode?.Name, "PI");
            }
            //模板确认批次好是否存在
            List<PrimaryData> list = FormFileExtension.ConvertDataTableToList<PrimaryData>(tableByImprot.dataTable, userinfo?.Profile?.DeptFullName, seroalno);

            var listNum = await _AtchNoService.GetAtchNoState(tableByImprot.ListNum);//已关闭的批次号返回
            if (listNum.Any())
            {
                if (listNum.Where(x => x.State == "已关闭").Any())
                {
                    returnTemplate.State = -2;
                    returnTemplate.Message = "导入数据批次号已被盘点";
                    returnTemplate.ListStrings = listNum.Select(x => x.Name).ToList();
                    return returnTemplate;
                }
                else
                {
                    var duplicateSysOrgSn = list
                           .GroupBy(x => x.SysOrgSn)
                           .Where(g => g.Count() > 1)
                           .Select(g => g.Key)
                           .ToList();
                    var duplicateNames = list
                                  .GroupBy(x => x.SysSn)
                                  .Where(g => g.Count() > 1)
                                  .Select(g => g.Key)
                                  .ToList();
                    if (duplicateSysOrgSn.Any() || duplicateNames.Any())//导入数据中存在重复的希捷SN
                    {
                        var mess = duplicateSysOrgSn.Any() ? "希捷SN" : "导入SN";
                        returnTemplate.State = -2;
                        returnTemplate.Message = $"导入数据中存在重复的{mess}";
                        returnTemplate.ListStrings = duplicateSysOrgSn.Any() ? duplicateSysOrgSn: duplicateNames;
                        return returnTemplate;
                        // Validate.Assert(duplicateNames.Any(), "再导入数据中有sn重复不能导入");
                    }
                    else
                    {
                        var resultsOrgSn = Repository.Where(e => tableByImprot.ListOrgPnSn.Contains(e.SysOrgSn) && tableByImprot.ListNum.Contains(e.PiNum)).ToList();//查数据重复希捷sn
                        if (resultsOrgSn.Any())
                        {
                            if (resultsOrgSn.Where(x => x.SnState == "已盘点").Any()) //返回具体的sn
                            {
                                returnTemplate.State = -2;
                                returnTemplate.Message = "导入数据下希捷SN已被盘点";
                                returnTemplate.ListStrings = resultsOrgSn.Where(x => x.SnState == "已盘点").Select(x => x.SysSn).ToList();
                                return returnTemplate;
                                //Validate.Assert(results.Where(x => x.SnState == "已盘点").Any(), "再导入数据中有sn有已经被盘点不能导入");
                            }
                            else
                            {
                                var improtList = list.Where(e => resultsOrgSn.Select(x => x.SysSn).Contains(e.SysSn));
                                returnTemplate.State = -1;
                                returnTemplate.Message = "请确认是否更改sn的信息";
                                returnTemplate.PrimaryOldDatas = resultsOrgSn;
                                returnTemplate.PrimaryNewDatas = improtList.ToList();
                                return returnTemplate;
                            }
                        }

                        var resultsSn = Repository.Where(e => tableByImprot.ListSn.Contains(e.SysSn) && tableByImprot.ListNum.Contains(e.PiNum)).ToList();//查数据重复sn
                        if (resultsSn.Any())
                        {
                            if (resultsSn.Where(x => x.SnState == "已盘点").Any()) //返回具体的sn
                            {
                                returnTemplate.State = -2;
                                returnTemplate.Message = "导入数据下SN已被盘点";
                                returnTemplate.ListStrings = resultsSn.Where(x => x.SnState == "已盘点").Select(x => x.SysSn).ToList();
                                return returnTemplate;
                                //Validate.Assert(results.Where(x => x.SnState == "已盘点").Any(), "再导入数据中有sn有已经被盘点不能导入");
                            }
                            else
                            {
                                var improtList = list.Where(e => resultsSn.Select(x => x.SysSn).Contains(e.SysSn));
                                returnTemplate.State = -1;
                                returnTemplate.Message = "请确认是否更改sn的信息";
                                returnTemplate.PrimaryOldDatas = resultsSn;
                                returnTemplate.PrimaryNewDatas = improtList.ToList();
                                return returnTemplate;
                            }
                        }
                    }
                }
            }
            else
            {
                var duplicateSysOrgSn = list
                            .GroupBy(x => x.SysOrgSn)
                            .Where(g => g.Count() > 1)
                            .Select(g => g.Key)
                            .ToList();
                var duplicateNames = list
                              .GroupBy(x => x.SysSn)
                              .Where(g => g.Count() > 1)
                              .Select(g => g.Key)
                              .ToList();
                if (duplicateSysOrgSn.Any() || duplicateNames.Any())//导入数据中存在重复的希捷SN
                {
                    var mess = duplicateSysOrgSn.Any() ? "希捷SN" : "导入SN";
                    returnTemplate.State = -2;
                    returnTemplate.Message = $"导入数据中存在重复的{mess}";
                    returnTemplate.ListStrings = duplicateSysOrgSn.Any() ? duplicateSysOrgSn : duplicateNames;
                    return returnTemplate;
                    // Validate.Assert(duplicateNames.Any(), "再导入数据中有sn重复不能导入");
                }
            }

            var createAtchNoDto = new AtchNoDto()
            {
                Name = seroalno,
                State = "导入",
                DeptName = userinfo?.Profile?.DeptFullName,
                AutomaticGeneration = tableByImprot.ListNum.Count() == 0 || (tableByImprot.ListNum[0] != seroalno),
            };

            await BulkInsertAndUpdata(list, createAtchNoDto, tableByImprot.ListBin, tableByImprot.ListLocation, tableByImprot.ListProject);
            returnTemplate.State = 0;
            returnTemplate.InsertCount = list.Count;
            return returnTemplate;
        }

        /// <summary>
        /// 确认导入excel
        /// </summary>
        /// <returns></returns>
        public async Task<ReturnTemplate> ConfirmExcelImport(IFormFile file)
        {
            //返回模板
            //1重复sn未盘点需要确认修改信息  原数据 新数据
            //2数据有空值 提示列
            //3项目中有使用厂内sn兵有空值  提示到项目
            //4导入数据中有重复的sn 厂内sn   原数据 新数据
            //5导入数据有重复sn并且已被盘点   提示到sn
            ///6批次号内有已经被已盘点的数据   提示到批次号
            ReturnTemplate returnTemplate = new ReturnTemplate();
            var seroalno = "";
            Validate.Assert(file == null || file.Length == 0, "上传文件是空");
            var tableByImprot = FormFileExtension.ExcelToDataTable(file);

            if (tableByImprot.ListNum.Any() && tableByImprot.ListNum.Count() != 1)//导入的批次号只能说一个或一种
            {
                returnTemplate.State = -2;
                returnTemplate.Message = "本次导入批次号只能说一个导入多个批次号";
                returnTemplate.ListStrings = tableByImprot.ListIndex;
                return returnTemplate;
            }
            if (tableByImprot.ListIndex.Any())//返回里面空的数据return
            {
                returnTemplate.State = -2;
                returnTemplate.Message = "导入数据中有空值请检查Excel以下列的值";
                returnTemplate.ListStrings = tableByImprot.ListIndex;
                return returnTemplate;
            }
            var userinfo = await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId);
            if (tableByImprot.ListNum.Count == 1)
            {
                seroalno = tableByImprot.ListNum[0];
            }
            if (tableByImprot.ListNum.Count == 0)
            {
                var maxcode = (await _AtchNoService.GetLastAtchNo());
                seroalno = SerialNoHelper.GetSerialno(maxcode?.Name, "PI");
            }
            List<PrimaryData> list = FormFileExtension.ConvertDataTableToList<PrimaryData>(tableByImprot.dataTable, userinfo.Profile.DeptFullName, seroalno);

            var listNum = await _AtchNoService.GetAtchNoState(tableByImprot.ListNum);//已关闭的批次号返回

            if (listNum.Any())
            {
                if (listNum.Where(x => x.State == "已关闭").Any())
                {
                    returnTemplate.State = -2;
                    returnTemplate.Message = "导入数据批次号已被盘点";
                    returnTemplate.ListStrings = listNum.Select(x => x.Name).ToList();
                    return returnTemplate;
                }
                else
                {
                    var duplicateSysOrgSn = list
                           .GroupBy(x => x.SysOrgSn)
                           .Where(g => g.Count() > 1)
                           .Select(g => g.Key)
                           .ToList();
                    var duplicateNames = list
                                  .GroupBy(x => x.SysSn)
                                  .Where(g => g.Count() > 1)
                                  .Select(g => g.Key)
                                  .ToList();
                    if (duplicateSysOrgSn.Any() || duplicateNames.Any())//导入数据中存在重复的希捷SN
                    {
                        var mess = duplicateSysOrgSn.Any() ? "希捷SN" : "导入SN";
                        returnTemplate.State = -2;
                        returnTemplate.Message = $"导入数据中存在重复的{mess}";
                        returnTemplate.ListStrings = duplicateSysOrgSn.Any() ? duplicateSysOrgSn : duplicateNames;
                        return returnTemplate;
                        // Validate.Assert(duplicateNames.Any(), "再导入数据中有sn重复不能导入");
                    }
                    else
                    {
                        var resultsOrgSn = Repository.Where(e => tableByImprot.ListOrgPnSn.Contains(e.SysOrgSn) && tableByImprot.ListNum.Contains(e.PiNum)).ToList();//查数据重复希捷sn
                        if (resultsOrgSn.Any())
                        {
                            if (resultsOrgSn.Where(x => x.SnState == "已盘点").Any()) //返回具体的sn
                            {
                                returnTemplate.State = -2;
                                returnTemplate.Message = "导入数据下希捷SN已被盘点";
                                returnTemplate.ListStrings = resultsOrgSn.Where(x => x.SnState == "已盘点").Select(x => x.SysSn).ToList();
                                return returnTemplate;
                                //Validate.Assert(results.Where(x => x.SnState == "已盘点").Any(), "再导入数据中有sn有已经被盘点不能导入");
                            }
                            else
                            {
                                var improtList = list.Where(e => resultsOrgSn.Select(x => x.SysSn).Contains(e.SysSn));

                                var updteList = resultsOrgSn.Join(improtList, i => i.SysSn, e => e.SysSn, (i, e) => new PrimaryData
                                {
                                    Id = i.Id,
                                    Name = i.Name,
                                    CreateTime = i.CreateTime,
                                    Creator = i.Creator,
                                    EditTime = DateTime.Now,
                                    Editor = Framework.Security.UserTokenService.GetUserToken().UserName,
                                    SnState = e.SnState,
                                    Source = e.Source,
                                    SysBin = e.SysBin,
                                    SysLocation = e.SysLocation,
                                    SysOrgPn = e.SysOrgPn,
                                    SysOrgSn = e.SysOrgSn,
                                    SysPn = e.SysPn,
                                    SysSn = e.SysSn,
                                    AccountBook = e.AccountBook,
                                    CreateDept = e.CreateDept,
                                    Description = e.Description,
                                    FilingNo = e.FilingNo,
                                    PiDpt = e.PiDpt,
                                    PiNum = e.PiNum,
                                    PiProject = e.PiProject,
                                    //...
                                });
                                var improtNewList = list.Except(improtList);
                                var createAtchNoDto = new AtchNoDto()
                                {
                                    Name = seroalno,
                                    State = "导入",
                                    DeptName = userinfo?.Profile?.DeptFullName,
                                    AutomaticGeneration = tableByImprot.ListNum.Count() == 0 || (tableByImprot.ListNum[0] != seroalno),
                                };
                                await BulkInsertAndUpdata(improtNewList.ToList(), createAtchNoDto, tableByImprot.ListBin, tableByImprot.ListLocation, tableByImprot.ListProject, updteList.ToList());
                                returnTemplate.State = 0;
                                returnTemplate.InsertCount = improtNewList.Count();
                                returnTemplate.UpdateCount = updteList.Count();
                                return returnTemplate;
                            }
                        }

                        var resultsSn = Repository.Where(e => tableByImprot.ListSn.Contains(e.SysSn) && tableByImprot.ListNum.Contains(e.PiNum)).ToList();//查数据重复sn
                        if (resultsSn.Any())
                        {
                            if (resultsSn.Where(x => x.SnState == "已盘点").Any()) //返回具体的sn
                            {
                                returnTemplate.State = -2;
                                returnTemplate.Message = "导入数据下SN已被盘点";
                                returnTemplate.ListStrings = resultsSn.Where(x => x.SnState == "已盘点").Select(x => x.SysSn).ToList();
                                return returnTemplate;
                                //Validate.Assert(results.Where(x => x.SnState == "已盘点").Any(), "再导入数据中有sn有已经被盘点不能导入");
                            }
                            else
                            {
                                var improtList = list.Where(e => resultsSn.Select(x => x.SysSn).Contains(e.SysSn));

                                var updteList = resultsSn.Join(improtList, i => i.SysSn, e => e.SysSn, (i, e) => new PrimaryData
                                {
                                    Id = i.Id,
                                    Name = i.Name,
                                    CreateTime = i.CreateTime,
                                    Creator = i.Creator,
                                    EditTime = DateTime.Now,
                                    Editor = Framework.Security.UserTokenService.GetUserToken().UserName,
                                    SnState = e.SnState,
                                    Source = e.Source,
                                    SysBin = e.SysBin,
                                    SysLocation = e.SysLocation,
                                    SysOrgPn = e.SysOrgPn,
                                    SysOrgSn = e.SysOrgSn,
                                    SysPn = e.SysPn,
                                    SysSn = e.SysSn,
                                    AccountBook = e.AccountBook,
                                    CreateDept = e.CreateDept,
                                    Description = e.Description,
                                    FilingNo = e.FilingNo,
                                    PiDpt = e.PiDpt,
                                    PiNum = e.PiNum,
                                    PiProject = e.PiProject,
                                    //...
                                });
                                var improtNewList = list.Except(improtList);
                                var createAtchNoDto = new AtchNoDto()
                                {
                                    Name = seroalno,
                                    State = "导入",
                                    DeptName = userinfo?.Profile?.DeptFullName,
                                    AutomaticGeneration = tableByImprot.ListNum.Count() == 0 || (tableByImprot.ListNum[0] != seroalno),
                                };
                                await BulkInsertAndUpdata(improtNewList.ToList(), createAtchNoDto, tableByImprot.ListBin, tableByImprot.ListLocation, tableByImprot.ListProject, updteList.ToList());
                                returnTemplate.State = 0;
                                returnTemplate.InsertCount = improtNewList.Count();
                                returnTemplate.UpdateCount = updteList.Count();
                                return returnTemplate;
                            }
                        }
                    }
                }
            }
            else
            {
                var duplicateSysOrgSn = list
                            .GroupBy(x => x.SysOrgSn)
                            .Where(g => g.Count() > 1)
                            .Select(g => g.Key)
                            .ToList();
                var duplicateNames = list
                              .GroupBy(x => x.SysSn)
                              .Where(g => g.Count() > 1)
                              .Select(g => g.Key)
                              .ToList();
                if (duplicateSysOrgSn.Any() || duplicateNames.Any())//导入数据中存在重复的希捷SN
                {
                    var mess = duplicateSysOrgSn.Any() ? "希捷SN" : "导入SN";
                    returnTemplate.State = -2;
                    returnTemplate.Message = $"导入数据中存在重复的{mess}";
                    returnTemplate.ListStrings = duplicateSysOrgSn.Any() ? duplicateSysOrgSn : duplicateNames;
                    return returnTemplate;
                    // Validate.Assert(duplicateNames.Any(), "再导入数据中有sn重复不能导入");
                }
            }

            //var s2s = await Repository.BulkInsertAsync(list);
            //var s2s2 = await _AtchNoService.AddAsync(new CreateAtchNoDto() { Name = seroalno, State = "导入", AutomaticGeneration = seroalno == tableByImprot.ListNum[0] ? false : true });
            ////判断bin 库位,没有需要新建

            returnTemplate.State = 0;
            return returnTemplate;
        }

        /// <summary>
        /// 批量保存数据
        /// </summary>
        /// <param name="improtNewList"></param>
        /// <param name="updteList"></param>
        /// <param name="createAtchNoDto"></param>
        public async Task BulkInsertAndUpdata(List<PrimaryData> improtNewList, AtchNoDto createAtchNoDto, List<string> listBins, List<string> listLocations, List<string> listProjects, List<PrimaryData> updteList = null)
        {
            //await _AtchNoService.AddAsync(createAtchNoDto);
            await Repository.BulkInsertAsync(improtNewList);
            if (updteList != null)
                await Repository.BulkUpdateAsync(updteList);
            await _AtchNoService.IsCreateAtch(createAtchNoDto, improtNewList.Count());
            //await _projectService.GetProjects(listProjects);
            await _areaService.GetAreas(listLocations);
            await _warehouseService.GetWarehouses(listBins);
        }

        public async Task BulkUpdateAsync(List<PrimaryData> improtNewList)
        {
            await Repository.BulkUpdateAsync(improtNewList);
        }

        public  async Task<PageModelDto<PrimaryDataDto>> GetPagedAsyncByExport(PrimaryDataSearchPagedDto search)
        {
            var whereExpression = BuildWhereExpression(ExpressionCreator.New<PrimaryData>(), search);

            var total = await Repository.CountAsync(whereExpression);
            if (total == 0) return new PageModelDto<PrimaryDataDto>(search);

            var includeProperties = BuildIncludeProperties();

            var entities = Repository.Where(whereExpression, includeProperties)
                                     .OrderBy(x => x.Id)
                                     .ToList();

            var entityDtos = await MapToEntityDto(entities);
            return new PageModelDto<PrimaryDataDto>(search, entityDtos, total);
        }
    }
}