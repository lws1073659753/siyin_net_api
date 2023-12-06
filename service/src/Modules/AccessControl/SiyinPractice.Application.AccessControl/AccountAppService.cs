using SiyinPractice.Application.Core;
using SiyinPractice.Caching.Interceptor;
using SiyinPractice.Domain.AccessControl;
using SiyinPractice.Domain.Core;
using SiyinPractice.Domain.Maintenance;
using SiyinPractice.Framework;
using SiyinPractice.Framework.Security;
using SiyinPractice.Interface.AccessControl;
using SiyinPractice.Shared.AccessControl.Dto;
using SiyinPractice.Shared.Core.Utility;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SiyinPractice.Application.AccessControl;

public class AccountAppService : AppService, IAccountAppService
{
    private readonly IEfRepository<SysUser> _userRepository;

    private readonly IEfRepository<LoginLog> _loginLogRepository;

    public AccountAppService(IEfRepository<SysUser> userRepository, IEfRepository<LoginLog> loginLogRepository)
    {
        _userRepository = userRepository;
        _loginLogRepository = loginLogRepository;
    }

    public async Task<UserValidatedInfoDto> LoginAsync(UserLoginDto input)
    {
        var log = new LoginLog
        {
            Succeed = false,
            CreateTime = DateTime.Now,
        };
        try
        {
            var user = await _userRepository.FetchAsync(x => new
            {
                x.Id,
                x.Account,
                x.Password,
                x.Salt,
                x.Status,
                x.Email,
                x.Name,
                x.RoleIds
            }, x => x.Account == input.Account);

            Validate.Assert(user is null, "用户名或密码错误");

            var httpContext = App.HttpContextAccessor.HttpContext;

            log.Account = input.Account;
            log.UserId = user.Id;
            log.UserName = user.Name;
            log.Device = httpContext.Request.Headers["device"].FirstOrDefault() ?? "web";
            log.RemoteIpAddress = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            Validate.Assert(user.Status != 1, "账号已锁定");

            // TODO
            var failLoginCount = 2;
            if (failLoginCount >= 5)
            {
                await _userRepository.UpdateAsync(new SysUser() { Id = user.Id, Status = 0 }, UpdatingProps<SysUser>(x => x.Status));
                Validate.Assert(true, "连续登录失败次数超过5次，账号已锁定");
            }

            var password = EncryptionService.MD5(input.Password + user.Salt);
            Validate.Assert(password != user.Password, "用户名或密码错误");

            Validate.Assert(user.RoleIds.IsNullOrEmpty(), "未分配任务角色，请联系管理员");

            log.Message = "登录成功";
            log.StatusCode = (int)HttpStatusCode.Created;
            log.Succeed = true;

            var userValidtedInfo = new UserValidatedInfoDto(user.Id, user.Account, user.Name, user.RoleIds, user.Status);
            //await _cacheService.SetValidateInfoToCacheAsync(userValidtedInfo);

            return userValidtedInfo;
        }
        catch (Exception ex)
        {
            log.Message = ex.Message;
            throw;
        }
        finally
        {
            await _loginLogRepository.InsertAsync(log);
        }
    }

    public async Task ChangeUserValidateInfoExpiresDtAsync(Guid id)
    {
        //await _cacheService.ChangeUserValidateInfoCacheExpiresDtAsync(id);
        await Task.CompletedTask;
    }

    public async Task UpdatePasswordAsync(Guid id, ChangeUserPwdDto input)
    {
        var user = await _userRepository.FetchAsync(x => new
        {
            x.Id,
            x.Salt,
            x.Password,
        }, x => x.Id == id);

        Validate.Assert(user is null, "用户不存在,参数信息不完整");

        var md5OldPwdString = EncryptionService.MD5(input.OldPassword + user.Salt);
        Validate.Assert(!md5OldPwdString.EqualsIgnoreCase(user.Password), "旧密码输入错误");

        var newPwdString = EncryptionService.MD5(input.Password + user.Salt);

        await _userRepository.UpdateAsync(new SysUser { Id = user.Id, Password = newPwdString }, UpdatingProps<SysUser>(x => x.Password));
    }

    public Task<UserValidatedInfoDto> GetUserValidatedInfoAsync([CachingParam] Guid id)
    {
        //return Task.FromResult(new UserValidatedInfoDto());
        throw new NotImplementedException();
    }

    public Task DeleteUserValidateInfoAsync([CachingParam] Guid id)
    {
        return Task.CompletedTask;
    }
}