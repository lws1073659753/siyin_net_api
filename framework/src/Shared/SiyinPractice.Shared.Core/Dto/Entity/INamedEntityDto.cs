namespace SiyinPractice.Shared.Core.Dto
{
    public interface INamedEntityDto : IEntityDto
    {
        string Name { get; set; }
    }
}