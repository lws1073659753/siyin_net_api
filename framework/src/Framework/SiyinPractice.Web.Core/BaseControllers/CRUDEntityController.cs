using SiyinPractice.Framework.Uow;
using SiyinPractice.Interface.Core;
using SiyinPractice.Shared.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace SiyinPractice.Web.Core.BaseControllers
{
    public abstract class CRUDEntityController<TDto, TSearchPagedInput, TCreateInput> : ControllerBase
        where TDto : INamedEntityDto
        where TSearchPagedInput : ISearchPagedDto
        where TCreateInput : IInputDto
    {
        protected readonly ICRUDEntityService<TDto, TSearchPagedInput, TCreateInput> EntityService;

        public CRUDEntityController(ICRUDEntityService<TDto, TSearchPagedInput, TCreateInput> service)
        {
            this.EntityService = service;
        }

        [HttpPost]
        [Transaction]
        public virtual Task<TDto> Create(TCreateInput accessRoler)
        {
            return EntityService.AddAsync(accessRoler);
        }

        [HttpPut]
        [Transaction]
        public virtual Task<int> Update(TDto accessRoler)
        {
            return EntityService.UpdateAsync(accessRoler);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TDto> Get(Guid id)
        {
            return EntityService.GetAsync(id);
        }

        [HttpGet]
        [Route("getall")]
        public virtual Task<IList<TDto>> GetAll()
        {
            return EntityService.GetAllAsync();
        }

        [HttpDelete("{id}")]
        [Transaction]
        public virtual Task<int> Remove(Guid id)
        {
            return EntityService.RemoveAsync(id);
        }

        [HttpDelete]
        [Transaction]
        [Route("removebyids")]
        public virtual Task<int> RemoveByIds(IEnumerable<Guid> ids)
        {
            return EntityService.RemoveAllAsync(ids);
        }

        [HttpGet]
        public Task<PageModelDto<TDto>> PagingQuery([FromQuery] TSearchPagedInput searchPagedInput)
        {
            return EntityService.GetPagedAsync(searchPagedInput);
        }
    }

    public abstract class CRUDEntityController<TCRUDEntityService, TDto, TSearchPagedInput, TCreateInput> : ControllerBase
       where TCRUDEntityService : ICRUDEntityService<TDto, TSearchPagedInput, TCreateInput>
       where TDto : INamedEntityDto
       where TSearchPagedInput : ISearchPagedDto
       where TCreateInput : IInputDto
    {
        protected readonly TCRUDEntityService EntityService;

        public CRUDEntityController(TCRUDEntityService service)
        {
            this.EntityService = service;
        }

        [HttpPost]
        [Transaction]
        public virtual Task<TDto> Create(TCreateInput accessRoler)
        {
            return EntityService.AddAsync(accessRoler);
        }

        [HttpPut]
        [Transaction]
        public virtual Task<int> Update(TDto accessRoler)
        {
            return EntityService.UpdateAsync(accessRoler);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TDto> Get(Guid id)
        {
            return EntityService.GetAsync(id);
        }

        [HttpGet]
        [Route("getall")]
        public virtual Task<IList<TDto>> GetAll()
        {
            return EntityService.GetAllAsync();
        }

        [HttpDelete("{id}")]
        [Transaction]
        public virtual Task<int> Remove(Guid id)
        {
            return EntityService.RemoveAsync(id);
        }

        [HttpDelete]
        [Transaction]
        [Route("removebyids")]
        public virtual Task<int> RemoveByIds(IEnumerable<Guid> ids)
        {
            return EntityService.RemoveAllAsync(ids);
        }

        [HttpGet]
        public Task<PageModelDto<TDto>> PagingQuery([FromQuery] TSearchPagedInput searchPagedInput)
        {
            return EntityService.GetPagedAsync(searchPagedInput);
        }
    }
}