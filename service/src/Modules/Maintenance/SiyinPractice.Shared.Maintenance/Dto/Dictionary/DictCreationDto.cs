using SiyinPractice.Shared.Core.Dto;

namespace SiyinPractice.Shared.Maintenance.Dto;

public class DictCreationDto : IInputDto
{
    public string Name { get; set; }

    public string Value { get; set; }
    public string Description { get; set; }

    public int Ordinal { get; set; }

    public IList<DictCreationDto> Children { get; set; }
}