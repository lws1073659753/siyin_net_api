using SiyinPractice.Domain.Core;
using System;

namespace SiyinPractice.Domain.AccessControl;

/// <summary>
/// 菜单角色关系
/// </summary>
public class SysRelation : Entity
{
    public Guid MenuId { get; set; }

    public Guid RoleId { get; set; }

    public virtual SysRole Role { get; set; }

    public virtual SysMenu Menu { get; set; }
}