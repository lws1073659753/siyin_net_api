using System;

namespace SiyinPractice.Shared.Core.Dto
{
    [Serializable]
    public abstract class EntityDto : IEntityDto
    {
        public Guid? Id { get; set; }
    }
}