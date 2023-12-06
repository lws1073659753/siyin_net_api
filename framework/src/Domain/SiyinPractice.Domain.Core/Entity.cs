using System;

namespace SiyinPractice.Domain.Core
{
    public class Entity : IEntity
    {
        public virtual Guid Id { get; set; }
    }
}