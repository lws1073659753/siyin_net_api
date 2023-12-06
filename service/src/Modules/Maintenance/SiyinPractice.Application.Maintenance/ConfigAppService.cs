using SiyinPractice.Application.Core;
using SiyinPractice.Domain.Core;
using SiyinPractice.Domain.Maintenance;
using SiyinPractice.Framework.Security;
using SiyinPractice.Interface.Maintenance;
using SiyinPractice.Shared.Core.Dto;
using SiyinPractice.Shared.Core.Exceptions;
using SiyinPractice.Shared.Core.Utility;
using SiyinPractice.Shared.Maintenance.Dto;
using System.Linq.Expressions;

namespace Adnc.Maint.Application.Services;

public class ConfigAppService : AppService, IConfigAppService
{
    private readonly IEfRepository<SysCfg> _cfgRepository;

    public ConfigAppService(
        IEfRepository<SysCfg> cfgRepository)
    {
        _cfgRepository = cfgRepository;
    }

    public async Task<Guid> CreateAsync(CfgCreationDto input)
    {
        var exists = await _cfgRepository.AnyAsync(x => x.Name.Equals(input.Name));
        Validate.Assert(exists, "参数名称已经存在");

        var cfg = Mapper.Map<SysCfg>(input);
        cfg.Id = Guid.NewGuid();
        if (cfg is AuditEntity auditEntity)
        {
            auditEntity.Creator = UserTokenService.GetUserToken().UserName;
            auditEntity.CreateTime = DateTime.Now;
        }
        await _cfgRepository.InsertAsync(cfg);

        return cfg.Id;
    }

    public async Task<int> UpdateAsync(Guid id, CfgUpdationDto input)
    {
        var exists = await _cfgRepository.AnyAsync(c => c.Name.Equals(input.Name.Trim()) && c.Id != id);
        Validate.Assert(exists, "参数名称已经存在");

        var entity = Mapper.Map<SysCfg>(input);
        if (entity is AuditEntity auditEntity)
        {
            auditEntity.Editor = UserTokenService.GetUserToken().UserName;
            auditEntity.EditTime = DateTime.Now;
        }
        entity.Id = id;
        var updatingProps = UpdatingProps<SysCfg>(x => x.Name, x => x.Value, x => x.Description);
        return await _cfgRepository.UpdateAsync(entity, updatingProps);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        return await _cfgRepository.DeleteAsync(id);
    }

    public async Task<CfgDto> GetAsync(Guid id)
    {
        var entity = await _cfgRepository.FindAsync(id);
        return Mapper.Map<CfgDto>(entity);
    }

    public async Task<PageModelDto<CfgDto>> GetPagedAsync(CfgSearchPagedDto search)
    {
        var whereExpression = ExpressionCreator
            .New<SysCfg>()
            .AndIf(search.Name.IsNotNullOrWhiteSpace(), x => x.Name.Contains($"{search.Name}"))
            .AndIf(search.Value.IsNotNullOrWhiteSpace(), x => x.Value.Contains($"{search.Value}"));

        var total = await _cfgRepository.CountAsync(whereExpression);
        if (total == 0)
            return new PageModelDto<CfgDto>(search);

        var entities = _cfgRepository
                                     .Where(whereExpression)
                                     .OrderByDescending(x => x.EditTime)
                                     .Skip(search.SkipRows())
                                     .Take(search.PageSize)
                                     .ToList();
        var cfgDtos = Mapper.Map<List<CfgDto>>(entities);

        return new PageModelDto<CfgDto>(search, cfgDtos, total);
    }
}