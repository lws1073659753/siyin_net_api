using System;

namespace SiyinPractice.Domain.Core
{
    public interface IAuditEntity : INamedEntity
    {
        string Description { get; set; }

        string Creator { get; set; }

        DateTime? CreateTime { get; set; }

        string Editor { get; set; }

        DateTime? EditTime { get; set; }
    }
}