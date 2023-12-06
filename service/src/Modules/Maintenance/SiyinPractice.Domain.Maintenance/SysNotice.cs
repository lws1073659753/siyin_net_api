using SiyinPractice.Domain.Business;

namespace SiyinPractice.Domain.Maintenance;

/// <summary>
/// 通知
/// </summary>
public class SysNotice : BusinessEntity
{
    public string Content { get; set; }

    public string Title { get; set; }

    public int? Type { get; set; }
}