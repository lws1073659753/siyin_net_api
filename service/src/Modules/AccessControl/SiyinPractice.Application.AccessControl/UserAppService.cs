using SiyinPractice.Application.Core;
using SiyinPractice.Domain.AccessControl;
using SiyinPractice.Domain.Core;
using SiyinPractice.Framework.Security;
using SiyinPractice.Interface.AccessControl;
using SiyinPractice.Shared.AccessControl.Dto;
using SiyinPractice.Shared.Core.Dto;
using SiyinPractice.Shared.Core.Exceptions;
using SiyinPractice.Shared.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SiyinPractice.Application.AccessControl;

public class UserAppService : AppService, IUserAppService
{
    private readonly IEfRepository<SysUser> _userRepository;
    private readonly IEfRepository<SysRole> _roleRepository;
    private readonly IEfRepository<SysMenu> _menuRepository;
    private readonly IEfRepository<SysRelation> _relationRepository;
    private readonly IEfRepository<SysDept> _deptRepository;

    public UserAppService(
        IEfRepository<SysUser> userRepository
        , IEfRepository<SysRole> roleRepository
        , IEfRepository<SysMenu> menuRepository
        , IEfRepository<SysRelation> relationRepository
        , IEfRepository<SysDept> deptRepository
        )
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _menuRepository = menuRepository;
        _relationRepository = relationRepository;
        _deptRepository = deptRepository;
    }

    public async Task<Guid> CreateAsync(CreateUserDto input)
    {
        var isExists = await _userRepository.AnyAsync(x => x.Account == input.Account);
        Validate.Assert(isExists, "账号已经存在");
        var user = Mapper.Map<SysUser>(input);
        user.Account = user.Account.ToLower();
        user.Salt = Random.Shared.Next(5, false);
        user.Password = EncryptionService.MD5(user.Password + user.Salt);
        if (user is AuditEntity auditEntity)
        {
            auditEntity.Creator = UserTokenService.GetUserToken().UserName;
            auditEntity.CreateTime = DateTime.Now;
        }
        await _userRepository.InsertAsync(user);

        return user.Id;
    }

    public async Task<int> UpdateAsync(Guid id, UpdateUserDto input)
    {
        var user = Mapper.Map<SysUser>(input);
        if (user is AuditEntity auditEntity)
        {
            auditEntity.Editor = UserTokenService.GetUserToken().UserName;
            auditEntity.EditTime = DateTime.Now;
        }
        user.Id = id;
        var updatingProps = UpdatingProps<SysUser>(x => x.Name, x => x.DeptId, x => x.Sex, x => x.Phone, x => x.Email, x => x.WeChat, x => x.DingDing, x => x.Birthday, x => x.Status, d => d.Editor, d => d.EditTime);
        return await _userRepository.UpdateAsync(user, updatingProps);
    }

    public async Task<int> SetRoleAsync(Guid id, UserSetRoleDto input)
    {
        var roleIdStr = input.RoleIds.Any() ? string.Join(",", input.RoleIds) : string.Empty;
        return await _userRepository.UpdateAsync(new SysUser() { Id = id, RoleIds = roleIdStr }, UpdatingProps<SysUser>(x => x.RoleIds));
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    public async Task<int> ResetPwd(Guid id)
    {
        var user = await _userRepository.FindAsync(id);
        Validate.Assert(user == null, "需要重置的账号不存在");
        //user.Account = user.Account.ToLower();
        user.Salt = Random.Shared.Next(5, false);
        user.Password = EncryptionService.MD5(123456 + user.Salt);
        if (user is AuditEntity auditEntity)
        {
            auditEntity.Editor = UserTokenService.GetUserToken().UserName;
            auditEntity.EditTime = DateTime.Now;
        }
        return await _userRepository.UpdateAsync(user);
    }

    public List<UserDto> GetAllAsync()
    {
        var domains = _userRepository.GetAll().ToList();
        var dtoTasks = domains.Select(x => new UserDto()
        {
            Id = x.Id,
            RoleIds = x.RoleIds,
            DingDing = x.DingDing,
            WeChat = x.WeChat,
            Phone = x.Phone,
            Email = x.Email,
            Birthday = x.Birthday,
            Status = x.Status,
            Name = x.Name,
            DeptId = x.DeptId,
            Sex = x.Sex,
        }).ToList();
        return dtoTasks;
    }

    public async Task<int> ChangeStatusAsync(Guid id, int status)
    {
        return await _userRepository.UpdateAsync(new SysUser { Id = id, Status = status }, UpdatingProps<SysUser>(x => x.Status));
    }

    public async Task<int> ChangeStatusAsync(IEnumerable<Guid> ids, int status)
    {
        return await _userRepository.UpdateRangeAsync(u => ids.Contains(u.Id), u => new SysUser { Status = status });
    }

    public async Task<List<string>> GetPermissionsAsync(Guid userId, IEnumerable<string> requestPermissions, string userBelongsRoleIds)
    {
        if (userBelongsRoleIds.IsNullOrWhiteSpace())
            return default;

        if (!requestPermissions.Any())
            return new List<string> { "allow" };

        var roleIds = userBelongsRoleIds.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => Guid.Parse(x.Trim()));

        var allMenuCodes = _relationRepository.GetAll().Where(x => x.Menu.Status).Distinct();
        var upperCodes = allMenuCodes?.Where(x => roleIds.Contains(x.RoleId)).Select(x => x.Menu.Code.ToUpper());
        if (!upperCodes.Any())
            return default;

        var result = upperCodes.Intersect(requestPermissions.Select(x => x.ToUpper()));
        return result.ToList();
    }

    public async Task<PageModelDto<UserDto>> GetPagedAsync(UserSearchPagedDto search)
    {
        var whereExpression = ExpressionCreator
                                            .New<SysUser>()
                                            .AndIf(search.Account.IsNotNullOrWhiteSpace(), x => x.Account.Contains($"{search.Account.Trim()}"))
                                            .AndIf(search.Name.IsNotNullOrWhiteSpace(), x => x.Name.Contains($"{search.Name.Trim()}"));

        var total = await _userRepository.CountAsync(whereExpression);
        if (total == 0)
            return new PageModelDto<UserDto>(search);

        var entities = _userRepository
                                        .Where(whereExpression)
                                        .OrderByDescending(x => x.Id)
                                        .Skip(search.SkipRows())
                                        .Take(search.PageSize)
                                        .ToList();

        var userDtos = Mapper.Map<List<UserDto>>(entities);
        if (userDtos.Any())
        {
            var deptIds = userDtos.Where(d => d.DeptId is not null).Select(d => d.DeptId).Distinct();
            var depts = _deptRepository.GetAll().Where(x => deptIds.Contains(x.Id)).Select(d => new { d.Id, d.FullName });
            var roles = _roleRepository.GetAll().Select(r => new { r.Id, r.Name });
            foreach (var user in userDtos)
            {
                user.DeptName = depts.FirstOrDefault(x => x.Id == user.DeptId)?.FullName;

                var roleIds = user.RoleIds.IsNullOrWhiteSpace()
                                        ? new List<Guid>()
                                        : user.RoleIds.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Guid.Parse(x))
                                        ;
                user.RoleNames = String.Join(",", roles.Where(x => roleIds.Contains(x.Id)).Select(x => x.Name));
            }
        }

        var xdata = await GetDeptSimpleTreeListAsync();
        return new PageModelDto<UserDto>(search, userDtos, total, xdata);
    }

    private async Task<List<DepartmentSimpleTreeDto>> GetDeptSimpleTreeListAsync()
    {
        var result = new List<DepartmentSimpleTreeDto>();

        var depts = _deptRepository.GetAll().OrderBy(x => x.Ordinal).ToList();

        if (!depts.Any())
            return result;

        var roots = depts.Where(d => d.Pid == Guid.Empty)
                                    .OrderBy(d => d.Ordinal)
                                    .Select(x => new DepartmentSimpleTreeDto { Id = x.Id, Label = x.SimpleName })
                                    .ToList();
        foreach (var node in roots)
        {
            GetChildren(node, depts);
            result.Add(node);
        }

        void GetChildren(DepartmentSimpleTreeDto currentNode, List<SysDept> depts)
        {
            var childrenNodes = depts.Where(d => d.Pid == currentNode.Id)
                                                       .OrderBy(d => d.Ordinal)
                                                       .Select(x => new DepartmentSimpleTreeDto() { Id = x.Id, Label = x.SimpleName })
                                                       .ToList();
            if (childrenNodes.Any())
            {
                currentNode.Children.AddRange(childrenNodes);
                foreach (var node in childrenNodes)
                {
                    GetChildren(node, depts);
                }
            }
        }

        return result;
    }

    public async Task<UserInfoDto> GetUserInfoAsync(Guid id)
    {
        var userProfile = await _userRepository.FetchAsync(u => new UserProfileDto
        {
            Account = u.Account,
            Avatar = u.Avatar,
            Birthday = u.Birthday,
            DeptId = u.DeptId,
            DeptFullName = u.Dept.FullName,
            Email = u.Email,
            Name = u.Name,
            Phone = u.Phone,
            RoleIds = u.RoleIds,
            Sex = u.Sex,
            Status = u.Status
        }, x => x.Id == id);

        if (userProfile == null)
            return null;

        var userInfoDto = new UserInfoDto { Id = id, Profile = userProfile };

        if (userProfile.RoleIds.IsNotNullOrEmpty())
        {
            var roleIds = userProfile.RoleIds.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Guid.Parse(x));

            var roles = _roleRepository.Where(x => roleIds.Contains(x.Id))
                                       .Select(r => new { r.Id, r.Tips, r.Name })
                                       .ToList();
            foreach (var role in roles)
            {
                userInfoDto.Roles.Add(role.Tips);
                userInfoDto.Profile.Roles.Add(role.Name);
            }

            var roleMenus = await GetMenusByRoleIdsAsync(_menuRepository, roleIds.ToArray(), true);
            if (roleMenus.Any())
                userInfoDto.Permissions.AddRange(roleMenus.Select(x => x.Url).Distinct());
        }

        return userInfoDto;
    }

    private async Task<List<SysMenu>> GetMenusByRoleIdsAsync(IEfRepository<SysMenu> repo, Guid[] roleIds, bool enabledOnly)
    {
        var query = repo.GetAll<SysRelation>().Where(r => roleIds.Contains(r.RoleId))
                                   .Select(u => new { u.Menu });
        if (enabledOnly)
            query = query.Where(r => r.Menu.Status);

        var relations = query.ToList();

        return relations.Select(d => d.Menu).Distinct().ToList();
    }
}