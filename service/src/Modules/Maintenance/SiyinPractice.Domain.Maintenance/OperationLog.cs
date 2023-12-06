using SiyinPractice.Domain.Core;

namespace SiyinPractice.Domain.Maintenance;

/// <summary>
/// 操作日志
/// </summary>
public class OperationLog : Entity
{
    public string ClassName { get; set; } = default!;

    public DateTime? CreateTime { get; set; }

    public string LogName { get; set; } = default!;

    public string LogType { get; set; } = default!;

    public string Message { get; set; } = default!;

    public string Method { get; set; } = default!;

    public string Succeed { get; set; } = default!;

    public long? UserId { get; set; } = default!;

    public string Account { get; set; } = default!;

    public string UserName { get; set; } = default!;

    public string RemoteIpAddress { get; set; } = default!;
}