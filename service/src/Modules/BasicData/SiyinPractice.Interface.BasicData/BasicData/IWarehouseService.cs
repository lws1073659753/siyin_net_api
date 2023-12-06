using SiyinPractice.Interface.Core;
using SiyinPractice.Shared.BasicData.Dto.BasicData;

namespace SiyinPractice.Interface.BasicData.BasicData
{
    public interface IWarehouseService : INamedEntityService<WarehouseDto, WarehouseSearchPagedDto, CreateWarehouseDto>
    {
        Task<int> GetWarehouses(List<string> strings);
    }
}