using SiyinPractice.Shared.Core.Dto;
using System;

namespace SiyinPractice.Shared.AccessControl.Dto
{
    public class UserSetRoleDto : IInputDto
    {
        public Guid[] RoleIds { get; set; }
    }
}