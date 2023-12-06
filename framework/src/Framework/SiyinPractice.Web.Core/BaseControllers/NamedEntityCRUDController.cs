using SiyinPractice.Interface.Core;
using SiyinPractice.Shared.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace SiyinPractice.Web.Core.BaseControllers
{
    public abstract class NamedEntityCRUDController<TDto, TSearchPagedInput, TCreateInput> : CRUDEntityController<TDto, TSearchPagedInput, TCreateInput>
        where TDto : INamedEntityDto
        where TSearchPagedInput : INamedSearchPagedDto
        where TCreateInput : IInputDto
    {
        protected readonly INamedEntityService<TDto, TSearchPagedInput, TCreateInput> NamedEntityService;

        public NamedEntityCRUDController(INamedEntityService<TDto, TSearchPagedInput, TCreateInput> service) : base(service)
        {
            NamedEntityService = service;
        }

        [HttpGet]
        [Route("name/{name}")]
        public virtual Task<TDto> GetByName(string name)
        {
            return NamedEntityService.GetByNameAsync(name);
        }
    }

    public abstract class NamedEntityCRUDController<TNamedCRUDEntityService, TDto, TSearchPagedInput, TCreateInput> : CRUDEntityController<TNamedCRUDEntityService, TDto, TSearchPagedInput, TCreateInput>
        where TNamedCRUDEntityService : INamedEntityService<TDto, TSearchPagedInput, TCreateInput>
        where TDto : INamedEntityDto
        where TSearchPagedInput : INamedSearchPagedDto
        where TCreateInput : IInputDto
    {
        public NamedEntityCRUDController(TNamedCRUDEntityService service) : base(service)
        {
        }

        [HttpGet]
        [Route("name/{name}")]
        public virtual Task<TDto> GetByName(string name)
        {
            return EntityService.GetByNameAsync(name);
        }
    }
}