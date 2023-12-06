using System;

namespace SiyinPractice.Shared.Core.Dto
{
    public class AuditEntityDto : NamedEntityDto, IAuditEntityDto
    {
        public string Description { get; set; }
        public string Creator { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Editor { get; set; }
        public DateTime? EditTime { get; set; }
    }
}