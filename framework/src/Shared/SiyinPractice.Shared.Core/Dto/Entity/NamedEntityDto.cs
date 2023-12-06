namespace SiyinPractice.Shared.Core.Dto
{
    public class NamedEntityDto : EntityDto, INamedEntityDto
    {
        public string Name { get; set; }
    }
}