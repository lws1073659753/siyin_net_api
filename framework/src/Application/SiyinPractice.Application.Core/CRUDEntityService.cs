using SiyinPractice.Domain.Core;
using SiyinPractice.Framework.Mapper;
using SiyinPractice.Interface.Core;
using SiyinPractice.Shared.Core.Dto;
using SiyinPractice.Shared.Core.Exceptions;
using SiyinPractice.Shared.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SiyinPractice.Application.Core
{
    public abstract class CRUDEntityService<TDomain, TDto, TSearchPagedInput, TCreateInput> : ICRUDEntityService<TDto, TSearchPagedInput, TCreateInput>
         where TDomain : IEntity, new()
         where TDto : IEntityDto
         where TSearchPagedInput : ISearchPagedDto
         where TCreateInput : IDto
    {
        protected readonly IEfRepository<TDomain> Repository;

        protected readonly IObjectMapper ObjectMapper;

        public CRUDEntityService(IEfRepository<TDomain> repository, IObjectMapper objectMapper)
        {
            this.Repository = repository;
            this.ObjectMapper = objectMapper;
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="createInput"></param>
        /// <returns></returns>
        public virtual async Task<TDto> AddAsync(TCreateInput createInput)
        {
            var entity = await MapToEntity(createInput);
            if (entity is AuditEntity auditEntity)
            {
                auditEntity.Creator = Framework.Security.UserTokenService.GetUserToken().UserName;
                auditEntity.CreateTime = DateTime.Now;
            }
            await Repository.InsertAsync(entity);
            return await MapToEntityDto(entity);
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TDto> GetAsync(Guid id)
        {
            //var entity = await Repository.FindAsync(id);
            var entity = await Repository.FindAsync(id, BuildIncludeProperties(), noTracking: false);

            return await MapToEntityDto(entity);
        }

        /// <summary>
        /// 根据Id批量获取
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual async Task<IList<TDto>> GetAsync(IEnumerable<Guid> ids)
        {
            var domains = Repository.Where(x => ids.Contains(x.Id));
            var dtoTasks = domains.Select(x => MapToEntityDto(x));
            return await Task.WhenAll(dtoTasks);
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IList<TDto>> GetAllAsync()
        {
            var domains = Repository.GetAll().ToList();
            var dtoTasks = domains.Select(x => MapToEntityDto(x));
            return await Task.WhenAll(dtoTasks);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(TDto dto)
        {
            Validate.Assert(dto == null, SiyinPracticeMessage.DTO_IS_NULL);
            //var domain = await MapToEntity(dto);
            //if (domain is AuditEntity auditEntity)
            //{
            //    auditEntity.Editor = Framework.Security.UserTokenService.GetUserToken().UserName;
            //    auditEntity.EditTime = DateTime.Now;
            //}
            var entity = await Repository.FindAsync(dto.Id.Value);
            Validate.Assert(entity == null, "数据不存在");
            if (entity is AuditEntity auditEntity)
            {
                if (dto is AuditEntityDto dtoEntity)
                {
                    dtoEntity.Creator = auditEntity.Creator;
                    dtoEntity.CreateTime = auditEntity.CreateTime;
                    dtoEntity.Editor = Framework.Security.UserTokenService.GetUserToken().UserName;
                    dtoEntity.EditTime = DateTime.Now;
                }
            }
            MapToEntity(dto, entity);
            return await Repository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public virtual async Task<int> RemoveAsync(TDto dto)
        {
            Validate.Assert(dto == null, SiyinPracticeMessage.DELETE_IS_NULL);
            var entity = await Repository.FindAsync(dto.Id.Value);
            Validate.Assert(entity == null, SiyinPracticeMessage.DELETE_NONEXIST);
            //return await repository.RemoveAsync(await MapToEntity(dto));
            return await Repository.DeleteAsync(dto.Id.Value);
        }

        /// <summary>
        /// 根据Id删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<int> RemoveAsync(Guid id)
        {
            var entity = await Repository.FindAsync(id);
            Validate.Assert(entity == null, SiyinPracticeMessage.DELETE_NONEXIST);
            return await Repository.DeleteAsync(id);
        }

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public virtual async Task<int> RemoveAllAsync(IEnumerable<TDto> items)
        {
            return await Repository.DeleteRangeAsync(x => items.Any(y => y.Id == x.Id));
        }

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual Task<int> RemoveAllAsync(IEnumerable<Guid> ids)
        {
            return Repository.DeleteRangeAsync(x => ids.Contains(x.Id));
        }

        /// <summary>
        /// 实体转DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual Task<TDto> MapToEntityDto(TDomain entity)
        {
            return Task.FromResult(ObjectMapper.Map<TDto>(entity));
        }

        /// <summary>
        /// 实体批量转DTO
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        protected virtual Task<List<TDto>> MapToEntityDto(List<TDomain> entities)
        {
            return Task.FromResult(ObjectMapper.Map<List<TDto>>(entities));
        }

        /// <summary>
        /// 创建实体DTO转实体
        /// </summary>
        /// <param name="createInput"></param>
        /// <returns></returns>
        protected virtual Task<TDomain> MapToEntity(TCreateInput createInput)
        {
            return Task.FromResult(ObjectMapper.Map<TDomain>(createInput));
        }

        /// <summary>
        /// DTO转实体
        /// </summary>
        /// <param name="updateInput"></param>
        /// <returns></returns>
        protected virtual Task<TDomain> MapToEntity(TDto updateInput)
        {
            return Task.FromResult(ObjectMapper.Map<TDomain>(updateInput));
        }

        //DTO赋值实体
        protected virtual void MapToEntity(TDto updateInput, TDomain entity)
        {
            ObjectMapper.Map(updateInput, entity);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public virtual async Task<PageModelDto<TDto>> GetPagedAsync(TSearchPagedInput search)
        {
            var whereExpression = BuildWhereExpression(ExpressionCreator.New<TDomain>(), search);

            var total = await Repository.CountAsync(whereExpression);
            if (total == 0) return new PageModelDto<TDto>(search);

            var includeProperties = BuildIncludeProperties();

            var entities = Repository.Where(whereExpression, includeProperties)
                                     .OrderBy(x => x.Id)
                                     .Skip(search.SkipRows())
                                     .Take(search.PageSize)
                                     .ToList();

            var entityDtos = await MapToEntityDto(entities);
            return new PageModelDto<TDto>(search, entityDtos, total);
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        protected virtual Expression<Func<TDomain, bool>> BuildWhereExpression(Expression<Func<TDomain, bool>> whereExpression, TSearchPagedInput search)
        {
            return whereExpression;
        }

        protected IEnumerable<Expression<Func<TDomain, object>>> BuildIncludeProperties()
        {
            var includeProperties = new List<Expression<Func<TDomain, object>>>();
            AddIncludeProperties(includeProperties);
            return includeProperties;
        }

        /// <summary>
        /// 查询Include属性
        /// </summary>
        /// <returns></returns>
        protected virtual void AddIncludeProperties(List<Expression<Func<TDomain, object>>> navigationPropertyPaths)
        {
        }
    }
}