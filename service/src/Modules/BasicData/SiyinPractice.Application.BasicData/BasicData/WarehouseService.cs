using SiyinPractice.Application.Core;
using SiyinPractice.Domain.BasicData;
using SiyinPractice.Domain.Core;
using SiyinPractice.Framework.Mapper;
using SiyinPractice.Interface.BasicData.BasicData;
using SiyinPractice.Shared.BasicData.Dto.BasicData;
using SiyinPractice.Shared.BasicData.Dto.Project;
using SiyinPractice.Shared.Core.Exceptions;
using SiyinPractice.Shared.Core.Utility;
using System.Linq.Expressions;

namespace SiyinPractice.Application.BasicData.BasicData
{
    public class WarehouseService : NamedEntityService<Warehouse, WarehouseDto, WarehouseSearchPagedDto, CreateWarehouseDto>, IWarehouseService
    {
        public WarehouseService(IEfRepository<Warehouse> repository, IObjectMapper objectMapper) : base(repository, objectMapper)
        {
        }
        public async Task<int> GetWarehouses(List<string> strings)
        {
            int a = 0;
            for (int i = 0; i < strings.Count; i++)
            {
                var exits = await Repository.AnyAsync(x => x.Name == strings[i]);

                if (exits == false)
                {
                    Warehouse warehouse = new();
                    warehouse.Name = strings[i];
                    warehouse.Id = Guid.NewGuid();
                    warehouse.Creator = Framework.Security.UserTokenService.GetUserToken().UserName;
                    warehouse.CreateTime = DateTime.Now;

                    a += await Repository.InsertAsync(warehouse);

                }

            }
            return a;

        }

        //判断更改名称是否重复
        public override async Task<int> UpdateAsync(WarehouseDto wdto)
        {
            Validate.Assert(wdto == null, SiyinPracticeMessage.DTO_IS_NULL);
            var nameExist = await Repository.AnyAsync(x => x.Id != wdto.Id.Value && x.Name == wdto.Name);
            Validate.Assert(nameExist, SiyinPracticeMessage.ENTITY_EXIST, wdto.Name);
            return await base.UpdateAsync(wdto);
        }
    }
}