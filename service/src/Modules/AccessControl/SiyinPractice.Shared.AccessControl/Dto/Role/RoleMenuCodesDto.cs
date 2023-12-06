using SiyinPractice.Shared.Core.Dto;
using System;

namespace SiyinPractice.Shared.AccessControl.Dto;

/// <summary>
/// 角色，权限
/// </summary>
[Serializable]
public class RoleMenuCodesDto : IDto
{
    /// <summary>
    /// 菜单Code
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 角色Id
    /// </summary>
    public Guid RoleId { get; set; }
}