using SiyinPractice.Caching.Interceptor;
using SiyinPractice.Domain.Shared.Maintenance;
using SiyinPractice.Interface.Core;
using SiyinPractice.Interface.Core.Attributes;
using SiyinPractice.Shared.Core.Dto;
using SiyinPractice.Shared.Maintenance.Dto;

namespace SiyinPractice.Interface.Maintenance;

/// <summary>
/// 配置管理
/// </summary>
public interface IConfigAppService : IAppService
{
    /// <summary>
    /// 新增配置
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [OperateLog(LogName = "新增配置")]
    Task<Guid> CreateAsync(CfgCreationDto input);

    /// <summary>
    /// 修改配置
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [OperateLog(LogName = "修改配置")]
    [CachingEvict(CacheKeyPrefix = CachingConsts.CfgSingleKeyPrefix)]
    Task<int> UpdateAsync([CachingParam] Guid id, CfgUpdationDto input);

    /// <summary>
    /// 删除配置
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [OperateLog(LogName = "删除配置")]
    [CachingEvict(CacheKeyPrefix = CachingConsts.CfgSingleKeyPrefix)]
    Task<int> DeleteAsync([CachingParam] Guid id);

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [CachingAble(CacheKeyPrefix = CachingConsts.CfgSingleKeyPrefix, Expiration = CachingConsts.OneMonth)]
    Task<CfgDto> GetAsync([CachingParam] Guid id);

    /// <summary>
    /// 配置列表
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    Task<PageModelDto<CfgDto>> GetPagedAsync(CfgSearchPagedDto search);
}