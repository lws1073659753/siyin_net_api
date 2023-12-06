using SiyinPractice.Shared.Core.Dto;
using System;
using System.Threading.Tasks;

namespace SiyinPractice.Interface.Core
{
    public interface INamedEntityService<TDto, TSearchPagedInput, TCreateInput> : ICRUDEntityService<TDto, TSearchPagedInput, TCreateInput>
         where TDto : INamedEntityDto
         where TSearchPagedInput : INamedSearchPagedDto
         where TCreateInput : IDto
    {
        Task<TDto> GetByNameAsync(string name);

        Task<bool> NameExistAsync(Guid id, string name);
    }
}