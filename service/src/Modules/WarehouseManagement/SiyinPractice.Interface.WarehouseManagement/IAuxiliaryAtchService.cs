
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Interface.Core;
using ConnmIntel.Shared.WarehouseManagement.Dto.AtchNo;
using ConnmIntel.Shared.WarehouseManagement.Dto.Auxiliary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnmIntel.Interface.WarehouseManagement
{
    public interface IAuxiliaryAtchService : INamedEntityService<AuxiliaryAtchDto, AuxiliaryAtchSearchPagedDto, CreateAuxiliaryAtchDto>
    {
        Task<AuxiliaryAtchDto> GetLastAuxiliaryAtch();
        Task<int> IsCreateAtch(AuxiliaryAtchDto auxiliaryAtchNoDto);
    }
}