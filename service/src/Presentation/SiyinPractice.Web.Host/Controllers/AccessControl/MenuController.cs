using SiyinPractice.Domain.Shared.AccessControl;
using SiyinPractice.Framework.Security;
using SiyinPractice.Interface.AccessControl;
using SiyinPractice.Shared.AccessControl.Dto;
using SiyinPractice.Web.Core.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiyinPractice.Web.Host.Controllers.AccessControl
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    [Route("usr/menus")]
    [ApiController]
    [Authorize]
    public class MenuController : ContelWorksControllerBase
    {
        private readonly IMenuAppService _menuService;
        private readonly IAccountAppService _accountService;

        public MenuController(IMenuAppService menuService
                              , IAccountAppService accountService)
        {
            _menuService = menuService;
            _accountService = accountService;
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menuDto">菜单</param>
        /// <returns></returns>
        [HttpPost]
        [ContelWorksAuthorize(PermissionConsts.Menu.Create)]
        public async Task<Guid> CreateAsync([FromBody] CreateMenuDto menuDto)
            => await _menuService.CreateAsync(menuDto);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="input">菜单</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ContelWorksAuthorize(PermissionConsts.Menu.Update)]
        public async Task<int> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateMenuDto input)
            => await _menuService.UpdateAsync(id, input);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id">菜单ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ContelWorksAuthorize(PermissionConsts.Menu.Delete)]
        public async Task<int> DeleteAsync([FromRoute] Guid id)
            => await _menuService.DeleteAsync(id);

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ContelWorksAuthorize(PermissionConsts.Menu.GetList)]
        public async Task<List<MenuNodeDto>> GetlistAsync()
            => await _menuService.GetlistAsync();

        /// <summary>
        /// 获取侧边栏路由菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("routers")]
        public async Task<List<MenuRouterDto>> GetMenusForRouterAsync()
        {
            var roleIds = UserTokenService.GetUserToken().RoeleIds.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            return await _menuService.GetMenusForRouterAsync(roleIds.Select(x => Guid.Parse(x)));
        }

        /// <summary>
        /// 根据角色获取菜单树
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        [HttpGet("{roleId}/menutree")]
        public async Task<MenuTreeDto> GetMenuTreeListByRoleIdAsync([FromRoute] Guid roleId)
            => await _menuService.GetMenuTreeListByRoleIdAsync(roleId);
    }
}