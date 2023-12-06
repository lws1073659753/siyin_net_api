using SiyinPractice.Application.Core;
using SiyinPractice.Domain.Core;
using SiyinPractice.Domain.Maintenance;
using SiyinPractice.Framework.Security;
using SiyinPractice.Interface.Maintenance;
using SiyinPractice.Shared.Core.Utility;
using SiyinPractice.Shared.Maintenance.Dto;
using System.Linq.Expressions;

namespace Adnc.Maint.Application.Services;

public class DictionaryAppService : AppService, IDictionaryAppService
{
    private readonly IEfRepository<SysDict> _dictRepository;

    public DictionaryAppService(IEfRepository<SysDict> dictRepository)
    {
        _dictRepository = dictRepository;
    }

    public async Task<Guid> CreateAsync(DictCreationDto input)
    {
        var exists = await _dictRepository.AnyAsync(x => x.Name.Equals(input.Name.Trim()));
        Validate.Assert(exists, "字典名字已经存在");

        var dists = new List<SysDict>();
        var id = Guid.NewGuid();
        var dict = new SysDict
        {
            Id = id,
            Name = input.Name,
            Value = input.Value,
            Ordinal = input.Ordinal,
            Pid = Guid.Empty,
            Description = input.Description,
            Creator = UserTokenService.GetUserToken().UserName,
            CreateTime = DateTime.Now
        };

        dists.Add(dict);
        input.Children?.ForEach(x =>
        {
            dists.Add(new SysDict
            {
                Id = Guid.NewGuid(),
                Pid = id,
                Name = x.Name,
                Value = x.Value,
                Ordinal = x.Ordinal
            });
        });

        await _dictRepository.InsertRangeAsync(dists);
        return id;
    }

    public async Task<int> UpdateAsync(Guid id, DictUpdationDto input)
    {
        var exists = await _dictRepository.AnyAsync(x => x.Name.Equals(input.Name.Trim()) && x.Id != id);
        Validate.Assert(exists, "字典名字已经存在");

        var dict = new SysDict
        {
            Name = input.Name,
            Value = input.Value,
            Id = id,
            Pid = Guid.Empty,
            Ordinal = input.Ordinal,
            Description = input.Description,
            Editor = UserTokenService.GetUserToken().UserName,
            EditTime = DateTime.Now
        };

        var subDicts = new List<SysDict>();
        input.Children?.ForEach(x =>
        {
            subDicts.Add(new SysDict
            {
                Id = Guid.NewGuid(),
                Pid = id,
                Name = x.Name,
                Value = x.Value,
                Ordinal = x.Ordinal
            });
        });

        var result = await _dictRepository.UpdateAsync(dict, UpdatingProps<SysDict>(d => d.Name, d => d.Value, d => d.Description, d => d.Ordinal, d => d.Editor, d => d.EditTime));
        await _dictRepository.DeleteRangeAsync(d => d.Pid == dict.Id);
        if (subDicts.IsNotNullOrEmpty())
            await _dictRepository.InsertRangeAsync(subDicts);

        return result;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        return await _dictRepository.DeleteRangeAsync(d => (d.Id == id) || (d.Pid == id));
    }

    public async Task<DictDto> GetAsync(Guid id)
    {
        var dictEntity = await _dictRepository.FindAsync(id);
        if (dictEntity is null)
            return default;

        var dictDto = Mapper.Map<DictDto>(dictEntity);
        var subDictEnties = _dictRepository.Where(x => x.Pid == id).ToList();
        if (subDictEnties is not null)
        {
            var subDictDtos = Mapper.Map<List<DictDto>>(subDictEnties);
            dictDto.Children = subDictDtos;
        }
        return dictDto;
    }

    public async Task<List<DictDto>> GetListAsync(DictSearchDto search)
    {
        var result = new List<DictDto>();

        var whereCondition = ExpressionCreator
            .New<SysDict>(x => x.Pid == Guid.Empty)
            .AndIf(search.Name.IsNotNullOrWhiteSpace(), x => x.Name.Contains($"{search.Name.Trim()}"));

        var dictEntities = _dictRepository
            .Where(whereCondition)
            .OrderBy(d => d.Ordinal)
            .ToList();

        if (dictEntities is null)
            return new List<DictDto>();

        var subPids = dictEntities.Select(x => x.Id);
        var allSubDictEntities = _dictRepository.Where(x => subPids.Contains(x.Pid)).ToList();

        var dictDtos = Mapper.Map<List<DictDto>>(dictEntities);
        var allSubDictDtos = Mapper.Map<List<DictDto>>(allSubDictEntities);
        foreach (var dto in dictDtos)
        {
            var subDtos = allSubDictDtos?.Where(x => x.Pid == dto.Id).ToList();
            if (subDtos.IsNotNullOrEmpty())
            {
                dto.Children = subDtos;
            }
        }

        return dictDtos;
    }
}