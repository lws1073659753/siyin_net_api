using SiyinPractice.Shared.Core.Dto;

namespace SiyinPractice.Shared.Maintenance.Dto;

[Serializable]
public class DictDto : EntityDto
{
    public string Name { get; set; }
    public string Description { get; set; }

    public int Ordinal { get; set; }

    public Guid? Pid { get; set; }

    private string _value;

    public string Value
    {
        get => _value is not null ? _value : string.Empty;
        set => _value = value;
    }

    private IList<DictDto> _data = Array.Empty<DictDto>();

    public IList<DictDto> Children
    {
        get => _data;
        set
        {
            if (value != null)
            {
                _data = value;
            }
        }
    }
}