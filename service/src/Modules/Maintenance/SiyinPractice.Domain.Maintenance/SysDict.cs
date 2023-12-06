using SiyinPractice.Domain.Business;
using SiyinPractice.Domain.Core;

namespace SiyinPractice.Domain.Maintenance;

/// <summary>
/// 字典
/// </summary>
public class SysDict : BusinessEntity, ISoftDelete
{
    public int Ordinal { get; set; }

    public Guid Pid { get; set; }

    public string? Value { get; set; }

    public bool IsDeleted { get; set; }
}