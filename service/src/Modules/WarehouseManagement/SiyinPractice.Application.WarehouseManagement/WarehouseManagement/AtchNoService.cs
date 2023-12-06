using ConnmIntel.Application.Core;
using ConnmIntel.Domain.Core;
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Framework.Mapper;
using ConnmIntel.Interface.AccessControl;
using ConnmIntel.Interface.WarehouseManagement;
using ConnmIntel.Shared.Core.Dto;
using ConnmIntel.Shared.Core.Exceptions;
using ConnmIntel.Shared.Core.Utility;
using ConnmIntel.Shared.WarehouseManagement.Dto.AtchNo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnmIntel.Application.WarehouseManagement.WarehouseManagement
{
    internal class AtchNoService : NamedEntityService<AtchNo, AtchNoDto, AtchNoSearchPagedDto, CreateAtchNoDto>, IAtchNoService
    {
        private readonly IUserAppService _userAppService;
        public AtchNoService(IEfRepository<AtchNo> repository, IObjectMapper objectMapper, IUserAppService userAppService) : base(repository, objectMapper)
        {
            _userAppService = userAppService;
        }
        public async Task<int> IsCreateAtch(AtchNoDto atchNoDto,int count)
        {
            Validate.Assert(atchNoDto == null, ConnmIntelMessage.DTO_IS_NULL);
           // var exits = await Repository.AnyAsync(x => x.Name == atchNoDto.Name);
            var first = await Repository.FindAsync(x => x.Name == atchNoDto.Name);

            if (first!=null)
            {
                first.TotalInventory += count;
                first.EditTime = DateTime.Now;
                first.Editor = Framework.Security.UserTokenService.GetUserToken().UserName;
                return await Repository.UpdateAsync(first);
            }
            else
            {
                var entity = await MapToEntity(atchNoDto);
                entity.Id=Guid.NewGuid();
                entity.TotalInventory = count;
                if (entity is AuditEntity auditEntity)
                {
                    auditEntity.Creator = Framework.Security.UserTokenService.GetUserToken().UserName;
                    auditEntity.CreateTime = DateTime.Now;
                }
                return await Repository.InsertAsync(entity);
            }
        }
        public async Task<List<AtchNoDto>> GetAtchNoState(List<string> strings)
        {
            var results = Repository.Where( e => strings.Contains(e.Name)).ToList();//查数据重复sn
            return await MapToEntityDto(results);
            //return results;
        }
        public async Task<int> UpdateAtchNoState(string name)
        {
            var deptFullName = (await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId))?.Profile?.DeptFullName;
            var exits = await Repository.AnyAsync(x => x.Name == name&&x.DeptName== deptFullName);
            Validate.Assert(!exits, $"该部门{deptFullName}下的批次号{name}不存在");
            var atchno=await Repository.FindAsync(x=>x.Name == name);
            Validate.Assert(atchno.State == "已关闭", $"{atchno.Name}已关闭");
            atchno.State = "已关闭";
            atchno.EditTime = DateTime.Now;
            atchno.Editor = Framework.Security.UserTokenService.GetUserToken().UserName;
            return await Repository.UpdateAsync(atchno);

        }
        public async Task<AtchNoDto> GetLastAtchNo()
        {
            //var deptFullName = (await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId))?.Profile?.DeptFullName;
            AtchNoDto atchNo = new();
           var entity = Repository.Where(x=>x.AutomaticGeneration ==true).OrderByDescending(x=>x.CreateTime).FirstOrDefault();//查数据重复sn
            atchNo=await MapToEntityDto(entity);
            return atchNo;
        }
        public List<AtchNo> CreateAtchNo(List<string> strings,bool automaticGeneration=false)
        {
            List<AtchNo> atchNos= new List<AtchNo>();
            foreach (string item in strings)
            {
                AtchNo atchNoatch = new AtchNo();
                atchNoatch.Id = Guid.NewGuid();
                atchNoatch.Name = item;
                atchNoatch.State = "导入";
                atchNoatch.CreateTime = DateTime.Now;
                atchNoatch.AutomaticGeneration = automaticGeneration;
                atchNoatch.Creator = Framework.Security.UserTokenService.GetUserToken().UserName;
                atchNos.Add(atchNoatch);
            }
            return atchNos;
        }
    }
}