using SiyinPractice.Shared.Core.Dto;

namespace SiyinPractice.Shared.AccessControl.Dto
{
    public class CreateAccessUserDto : CreateAuditEntityDto
    {
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}