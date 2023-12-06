using System;

namespace SiyinPractice.Shared.Core.Dto
{
    public interface IAuditEntityDto : INamedEntityDto
    {
        string Description { get; set; }
        string Creator { get; set; }
        DateTime? CreateTime { get; set; }
        string Editor { get; set; }
        DateTime? EditTime { get; set; }
    }
}