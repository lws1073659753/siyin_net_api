using SiyinPractice.Shared.Core.Dto;

namespace SiyinPractice.Shared.Maintenance.Dto;

/// <summary>
/// 系统配置
/// </summary>
[Serializable]
public class CfgDto : AuditEntityDto
{
    /// <summary>
    /// 参数值
    /// </summary>
    public string Value { get; set; }
}