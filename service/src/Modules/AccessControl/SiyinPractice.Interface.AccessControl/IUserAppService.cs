using SiyinPractice.Caching.Interceptor;
using SiyinPractice.Domain.Shared.AccessControl;
using SiyinPractice.Interface.Core;
using SiyinPractice.Interface.Core.Attributes;
using SiyinPractice.Shared.AccessControl.Dto;
using SiyinPractice.Shared.Core.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiyinPractice.Interface.AccessControl
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public interface IUserAppService : IAppService
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperateLog(LogName = "新增用户")]
        Task<Guid> CreateAsync(CreateUserDto input);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperateLog(LogName = "修改用户")]
        [CachingEvict(CacheKeyPrefix = CachingConsts.UserValidatedInfoKeyPrefix)]
        Task<int> UpdateAsync([CachingParam] Guid id, UpdateUserDto input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperateLog(LogName = "删除用户")]
        [CachingEvict(CacheKeyPrefix = CachingConsts.UserValidatedInfoKeyPrefix)]
        Task<int> DeleteAsync([CachingParam] Guid id);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperateLog(LogName = "重置密码")]
        [CachingEvict(CacheKeyPrefix = CachingConsts.UserValidatedInfoKeyPrefix)]
        Task<int> ResetPwd([CachingParam] Guid id);

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperateLog(LogName = "设置用户角色")]
        [CachingEvict(CacheKeys = new[] { CachingConsts.MenuRelationCacheKey, CachingConsts.MenuCodesCacheKey }
                             , CacheKeyPrefix = CachingConsts.UserValidatedInfoKeyPrefix)]
        Task<int> SetRoleAsync([CachingParam] Guid id, UserSetRoleDto input);

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [OperateLog(LogName = "修改用户状态")]
        [CachingEvict(CacheKeyPrefix = CachingConsts.UserValidatedInfoKeyPrefix)]
        Task<int> ChangeStatusAsync([CachingParam] Guid id, int status);

        /// <summary>
        /// 批量修改用户状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [OperateLog(LogName = "批量修改用户状态")]
        [CachingEvict(CacheKeyPrefix = CachingConsts.UserValidatedInfoKeyPrefix)]
        Task<int> ChangeStatusAsync([CachingParam] IEnumerable<Guid> ids, int status);

        /// <summary>
        /// 获取用户是否拥有指定权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="requestPermissions"></param>
        /// <param name="userBelongsRoleIds"></param>
        /// <returns></returns>
        //[OperateLog(LogName = "获取当前用户是否拥有指定权限")]
        Task<List<string>> GetPermissionsAsync(Guid userId, IEnumerable<string> requestPermissions, string userBelongsRoleIds);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<PageModelDto<UserDto>> GetPagedAsync(UserSearchPagedDto search);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserInfoDto> GetUserInfoAsync(Guid id);

        List<UserDto> GetAllAsync();
    }
}