using SiyinPractice.Domain.Core;
using SiyinPractice.Framework.Mapper;
using SiyinPractice.Interface.Core;
using SiyinPractice.Shared.Core.Dto;
using SiyinPractice.Shared.Core.Exceptions;
using SiyinPractice.Shared.Core.Utility;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SiyinPractice.Application.Core
{
    public abstract class NamedEntityService<TDomain, TDto, TSearchPagedInput, TCreateInput>
                                                 : CRUDEntityService<TDomain, TDto, TSearchPagedInput, TCreateInput>, INamedEntityService<TDto, TSearchPagedInput, TCreateInput>
                             where TDomain : class, INamedEntity, new()
                             where TDto : class, INamedEntityDto
                             where TSearchPagedInput : INamedSearchPagedDto
                             where TCreateInput : ICreateNamedEntityDto
    {
        public NamedEntityService(IEfRepository<TDomain> repository, IObjectMapper objectMapper) : base(repository, objectMapper)
        {
        }

        public override async Task<TDto> AddAsync(TCreateInput createInput)
        {
            Validate.Assert(createInput == null, SiyinPracticeMessage.DTO_IS_NULL);
            var exits = await Repository.AnyAsync(x => x.Name == createInput.Name);
            Validate.Assert(exits, SiyinPracticeMessage.ENTITY_EXIST, createInput.Name);
            return await base.AddAsync(createInput);
        }

        public override async Task<int> UpdateAsync(TDto dto)
        {
            Validate.Assert(dto == null, SiyinPracticeMessage.DTO_IS_NULL);

            //var nameExist = await Repository.AnyAsync(x => x.Id != dto.Id.Value && x.Name == dto.Name);
            //Validate.Assert(!nameExist, SiyinPracticeMessage.ENTITY_EXIST, dto.Name);

            //var entity = await Repository.FindAsync(dto.Id.Value);
            //if (dto is AuditEntity dtoEntity)
            //{
            //    if(entity is AuditEntity auditEntity)
            //    {
            //        dtoEntity.Creator = auditEntity.Creator;
            //        dtoEntity.CreateTime = auditEntity.CreateTime;
            //        dtoEntity.Editor = Framework.Security.UserTokenService.GetUserToken().UserName;
            //        dtoEntity.EditTime = DateTime.Now;
            //    }
            //}
            //MapToEntity(dto, entity);
            return await base.UpdateAsync(dto);
        }

        public virtual async Task<TDto> GetByNameAsync(string name)
        {
            return await MapToEntityDto(await Repository.FindAsync(x => x.Name == name, BuildIncludeProperties()));
        }

        public Task<bool> NameExistAsync(Guid id, string name)
        {
            return Repository.AnyAsync(x => x.Id == id && x.Name == name);
        }

        protected override Expression<Func<TDomain, bool>> BuildWhereExpression(Expression<Func<TDomain, bool>> whereExpression, TSearchPagedInput search)
        {
            return base.BuildWhereExpression(whereExpression, search)
                  .AndIf(search.Name.IsNotNullOrWhiteSpace(), x => x.Name.Contains($"{search.Name}"));
        }
    }
}