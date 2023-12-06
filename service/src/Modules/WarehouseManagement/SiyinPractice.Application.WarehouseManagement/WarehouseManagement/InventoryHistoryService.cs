using ConnmIntel.Application.Core;
using ConnmIntel.Domain.Core;
using ConnmIntel.Domain.Maintenance;
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Framework.Mapper;
using ConnmIntel.Interface.AccessControl;
using ConnmIntel.Interface.BasicData;
using ConnmIntel.Interface.WarehouseManagement;
using ConnmIntel.Shared.Core.Dto;
using ConnmIntel.Shared.Core.Exceptions;
using ConnmIntel.Shared.Core.Utility;
using ConnmIntel.Shared.Maintenance.Dto;
using ConnmIntel.Shared.WarehouseManagement.Dto.InventoryHistory;
using ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConnmIntel.Application.WarehouseManagement.WarehouseManagement
{
    public class InventoryHistoryService : NamedEntityService<InventoryHistory, InventoryHistoryDto, InventoryHistorySearchPagedDto, CreateInventoryHistoryDto>, IInventoryHistoryService
    {
        private readonly IUserAppService _userAppService;
        private readonly IEfRepository<PrimaryData> _efRepository;
        private readonly IEfRepository<SysDict> _dictionaryRepository;
        private string DATAQUERYTABLELABEL = "DataQueryTableLabel";
        private string DATAQUERYTABLELABELBYAUXILIARY = "DataQueryTableLabelByAuxiliary";
        public InventoryHistoryService(IEfRepository<InventoryHistory> repository, IObjectMapper objectMapper, IEfRepository<PrimaryData> efRepository,
            IEfRepository<SysDict> dictionaryRepository, IUserAppService userAppService) : base(repository, objectMapper)
        {
            _efRepository = efRepository;
            _dictionaryRepository = dictionaryRepository;
            _userAppService= userAppService;
        }
        protected override Expression<Func<InventoryHistory, bool>> BuildWhereExpression(Expression<Func<InventoryHistory, bool>> whereExpression, InventoryHistorySearchPagedDto search)
        {
            var deptFullName = _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId)
                                             .GetAwaiter()
                                             .GetResult()?.Profile?.DeptFullName;
            return base.BuildWhereExpression(whereExpression, search)
                 .AndIf(search.BeginTime.IsNotNullOrWhiteSpace(), x => x.CreateTime >= Convert.ToDateTime(search.BeginTime))
                  .AndIf(search.EndTime.IsNotNullOrWhiteSpace(), x => x.CreateTime <= Convert.ToDateTime(search.EndTime))
                  .AndIf(true,x => x.SnState=="已盘点")
                  .AndIf(search.DeptName.IsNotNullOrWhiteSpace(), x => x.CreateDept.Contains($"{search.DeptName}"))
                  .AndIf(deptFullName.IsNotNullOrWhiteSpace(), x => x.PiDpt==($"{deptFullName}"))
                  .AndIf(search.ProjectName.IsNotNullOrWhiteSpace(), x => x.ProjectName == ($"{search.ProjectName}"))
                  .AndIf(search.Location.IsNotNullOrWhiteSpace(), x => x.SysLocation==($"{search.Location}"))
                  .AndIf(search.ScanSn.IsNotNullOrWhiteSpace(), x => x.ScanSn.Contains($"{search.ScanSn}"))
                  .AndIf(search.BoxName.IsNotNullOrWhiteSpace(), x => x.BoxName==($"{search.BoxName}"))
                  .AndIf(search.Pallet.IsNotNullOrWhiteSpace(), x => x.ScanPallet==($"{search.Pallet}"))
                  .AndIf(search.ScanPn.IsNotNullOrWhiteSpace(), x => x.ScanPn.Contains($"{search.ScanPn}"))
                  .AndIf(search.Creator.IsNotNullOrWhiteSpace(), x => x.Creator.Contains($"{search.Creator}"))
                  .AndIf(search.SysBin.IsNotNullOrWhiteSpace(), x => x.SysBin== ($"{search.SysBin}"));
        }

        public async Task<InventoryHistoryDto> CreateErrorSnHistory(CreateInventoryHistoryDto createInput)
        {
            var exits =await _efRepository.FindAsync(x => x.SysSn == createInput.ScanSn);
            if (exits!=null)
            {
                var deptFullName = (await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId))?.Profile?.DeptFullName;
                if(exits.PiDpt!= deptFullName)
                {
                    createInput.SnState = "导入SN的部门:" + exits.PiDpt + "和该账号的部门:"+ deptFullName+"不一致";
                }
                else
                {
                    createInput.SnState = "SN" + createInput.ScanSn + "已盘点";
                }
                //createInput.Source = exits.Source;
            }
            var entity = await MapToEntity(createInput);
            if (entity is AuditEntity auditEntity)
            {
                auditEntity.Creator = Framework.Security.UserTokenService.GetUserToken().UserName;
                auditEntity.CreateTime = DateTime.Now;
            }
            await Repository.InsertAsync(entity);
            return await MapToEntityDto(entity);
        }
        public async Task<int> CreateHistory(CreateInventoryHistoryDto createInput)
        {
            Validate.Assert(createInput == null, ConnmIntelMessage.DTO_IS_NULL);
           
            var entity = await MapToEntity(createInput);
            if (entity is AuditEntity auditEntity)
            {
                auditEntity.Creator = Framework.Security.UserTokenService.GetUserToken().UserName;
                auditEntity.CreateTime = DateTime.Now;
            }
            return await Repository.InsertAsync(entity);
        }

        public async Task BulkInsertAsync(List<InventoryHistory> inventoryHistories)
        {
            await Repository.BulkInsertAsync(inventoryHistories);
        }
        /// <summary>
        /// 表格label
        /// </summary>
        /// <returns></returns>
        public async Task<List<DictDto>> GetDataQueryTableLabel()
        {
            var status = new List<DictDto>();
            var dictioary = await _dictionaryRepository.FindAsync(x => x.Name == DATAQUERYTABLELABEL);
            if (dictioary != null)
            {
                var items = _dictionaryRepository.Where(x => x.Pid == dictioary.Id).ToList();
                status = ObjectMapper.Map<List<DictDto>>(items);
            }
            return status;
        }
        public async Task<List<DictDto>> GetDataQueryTableLabelByAuxiliary()
        {
            var status = new List<DictDto>();
            var dictioary = await _dictionaryRepository.FindAsync(x => x.Name == DATAQUERYTABLELABELBYAUXILIARY);
            if (dictioary != null)
            {
                var items = _dictionaryRepository.Where(x => x.Pid == dictioary.Id).ToList();
                status = ObjectMapper.Map<List<DictDto>>(items);
            }
            return status;
        }
        public async Task<PageModelDto<InventoryHistoryDto>> GetPagedAsyncByExport(InventoryHistorySearchPagedDto search)
        {
            var whereExpression = BuildWhereExpression(ExpressionCreator.New<InventoryHistory>(), search);

            var total = await Repository.CountAsync(whereExpression);
            if (total == 0) return new PageModelDto<InventoryHistoryDto>(search);

            var includeProperties = BuildIncludeProperties();

            var entities = Repository.Where(whereExpression, includeProperties)
                                     .OrderBy(x => x.Id)
                                     .ToList();

            var entityDtos = await MapToEntityDto(entities);
            return new PageModelDto<InventoryHistoryDto>(search, entityDtos, total);
        }
    }
}