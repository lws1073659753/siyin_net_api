using SiyinPractice.Shared.Core.Dto;
using System.Collections.Generic;

namespace SiyinPractice.Shared.AccessControl.Dto;

public class RolePermissionsCheckerDto : IDto
{
    public IEnumerable<long> RoleIds { get; set; }
    public IEnumerable<string> Permissions { get; set; }
}