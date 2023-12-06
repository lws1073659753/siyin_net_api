using SiyinPractice.Domain.Shared.AccessControl;
using SiyinPractice.Framework.Security;
using SiyinPractice.Interface.AccessControl;
using SiyinPractice.Shared.AccessControl.Dto;
using SiyinPractice.Shared.Core.Dto;
using SiyinPractice.Web.Core.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiyinPractice.Web.Host.Controllers.AccessControl
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("usr/users")]
    [ApiController]
    [Authorize]
    public class UserController : ContelWorksControllerBase
    {
        private readonly IUserAppService _userService;

        public UserController(IUserAppService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input">用户信息</param>
        /// <returns></returns>
        [HttpPost]
        [ContelWorksAuthorize(PermissionConsts.User.Create)]
        public async Task<Guid> CreateAsync([FromBody] CreateUserDto input)
            => await _userService.CreateAsync(input);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="input">用户信息</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ContelWorksAuthorize(PermissionConsts.User.Update)]
        public async Task<int> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateUserDto input)
            => await _userService.UpdateAsync(id, input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ContelWorksAuthorize(PermissionConsts.User.Delete)]
        public async Task<int> DeleteAsync([FromRoute] Guid id)
            => await _userService.DeleteAsync(id);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id">重置密码</param>
        /// <returns></returns>
        [HttpPut("{id}/resetpwd")]
        [ContelWorksAuthorize(PermissionConsts.User.ResetPwd)]
        public async Task<int> ResetPwd([FromRoute] Guid id)
            => await _userService.ResetPwd(id);

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <param name="roleIds">角色</param>
        /// <returns></returns>
        [HttpPut("{id}/roles")]
        [ContelWorksAuthorize(PermissionConsts.User.SetRole)]
        public async Task<int> SetRoleAsync([FromRoute] Guid id, [FromBody] Guid[] roleIds)
            => await _userService.SetRoleAsync(id, new UserSetRoleDto { RoleIds = roleIds });

        /// <summary>
        /// 变更用户状态
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        [HttpPut("{id}/status")]
        [ContelWorksAuthorize(PermissionConsts.User.ChangeStatus)]
        public async Task<int> ChangeStatus([FromRoute] Guid id, [FromBody] SimpleDto<int> status)
            => await _userService.ChangeStatusAsync(id, status.Value);

        /// <summary>
        /// 批量变更用户状态
        /// </summary>
        /// <param name="input">用户Ids与状态</param>
        /// <returns></returns>
        [HttpPut("batch/status")]
        [ContelWorksAuthorize(PermissionConsts.User.ChangeStatus)]
        public async Task<int> ChangeStatus([FromBody] ChangeUserStatusDto input)
            => await _userService.ChangeStatusAsync(input.UserIds, input.Status);

        /// <summary>
        /// 获取当前用户是否拥有指定权限
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="requestPermissions"></param>
        /// <param name="userBeGuidsRoleIds"></param>
        /// <returns></returns>
        [HttpGet("{id}/permissions")]
        public async Task<List<string>> GetCurrenUserPermissions([FromRoute] Guid id, [FromQuery] IEnumerable<string> requestPermissions, [FromQuery] string userBeGuidsRoleIds)
        {
            //if (id != _userContext.Id)
            //{
            //    _logger.LogDebug($"id={id},usercontextid={_userContext.Id}");
            //    return Forbid();
            //}
            var result = await _userService.GetPermissionsAsync(id, requestPermissions, userBeGuidsRoleIds);
            return result ?? new List<string>();
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpGet()]
        [ContelWorksAuthorize(PermissionConsts.User.GetList)]
        public async Task<PageModelDto<UserDto>> GetPagedAsync([FromQuery] UserSearchPagedDto search)
            => await _userService.GetPagedAsync(search);

        /// <summary>
        /// 获取登录用户个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("current")]
        public async Task<UserInfoDto> GetCurrentUserInfoAsync() => await _userService.GetUserInfoAsync(UserTokenService.GetUserToken().UserId);

        /// <summary>
        /// 获取登录用户个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public List<UserDto> GetAllAsync() => _userService.GetAllAsync();
    }
}