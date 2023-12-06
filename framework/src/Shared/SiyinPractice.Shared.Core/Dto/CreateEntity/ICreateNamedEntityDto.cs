namespace SiyinPractice.Shared.Core.Dto
{
    public interface ICreateNamedEntityDto : IInputDto
    {
        string Name { get; set; }
    }
}