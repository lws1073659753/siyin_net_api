using System;

namespace SiyinPractice.Domain.Core
{
    public class AuditEntity : NamedEntity, IAuditEntity
    {
        public virtual string Description { get; set; }

        public virtual string Creator { get; set; }

        public virtual DateTime? CreateTime { get; set; }

        public virtual string Editor { get; set; }

        public virtual DateTime? EditTime { get; set; }
    }
}