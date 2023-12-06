using ConnmIntel.Application.Core;
using ConnmIntel.Domain.Core;
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Framework.Mapper;
using ConnmIntel.Interface.AccessControl;
using ConnmIntel.Interface.WarehouseManagement;
using ConnmIntel.Shared.WarehouseManagement.Dto.Auxiliary;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using ConnmIntel.Shared.Core.Exceptions;
using ConnmIntel.Shared.Core.Utility;
using ConnmIntel.Shared.WarehouseManagement.Dto.AtchNo;

namespace ConnmIntel.Application.WarehouseManagement.WarehouseManagement
{
    internal class AuxiliaryAtchService : NamedEntityService<AuxiliaryAtch, AuxiliaryAtchDto, AuxiliaryAtchSearchPagedDto, CreateAuxiliaryAtchDto>, IAuxiliaryAtchService
    {
        private readonly IUserAppService _userAppService;
        public AuxiliaryAtchService(IEfRepository<AuxiliaryAtch> repository, IObjectMapper objectMapper, IUserAppService userAppService) : base(repository, objectMapper)
        {
            _userAppService = userAppService;
        }
        public async Task<AuxiliaryAtchDto> GetLastAuxiliaryAtch()
        {
            var deptFullName = (await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId))?.Profile?.DeptFullName;
            AuxiliaryAtchDto atchNo = new();
            var entity = Repository.Where(x => x.DeptName == deptFullName).OrderByDescending(x => x.CreateTime).FirstOrDefault();//查数据重复sn
            atchNo = await MapToEntityDto(entity);
            return atchNo;
        }
        public async Task<int> IsCreateAtch(AuxiliaryAtchDto auxiliaryAtchNoDto)
        {
            Validate.Assert(auxiliaryAtchNoDto == null, ConnmIntelMessage.DTO_IS_NULL);
            // var exits = await Repository.AnyAsync(x => x.Name == atchNoDto.Name);
            var first = await Repository.FindAsync(x => x.Name == auxiliaryAtchNoDto.Name);

            if (first != null)
            {
                
                first.EditTime = DateTime.Now;
                first.Editor = Framework.Security.UserTokenService.GetUserToken().UserName;
                return await Repository.UpdateAsync(first);
            }
            else
            {
                var entity = await MapToEntity(auxiliaryAtchNoDto);
                entity.Id = Guid.NewGuid();
                if (entity is AuditEntity auditEntity)
                {
                    auditEntity.Creator = Framework.Security.UserTokenService.GetUserToken().UserName;
                    auditEntity.CreateTime = DateTime.Now;
                }
                return await Repository.InsertAsync(entity);
            }
        }
    }
}
