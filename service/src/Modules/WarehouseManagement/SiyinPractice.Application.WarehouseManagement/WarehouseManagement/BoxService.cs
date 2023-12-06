using ConnmIntel.Application.Core;
using ConnmIntel.Domain.Core;
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Framework.Extensions;
using ConnmIntel.Framework.Mapper;
using ConnmIntel.Interface.WarehouseManagement;
using ConnmIntel.Shared.Core.Exceptions;
using ConnmIntel.Shared.Core.Utility;
using ConnmIntel.Shared.WarehouseManagement.Dto.Box;
using ConnmIntel.Shared.WarehouseManagement.Dto.Inventory;
using NetTopologySuite.Geometries;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnmIntel.Application.WarehouseManagement.WarehouseManagement
{
    internal class BoxService : NamedEntityService<Box, BoxDto, BoxSearchPagedDto, CreateBoxDto>, IBoxService
    {
        private readonly IEfRepository<Inventory> _efRepository;
        public BoxService(IEfRepository<Box> repository, IObjectMapper objectMapper, IEfRepository<Inventory> efRepository) : base(repository, objectMapper)
        {
            _efRepository = efRepository;
        }
        public async Task<List<InventoryDto>> AnyAsyncBoxName(string boxName)
        {
            List<InventoryDto> inventoryDtos = new List<InventoryDto>();
            var exits = await Repository.AnyAsync(x => x.Name == boxName);
            if (exits)
            {
                var entitylist = _efRepository.Where(x => x.BoxName == boxName).ToList();
                inventoryDtos = ObjectMapper.Map<List<InventoryDto>>(entitylist);
                return inventoryDtos;
            }
            else
            {
                return inventoryDtos;
            }
        }
        public async Task<string> GetBoxName(string boxPrefix, string bin, string pallet)
        {
            var lastpallet = Repository.Where(x => x.AutomaticGeneration == true).OrderByDescending(x => x.CreateTime).FirstOrDefault();
            var boxName = SerialNoHelper.GetSerialno(lastpallet?.Name, boxPrefix == null ? "" : boxPrefix).ToString();
            CreateBoxDto createBoxDto = new CreateBoxDto();
            createBoxDto.Name = boxName;
            createBoxDto.AutomaticGeneration = true;
            createBoxDto.State = "已盘点";
            createBoxDto.BoxCount = 0;
            createBoxDto.Bin = bin;
            createBoxDto.Pallet = pallet;
            await IsCreateBox(createBoxDto);
            return boxName;
        }
        public async Task<BoxDto> IsCreateBox(CreateBoxDto createBoxDto)
        {
            var exits = await Repository.FindAsync(x => x.Name == createBoxDto.Name);
            if (exits != null)
            {
                exits.BoxCount = createBoxDto.BoxCount;
                exits.Pallet = createBoxDto.Pallet;
                exits.Location = createBoxDto.Location;
                exits.Bin = createBoxDto.Bin;
                await Repository.UpdateAsync(exits);
                return await MapToEntityDto(exits);

            }
            else
            {
                var entity = await MapToEntity(createBoxDto);
                if (entity is AuditEntity auditEntity)
                {
                    auditEntity.Creator = Framework.Security.UserTokenService.GetUserToken().UserName;
                    auditEntity.CreateTime = DateTime.Now;
                }
                await Repository.InsertAsync(entity);
                return await MapToEntityDto(entity);
            }
        }

        public async Task<int> UpdateBoxCount(string boxName)
        {
            var box = await Repository.FindAsync(x => x.Name == boxName);
            Validate.Assert(box == null, ConnmIntelMessage.DELETE_IS_NULL);

            box.BoxCount--;
            return await Repository.UpdateAsync(box);
        }
    }
}