using SiyinPractice.Domain.Business;
using System;
using System.Collections.ObjectModel;

namespace SiyinPractice.Domain.AccessControl;

/// <summary>
/// 角色
/// </summary>
public class SysRole : BusinessEntity
{
    public Guid? DeptId { get; set; }

    public int Ordinal { get; set; }

    public Guid? Pid { get; set; }

    public string Tips { get; set; }

    public int? Version { get; set; }

    public virtual Collection<SysRelation> Relations { get; set; }
}