using SiyinPractice.Shared.Core.Dto;
using System;

namespace SiyinPractice.Shared.AccessControl.Dto;

/// <summary>
/// 角色
/// </summary>
[Serializable]
public class RoleDto : EntityDto
{
    ///// <summary>
    ///// 部门Id
    ///// </summary>
    ////public long? DeptId { get; set; }

    /// <summary>
    /// 角色名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 序号
    /// </summary>
    public int Ordinal { get; set; }

    /// <summary>
    /// 父级角色Id
    /// </summary>
    public long? Pid { get; set; }

    /// <summary>
    /// 角色描述
    /// </summary>
    public string Tips { get; set; }

    /// <summary>
    /// 权限集合
    /// </summary>
    public string Permissions { get; set; }

    ///// <summary>
    ///// 角色版本号
    ///// </summary>
    ////public int? Version { get; set; }
}