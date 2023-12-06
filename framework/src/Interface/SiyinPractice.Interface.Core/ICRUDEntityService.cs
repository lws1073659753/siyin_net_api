using SiyinPractice.Shared.Core.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiyinPractice.Interface.Core
{
    public interface ICRUDEntityService<TDto, TSearchPagedInput, TCreateInput> : IAppService
         where TDto : IEntityDto
         where TSearchPagedInput : ISearchPagedDto
         where TCreateInput : IDto
    {
        Task<TDto> AddAsync(TCreateInput createInput);

        Task<int> UpdateAsync(TDto dto);

        Task<int> RemoveAsync(TDto dto);

        Task<int> RemoveAsync(Guid id);

        Task<int> RemoveAllAsync(IEnumerable<TDto> items);

        Task<int> RemoveAllAsync(IEnumerable<Guid> ids);

        Task<TDto> GetAsync(Guid id);

        Task<IList<TDto>> GetAsync(IEnumerable<Guid> ids);

        Task<IList<TDto>> GetAllAsync();

        Task<PageModelDto<TDto>> GetPagedAsync(TSearchPagedInput search);
    }
}