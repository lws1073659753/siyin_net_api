using System;

namespace SiyinPractice.Shared.Core.Dto
{
    public interface IEntityDto : IDto
    {
        Guid? Id { get; set; }
    }
}