using SiyinPractice.Shared.Core.Dto;
using System;
using System.Collections.Generic;

namespace SiyinPractice.Shared.AccessControl.Dto;

/// <summary>
/// 精简部门树结构
/// </summary>
[Serializable]
public class DepartmentSimpleTreeDto : EntityDto
{
    /// <summary>
    /// 部门简称
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// 子部门
    /// </summary>
    public List<DepartmentSimpleTreeDto> Children { get; private set; } = new List<DepartmentSimpleTreeDto>();
}