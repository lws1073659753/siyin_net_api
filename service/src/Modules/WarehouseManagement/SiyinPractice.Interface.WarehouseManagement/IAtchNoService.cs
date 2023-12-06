using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Interface.Core;
using ConnmIntel.Shared.WarehouseManagement.Dto.AtchNo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnmIntel.Interface.WarehouseManagement
{
    public interface IAtchNoService : INamedEntityService<AtchNoDto, AtchNoSearchPagedDto, CreateAtchNoDto>
    {
        Task<List<AtchNoDto>> GetAtchNoState(List<string> strings);
        List<AtchNo> CreateAtchNo(List<string> strings, bool automaticGeneration = false);
        Task<AtchNoDto> GetLastAtchNo();
        Task<int> IsCreateAtch(AtchNoDto atchNoDto, int count);
        Task<int> UpdateAtchNoState(string name);
    }
}