using SiyinPractice.Shared.Core.Dto;

namespace SiyinPractice.Shared.AccessControl.Dto;

public class CreateRoleDto : IInputDto
{
    /// <summary>
    /// 角色名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 角色描述
    /// </summary>
    public string Tips { get; set; }

    /// <summary>
    /// 序号
    /// </summary>
    public int Ordinal { get; set; }
}