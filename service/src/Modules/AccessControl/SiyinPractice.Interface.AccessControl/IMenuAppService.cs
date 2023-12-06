using SiyinPractice.Caching.Interceptor;
using SiyinPractice.Domain.Shared.AccessControl;
using SiyinPractice.Interface.Core;
using SiyinPractice.Interface.Core.Attributes;
using SiyinPractice.Shared.AccessControl.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiyinPractice.Interface.AccessControl
{
    /// <summary>
    /// 菜单/权限服务
    /// </summary>
    public interface IMenuAppService : IAppService
    {
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperateLog(LogName = "新增菜单")]
        [CachingEvict(CacheKeys = new[] { CachingConsts.MenuListCacheKey, CachingConsts.MenuTreeListCacheKey, CachingConsts.MenuRelationCacheKey, CachingConsts.MenuCodesCacheKey })]
        Task<Guid> CreateAsync(CreateMenuDto input);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperateLog(LogName = "修改菜单")]
        [CachingEvict(CacheKeys = new[] { CachingConsts.MenuListCacheKey, CachingConsts.MenuTreeListCacheKey, CachingConsts.MenuRelationCacheKey, CachingConsts.MenuCodesCacheKey })]
        Task<int> UpdateAsync(Guid id, UpdateMenuDto input);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperateLog(LogName = "删除菜单")]
        [CachingEvict(CacheKeys = new[] { CachingConsts.MenuListCacheKey, CachingConsts.MenuTreeListCacheKey, CachingConsts.MenuRelationCacheKey, CachingConsts.MenuCodesCacheKey })]
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        [CachingAble(CacheKey = CachingConsts.MenuTreeListCacheKey, Expiration = CachingConsts.OneYear)]
        Task<List<MenuNodeDto>> GetlistAsync();

        /// <summary>
        /// 获取左侧路由菜单
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<List<MenuRouterDto>> GetMenusForRouterAsync(IEnumerable<Guid> roleIds);

        /// <summary>
        /// 获取指定角色的菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<MenuTreeDto> GetMenuTreeListByRoleIdAsync(Guid roleId);
    }
}