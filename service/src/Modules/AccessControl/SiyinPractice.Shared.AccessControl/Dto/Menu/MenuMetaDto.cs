using SiyinPractice.Shared.Core.Dto;
using System;

namespace SiyinPractice.Shared.AccessControl.Dto;

/// <summary>
/// 菜单元数据
/// </summary>
[Serializable]
public class MenuMetaDto : IDto
{
    /// <summary>
    /// 菜单标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 菜单图标
    /// </summary>
    public string Icon { get; set; }
}