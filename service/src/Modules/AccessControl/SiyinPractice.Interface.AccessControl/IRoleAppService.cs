using SiyinPractice.Caching.Interceptor;
using SiyinPractice.Domain.Shared.AccessControl;
using SiyinPractice.Interface.Core;
using SiyinPractice.Interface.Core.Attributes;
using SiyinPractice.Shared.AccessControl.Dto;
using SiyinPractice.Shared.Core.Dto;
using System;
using System.Threading.Tasks;

namespace SiyinPractice.Interface.AccessControl
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public interface IRoleAppService : IAppService
    {
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperateLog(LogName = "新增角色")]
        [CachingEvict(CacheKey = CachingConsts.RoleListCacheKey)]
        Task<Guid> CreateAsync(CreateRoleDto input);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperateLog(LogName = "修改角色")]
        [CachingEvict(CacheKey = CachingConsts.RoleListCacheKey)]
        Task<int> UpdateAsync(Guid id, UpdateRoleDto input);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperateLog(LogName = "删除角色")]
        [CachingEvict(CacheKeys = new[] { CachingConsts.MenuRelationCacheKey, CachingConsts.MenuCodesCacheKey, CachingConsts.RoleListCacheKey })]
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperateLog(LogName = "设置角色权限")]
        [CachingEvict(CacheKeys = new[] { CachingConsts.MenuRelationCacheKey, CachingConsts.MenuCodesCacheKey })]
        Task<int> SetPermissonsAsync(RoleSetPermissonsDto input);

        /// <summary>
        /// 获取用户拥有的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<RoleTreeDto> GetRoleTreeListByUserIdAsync(Guid userId);

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageModelDto<RoleDto>> GetPagedAsync(RolePagedSearchDto input);
    }
}