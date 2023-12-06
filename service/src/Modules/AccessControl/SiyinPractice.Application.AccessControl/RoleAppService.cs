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

public class RoleAppService : AppService, IRoleAppService
{
    private readonly IEfRepository<SysRole> _roleRepository;
    private readonly IEfRepository<SysUser> _userRepository;
    private readonly IEfRepository<SysRelation> _relationRepository;

    public RoleAppService(IEfRepository<SysRole> roleRepository,
        IEfRepository<SysUser> userRepository,
        IEfRepository<SysRelation> relationRepository
        )
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _relationRepository = relationRepository;
    }

    public async Task<Guid> CreateAsync(CreateRoleDto input)
    {
        var isExists = await _roleRepository.AnyAsync(x => x.Name == input.Name);
        Validate.Assert(isExists, "该角色名称已经存在");

        var role = Mapper.Map<SysRole>(input);
        if (role is AuditEntity auditEntity)
        {
            auditEntity.Creator = UserTokenService.GetUserToken().UserName;
            auditEntity.CreateTime = DateTime.Now;
        }
        await _roleRepository.InsertAsync(role);

        return role.Id;
    }

    public async Task<int> UpdateAsync(Guid id, UpdateRoleDto input)
    {
        var isExists = await _roleRepository.AnyAsync(x => x.Name == input.Name && x.Id != id);
        Validate.Assert(isExists, "该角色名称已经存在");

        var role = Mapper.Map<SysRole>(input);
        if (role is AuditEntity auditEntity)
        {
            auditEntity.Editor = UserTokenService.GetUserToken().UserName;
            auditEntity.EditTime = DateTime.Now;
        }
        role.Id = id;
        return await _roleRepository.UpdateAsync(role, UpdatingProps<SysRole>(x => x.Name, x => x.Tips, x => x.Ordinal, d => d.Editor, d => d.EditTime));
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var isExists = await _userRepository.AnyAsync(x => x.RoleIds.Contains(id.ToString()));
        Validate.Assert(isExists, "有用户使用该角色，禁止删除");
        return await _roleRepository.DeleteAsync(id);
    }

    public async Task<int> SetPermissonsAsync(RoleSetPermissonsDto input)
    {
        Validate.Assert(input.RoleId == Guid.Empty, "禁止设置初始角色");

        await _relationRepository.DeleteRangeAsync(x => x.RoleId == input.RoleId);

        var relations = new List<SysRelation>();
        foreach (var permissionId in input.Permissions)
        {
            relations.Add(
                new SysRelation
                {
                    RoleId = input.RoleId,
                    MenuId = permissionId
                }
            );
        }
        return await _relationRepository.InsertRangeAsync(relations);
    }

    public async Task<RoleTreeDto> GetRoleTreeListByUserIdAsync(Guid userId)
    {
        RoleTreeDto result = null;
        IEnumerable<ZTreeNodeDto<Guid, dynamic>> treeNodes = null;

        var user = await _userRepository.FetchAsync(x => new { x.RoleIds }, x => x.Id == userId);
        if (user is null)
            return null;

        var roles = _roleRepository.GetAll();
        var roleIds = user.RoleIds?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Guid.Parse(x)).ToList() ?? new List<Guid>();
        if (roles.Any())
        {
            treeNodes = roles.Select(x => new ZTreeNodeDto<Guid, dynamic>
            {
                Id = x.Id,
                PID = x.Pid ?? Guid.Empty,
                Name = x.Name,
                Open = !(x.Pid.HasValue && x.Pid.Value != Guid.Empty),
                Checked = roleIds.Contains(x.Id)
            });

            result = new RoleTreeDto
            {
                TreeData = treeNodes.Select(x => new Node<Guid>
                {
                    Id = x.Id,
                    PID = x.PID,
                    Name = x.Name,
                    Checked = x.Checked
                }),
                CheckedIds = treeNodes.Where(x => x.Checked).Select(x => x.Id)
            };
        }

        return result;
    }

    public async Task<PageModelDto<RoleDto>> GetPagedAsync(RolePagedSearchDto search)
    {
        var whereExpression = ExpressionCreator
                                              .New<SysRole>()
                                              .AndIf(search.RoleName.IsNotNullOrWhiteSpace(), x => x.Name.Contains(search.RoleName));

        var total = await _roleRepository.CountAsync(whereExpression);
        if (total == 0)
            return new PageModelDto<RoleDto>(search);

        var entities = _roleRepository
                            .Where(whereExpression)
                            .OrderByDescending(x => x.Id)
                            .Skip(search.SkipRows())
                            .Take(search.PageSize)
                            .ToList();
        var dtos = Mapper.Map<List<RoleDto>>(entities);

        return new PageModelDto<RoleDto>(search, dtos, total);
    }
}