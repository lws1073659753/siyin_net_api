using SiyinPractice.Shared.Core.Dto;
using System;

namespace SiyinPractice.Shared.AccessControl.Dto;

/// <summary>
/// 部门
/// </summary>
public class CreateDepartmentDto : IInputDto
{
    /// <summary>
    /// 部门全称
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// 序号
    /// </summary>
    public int Ordinal { get; set; }

    /// <summary>
    /// 父级Id
    /// </summary>
    public Guid? Pid { get; set; }

    ///// <summary>
    ///// 父级Id集合
    ///// </summary>
    ////public string Pids { get; set; }

    /// <summary>
    /// 部门简称
    /// </summary>
    public string SimpleName { get; set; }

    ///// <summary>
    ///// 部门描述
    ///// </summary>
    ////public string Tips { get; set; }

    ///// <summary>
    ///// 版本号
    ///// </summary>
    ////public int? Version { get; set; }
}