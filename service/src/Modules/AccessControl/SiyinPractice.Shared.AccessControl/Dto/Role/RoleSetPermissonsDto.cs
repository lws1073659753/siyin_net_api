using SiyinPractice.Shared.Core.Dto;
using System;

namespace SiyinPractice.Shared.AccessControl.Dto;

public class RoleSetPermissonsDto : IDto
{
    public Guid RoleId { set; get; }
    public Guid[] Permissions { get; set; }
}