using SiyinPractice.Shared.Core.Dto;

namespace SiyinPractice.Shared.Maintenance.Dto;

/// <summary>
/// 系统通知
/// </summary>
public class NoticeDto : EntityDto
{
    public string Creator { get; set; }

    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public int? Type { get; set; }
}