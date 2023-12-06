using SiyinPractice.Application.Core;
using SiyinPractice.Domain.BasicData;
using SiyinPractice.Domain.Core;
using SiyinPractice.Framework.Mapper;
using SiyinPractice.Interface.BasicData.BasicData;
using SiyinPractice.Shared.BasicData.Dto.BasicData;
using SiyinPractice.Shared.Core.Exceptions;
using SiyinPractice.Shared.Core.Utility;

using NPOI.SS.Formula.PTG;
using System.Linq.Expressions;

namespace SiyinPractice.Application.BasicData.BasicData
{
    public class AreaService : NamedEntityService<Area, AreaDto, AreaSearchPagedDto, CreateAreaDto>, IAreaService
    {
        public AreaService(IEfRepository<Area> repository, IObjectMapper objectMapper) : base(repository, objectMapper)
        {
        }
        public async Task<int> GetAreas(List<string> strings)
        {
            int a = 0;
            for (int i = 0; i < strings.Count; i++)
            {
                var exits = await Repository.AnyAsync(x => x.Name == strings[i]);

                if (exits == false)
                {
                    Area area = new();
                    area.Name = strings[i];
                    area.Id = Guid.NewGuid();
                    area.Creator = Framework.Security.UserTokenService.GetUserToken().UserName;
                    area.CreateTime = DateTime.Now;

                    a += await Repository.InsertAsync(area);

                }

            }
            return a;

        }

        //判断更改名称是否重复
        public override async Task<int> UpdateAsync(AreaDto adto)
        {
            Validate.Assert(adto == null, SiyinPracticeMessage.DTO_IS_NULL);
            var nameExist = await Repository.AnyAsync(x => x.Id != adto.Id.Value && x.Name == adto.Name);
            Validate.Assert(nameExist, SiyinPracticeMessage.ENTITY_EXIST, adto.Name);
            return await base.UpdateAsync(adto);
        }
    }
}