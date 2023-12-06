namespace SiyinPractice.Shared.Core.Dto
{
    public class NamedSearchPagedDto : SearchPagedDto, INamedSearchPagedDto
    {
        public string Name { get; set; }
    }
}