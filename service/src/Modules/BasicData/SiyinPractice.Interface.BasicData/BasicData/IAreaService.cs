using SiyinPractice.Interface.Core;
using SiyinPractice.Shared.BasicData.Dto.BasicData;

namespace SiyinPractice.Interface.BasicData.BasicData
{
    public interface IAreaService : INamedEntityService<AreaDto, AreaSearchPagedDto, CreateAreaDto>
    {
        Task<int> GetAreas(List<string> strings);
    }
}