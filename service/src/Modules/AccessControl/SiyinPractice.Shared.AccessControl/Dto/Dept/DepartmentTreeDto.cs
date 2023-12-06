using System;
using System.Collections.Generic;

namespace SiyinPractice.Shared.AccessControl.Dto;

/// <summary>
/// 部门节点
/// </summary>
[Serializable]
public class DepartmentTreeDto : DepartmentDto
{
    public List<DepartmentTreeDto> Children { get; set; } = new List<DepartmentTreeDto>();
}