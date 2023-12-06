using SiyinPractice.Caching.Interceptor;
using SiyinPractice.Domain.Shared.Maintenance;
using SiyinPractice.Interface.Core;
using SiyinPractice.Interface.Core.Attributes;
using SiyinPractice.Shared.Maintenance.Dto;

namespace SiyinPractice.Interface.Maintenance;

/// <summary>
/// 字典管理
/// </summary>
public interface IDictionaryAppService : IAppService
{
    /// <summary>
    /// 新增字典
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [OperateLog(LogName = "新增字典")]
    Task<Guid> CreateAsync(DictCreationDto input);

    /// <summary>
    /// 修改字典
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [OperateLog(LogName = "修改字典")]
    [CachingEvict(CacheKeyPrefix = CachingConsts.DictSingleKeyPrefix)]
    Task<int> UpdateAsync([CachingParam] Guid id, DictUpdationDto input);

    /// <summary>
    /// 删除字典
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [OperateLog(LogName = "删除字典")]
    [CachingEvict(CacheKeyPrefix = CachingConsts.DictSingleKeyPrefix)]
    Task<int> DeleteAsync([CachingParam] Guid id);

    /// <summary>
    /// 字典列表
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    Task<List<DictDto>> GetListAsync(DictSearchDto search);

    /// <summary>
    /// 获取单个字典
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [CachingAble(CacheKeyPrefix = CachingConsts.DictSingleKeyPrefix, Expiration = CachingConsts.OneMonth)]
    Task<DictDto> GetAsync([CachingParam] Guid id);
}