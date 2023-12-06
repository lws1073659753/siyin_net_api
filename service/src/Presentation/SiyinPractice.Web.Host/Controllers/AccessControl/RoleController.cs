using SiyinPractice.Domain.Shared.AccessControl;
using SiyinPractice.Interface.AccessControl;
using SiyinPractice.Shared.AccessControl.Dto;
using SiyinPractice.Shared.Core.Dto;
using SiyinPractice.Web.Core.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace SiyinPractice.Web.Host.Controllers.AccessControl
{
    /// <summary>
    /// 角色
    /// </summary>
    [Route("usr/roles")]
    [ApiController]
    [Authorize]
    public class RoleController : ContelWorksControllerBase
    {
        private readonly IRoleAppService _roleService;

        public RoleController(IRoleAppService roleService)
           => _roleService = roleService;

        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="input">角色查询条件</param>
        /// <returns></returns>
        [HttpGet()]
        [ContelWorksAuthorize(PermissionConsts.Role.GetList)]
        public async Task<PageModelDto<RoleDto>> GetPagedAsync([FromQuery] RolePagedSearchDto input)
            => await _roleService.GetPagedAsync(input);

        /// <summary>
        /// 根据用户ID获取角色树
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [HttpGet("{userId}/rolestree")]
        public async Task<RoleTreeDto> GetRoleTreeListByUserIdAsync([FromRoute] Guid userId)
            => await _roleService.GetRoleTreeListByUserIdAsync(userId);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ContelWorksAuthorize(PermissionConsts.Role.Delete)]
        public async Task<int> DeleteAsync([FromRoute] Guid id)
            => await _roleService.DeleteAsync(id);

        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <param name="permissions">用户权限Ids</param>
        /// <returns></returns>
        [HttpPut("{id}/permissons")]
        [ContelWorksAuthorize(PermissionConsts.Role.SetPermissons)]
        public async Task<int> SetPermissonsAsync([FromRoute] Guid id, [FromBody] Guid[] permissions)
            => await _roleService.SetPermissonsAsync(new RoleSetPermissonsDto() { RoleId = id, Permissions = permissions });

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="input">角色</param>
        /// <returns></returns>
        [HttpPost]
        [ContelWorksAuthorize(PermissionConsts.Role.Create)]
        public async Task<Guid> CreateAsync([FromBody] CreateRoleDto input)
            => await _roleService.CreateAsync(input);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="input">角色</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ContelWorksAuthorize(PermissionConsts.Role.Update)]
        public async Task<int> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRoleDto input)
            => await _roleService.UpdateAsync(id, input);
    }
}