using SiyinPractice.Caching.Interceptor;
using SiyinPractice.Domain.Shared.AccessControl;
using SiyinPractice.Interface.Core;
using SiyinPractice.Interface.Core.Attributes;
using SiyinPractice.Shared.AccessControl.Dto;
using System;
using System.Threading.Tasks;

namespace SiyinPractice.Interface.AccessControl
{
    /// <summary>
    /// 认证服务
    /// </summary>
    public interface IAccountAppService : IAppService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperateLog(LogName = "登录")]
        Task<UserValidatedInfoDto> LoginAsync(UserLoginDto input);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperateLog(LogName = "修改密码")]
        [CachingEvict(CacheKeyPrefix = CachingConsts.UserValidatedInfoKeyPrefix)]
        Task UpdatePasswordAsync([CachingParam] Guid id, ChangeUserPwdDto input);

        /// <summary>
        /// 获取认证信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[OperateLog(LogName = "获取认证信息")]
        [CachingAble(CacheKeyPrefix = CachingConsts.UserValidatedInfoKeyPrefix)]
        Task<UserValidatedInfoDto> GetUserValidatedInfoAsync([CachingParam] Guid id);

        /// <summary>
        /// 移除认证信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperateLog(LogName = "移除认证信息")]
        [CachingEvict(CacheKeyPrefix = CachingConsts.UserValidatedInfoKeyPrefix)]
        Task DeleteUserValidateInfoAsync([CachingParam] Guid id);

        /// <summary>
        /// 调整认证信息过期是时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperateLog(LogName = "调整认证信息过期时间")]
        Task ChangeUserValidateInfoExpiresDtAsync(Guid id);
    }
}