using SiyinPractice.Application.Core;
using SiyinPractice.Domain.AccessControl;
using SiyinPractice.Domain.Core;
using SiyinPractice.Framework.Security;
using SiyinPractice.Interface.AccessControl;
using SiyinPractice.Shared.AccessControl.Dto;
using SiyinPractice.Shared.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiyinPractice.Application.AccessControl;

public class MenuAppService : AppService, IMenuAppService
{
    private readonly IEfRepository<SysMenu> _menuRepository;
    private readonly IEfRepository<SysRelation> _relationRepository;

    public MenuAppService(IEfRepository<SysMenu> menuRepository
                          , IEfRepository<SysRelation> relationRepository
                         )
    {
        _menuRepository = menuRepository;
        _relationRepository = relationRepository;
    }

    public async Task<Guid> CreateAsync(CreateMenuDto input)
    {
        var isExistsCode = await _menuRepository.AnyAsync(x => x.Code == input.Code);
        Validate.Assert(isExistsCode, "该菜单编码已经存在");

        var isExistsName = await _menuRepository.AnyAsync(x => x.Name == input.Name);
        Validate.Assert(isExistsName, "该菜单名称已经存在");

        var parentMenu = await _menuRepository.FindAsync(x => x.Code == input.PCode);
        var addDto = ProducePCodes(input, parentMenu);
        var menu = Mapper.Map<SysMenu>(addDto);
        if (menu is AuditEntity auditEntity)
        {
            auditEntity.Creator = UserTokenService.GetUserToken().UserName;
            auditEntity.CreateTime = DateTime.Now;
        }
        await _menuRepository.InsertAsync(menu);

        return menu.Id;
    }

    public async Task<int> UpdateAsync(Guid id, UpdateMenuDto input)
    {
        var isExistsCode = await _menuRepository.AnyAsync(x => x.Code == input.Code && x.Id != id);
        Validate.Assert(isExistsCode, "该菜单编码已经存在");

        var isExistsName = await _menuRepository.AnyAsync(x => x.Name == input.Name && x.Id != id);
        Validate.Assert(isExistsName, "该菜单名称已经存在");

        var parentMenu = await _menuRepository.FindAsync(x => x.Code == input.PCode);
        var updateDto = ProducePCodes(input, parentMenu);
        var menu = Mapper.Map<SysMenu>(updateDto);
        if (menu is AuditEntity auditEntity)
        {
            auditEntity.Editor = UserTokenService.GetUserToken().UserName;
            auditEntity.EditTime = DateTime.Now;
        }
        menu.Id = id;

        var updatingProps = UpdatingProps<SysMenu>(x => x.Code
                                                        , x => x.Component
                                                        , x => x.Hidden
                                                        , x => x.Icon
                                                        , x => x.IsMenu
                                                        , x => x.IsOpen
                                                        , x => x.Levels
                                                        , x => x.Name
                                                        , x => x.Ordinal
                                                        , x => x.PCode
                                                        , x => x.PCodes
                                                        , x => x.Status
                                                        , x => x.Tips
                                                        , x => x.Url
                                                        , d => d.Editor, d => d.EditTime);

        return await _menuRepository.UpdateAsync(menu, updatingProps);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var menu = await _menuRepository.FindAsync(id);
        return await _menuRepository.DeleteRangeAsync(x => x.PCodes.Contains($"[{menu.Code}]") || x.Id == id);
    }

    public async Task<List<MenuNodeDto>> GetlistAsync()
    {
        var result = new List<MenuNodeDto>();
        var menus = _menuRepository.GetAll().OrderBy(o => o.Levels).ThenBy(x => x.Ordinal);
        var menuNodes = Mapper.Map<List<MenuNodeDto>>(menus);
        foreach (var node in menuNodes)
        {
            var parentNode = menuNodes.FirstOrDefault(x => x.Code == node.PCode);
            if (parentNode != null)
                node.ParentId = parentNode.Id;
        }

        var dictNodes = menuNodes.ToDictionary(x => x.Id);
        foreach (var pair in dictNodes)
        {
            var currentNode = pair.Value;
            if (currentNode.ParentId.HasValue && dictNodes.ContainsKey(currentNode.ParentId.Value))
                dictNodes[currentNode.ParentId.Value].Children.Add(currentNode);
            else
                result.Add(currentNode);
        }

        return result;
    }

    public async Task<List<MenuRouterDto>> GetMenusForRouterAsync(IEnumerable<Guid> roleIds)
    {
        var result = new List<MenuRouterDto>();
        //所有菜单
        var allMenus = _menuRepository.GetAll();
        //所有菜单角色关系
        var allRelations = _relationRepository.GetAll();
        //角色拥有的菜单Ids
        var menusIds = allRelations.Where(x => roleIds.Contains(x.RoleId)).Select(x => x.MenuId).Distinct();
        //更加菜单Id获取菜单实体
        var menus = allMenus.Where(x => menusIds.Contains(x.Id));

        if (menus.Any())
        {
            var routerMenus = new List<MenuRouterDto>();

            var componentMenus = menus.Where(x => !string.IsNullOrWhiteSpace(x.Component));
            foreach (var menu in componentMenus)
            {
                var routerMenu = Mapper.Map<MenuRouterDto>(menu);
                routerMenu.Path = menu.Url;
                routerMenu.Meta = new MenuMetaDto
                {
                    Icon = menu.Icon,
                    Title = menu.Code
                };
                routerMenus.Add(routerMenu);
            }

            foreach (var node in routerMenus)
            {
                var parentNode = routerMenus.FirstOrDefault(x => x.Code == node.PCode);
                if (parentNode != null)
                    node.ParentId = parentNode.Id;
            }

            var dictNodes = routerMenus.ToDictionary(x => x.Id);
            foreach (var pair in dictNodes.OrderBy(x => x.Value.Ordinal))
            {
                var currentNode = pair.Value;
                if (currentNode.ParentId.HasValue && dictNodes.ContainsKey(currentNode.ParentId.Value))
                    dictNodes[currentNode.ParentId.Value].Children.Add(currentNode);
                else
                    result.Add(currentNode);
            }
        }

        return result;
    }

    public async Task<MenuTreeDto> GetMenuTreeListByRoleIdAsync(Guid roleId)
    {
        var menuIds = _relationRepository.GetAll().Where(x => x.RoleId == roleId).Select(r => r.MenuId).ToList() ?? new List<Guid>();
        var roleTreeList = new List<ZTreeNodeDto<Guid, dynamic>>();

        var menus = _menuRepository.GetAll().OrderBy(x => x.Ordinal).ToList();

        foreach (var menu in menus)
        {
            var parentMenu = menus.FirstOrDefault(x => x.Code == menu.PCode);
            var node = new ZTreeNodeDto<Guid, dynamic>
            {
                Id = menu.Id,
                PID = parentMenu != null ? parentMenu.Id : Guid.Empty,
                Name = menu.Name,
                Open = parentMenu != null,
                Checked = menuIds.Contains(menu.Id)
            };
            roleTreeList.Add(node);
        }

        var nodes = Mapper.Map<List<Node<Guid>>>(roleTreeList);
        foreach (var node in nodes)
        {
            foreach (var child in nodes)
            {
                if (child.PID == node.Id)
                    node.Children.Add(child);
            }
        }

        var groups = roleTreeList.GroupBy(x => x.PID).Where(x => x.Key != Guid.Empty);
        foreach (var group in groups)
        {
            roleTreeList.RemoveAll(x => x.Id == group.Key);
        }

        return new MenuTreeDto
        {
            TreeData = nodes.Where(x => x.PID == Guid.Empty),
            CheckedIds = roleTreeList.Where(x => x.Checked && x.PID != Guid.Empty).Select(x => x.Id)
        };
    }

    private CreateMenuDto ProducePCodes(CreateMenuDto saveDto, MenuDto parentMenuDto)
    {
        if (saveDto.PCode.IsNullOrWhiteSpace() || saveDto.PCode.EqualsIgnoreCase("0"))
        {
            saveDto.PCode = "0";
            saveDto.PCodes = "[0],";
            saveDto.Levels = 1;
            return saveDto;
        }

        saveDto.Levels = parentMenuDto.Levels + 1;
        saveDto.PCodes = $"{parentMenuDto.PCodes}[{parentMenuDto.Code}]";

        return saveDto;
    }

    private CreateMenuDto ProducePCodes(CreateMenuDto saveDto, SysMenu parentMenu)
    {
        if (saveDto.PCode.IsNullOrWhiteSpace() || saveDto.PCode.EqualsIgnoreCase("0"))
        {
            saveDto.PCode = "0";
            saveDto.PCodes = "[0],";
            saveDto.Levels = 1;
            return saveDto;
        }

        saveDto.Levels = parentMenu.Levels + 1;
        saveDto.PCodes = $"{parentMenu.PCodes}[{parentMenu.Code}]";

        return saveDto;
    }
}