using ConnmIntel.Application.Core;
using ConnmIntel.Domain.Core;
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Framework.Extensions;
using ConnmIntel.Framework.Mapper;
using ConnmIntel.Interface.WarehouseManagement;
using ConnmIntel.Shared.Core.Dto;
using ConnmIntel.Shared.Core.Exceptions;
using ConnmIntel.Shared.Core.Utility;
using ConnmIntel.Shared.WarehouseManagement.Dto.Pallet;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnmIntel.Application.WarehouseManagement.WarehouseManagement
{
    internal class PalletService : NamedEntityService<Pallet, PalletDto, PalletSearchPagedDto, CreatePalletDto>, IPalletService
    {
        private readonly IEfRepository<Inventory> _efRepository;
        private readonly IEfRepository<Box> _boxrepository;
        public PalletService(IEfRepository<Pallet> repository, IObjectMapper objectMapper, IEfRepository<Inventory> efRepository, IEfRepository<Box> boxrepository) : base(repository, objectMapper)
        {
            _efRepository = efRepository;
            _boxrepository = boxrepository;
        }
        public string GetPalletName(string palletPrefix)
        {
            var lastpallet = Repository.Where(x => x.AutomaticGeneration == true).OrderByDescending(x => x.CreateTime).FirstOrDefault();
            return SerialNoHelper.GetSerialno(lastpallet?.TagName, palletPrefix).ToString();
        }
        public async Task<PalletDto> PrintInventorySheet(CreatePalletDto createInput)//盘点paller是否存在
        {
            Validate.Assert(createInput == null, ConnmIntelMessage.DTO_IS_NULL);
            var exits = await Repository.FindAsync(x => x.Name == createInput.Name);
            var listInventory = _efRepository.Where(x => x.ScanPallet == createInput.Name).ToList();
            foreach (var item in listInventory)
            {
                if (string.IsNullOrEmpty(item.ScanPn))
                {
                    item.ScanPn = item.SysPn;
                }
            }
            List<PrintPalletTagDto> printPalletTagDtos = new List<PrintPalletTagDto>();
            printPalletTagDtos = listInventory.GroupBy(x => x.ScanPn).Select(x => new PrintPalletTagDto() { ScanPn = x.FirstOrDefault().ScanPn, ScanPnQty = x.Count() }).ToList();
            //var boxCount =await _boxrepository.CountAsync(x => x.Pallet == createInput.Name);
            if (exits != null)//返回打印信息
            {
                var palle = await MapToEntityDto(exits);
                palle.ListPallet = printPalletTagDtos;
               // palle.BoxCount = boxCount;
                return palle;
            }
            else
            {
                createInput.Bin = listInventory.Any()? listInventory.FirstOrDefault().SysBin:"";
                createInput.Location = listInventory.Any() ? listInventory.FirstOrDefault().SysLocation : "";

                //var tagName = GetPalletName(createInput.PalletPrefix);
                var entity = await MapToEntity(createInput);
                entity.TagName = null;
                if (entity is AuditEntity auditEntity)
                {
                    auditEntity.Creator = Framework.Security.UserTokenService.GetUserToken().UserName;
                    auditEntity.CreateTime = DateTime.Now;
                }
                await Repository.InsertAsync(entity);
                var palle = await MapToEntityDto(entity);
                palle.ListPallet = printPalletTagDtos;
               // palle.BoxCount = boxCount;
                return palle;
            }
        }
        public async Task<PalletDto> PrintInventorySheetByBox(CreatePalletDto createInput)//盘点paller是否存在
        {
            Validate.Assert(createInput == null, ConnmIntelMessage.DTO_IS_NULL);
            var exits = await Repository.FindAsync(x => x.Name == createInput.Name);
            var listInventory = _efRepository.Where(x => x.ScanPallet == createInput.Name);
            List<PrintPalletTagDto> printPalletTagDtos = new List<PrintPalletTagDto>();
            printPalletTagDtos = listInventory.GroupBy(x => x.BoxName).Select(x => new PrintPalletTagDto() { BoxName = x.FirstOrDefault().BoxName, BoxCount = x.Count() }).ToList();
           // var boxCount = await _boxrepository.CountAsync(x => x.Pallet == createInput.Name);
            if (exits != null)//返回打印信息
            {
                var palle = await MapToEntityDto(exits);
                palle.ListPallet = printPalletTagDtos;
                //palle.BoxCount = boxCount;
                return palle;
            }
            else
            {
                createInput.Bin = listInventory.Any() ? listInventory.FirstOrDefault().SysBin : "";
                createInput.Location = listInventory.Any() ? listInventory.FirstOrDefault().SysLocation : "";
                //var tagName = GetPalletName(createInput.PalletPrefix);
                var entity = await MapToEntity(createInput);
                entity.TagName = null;
                if (entity is AuditEntity auditEntity)
                {
                    auditEntity.Creator = Framework.Security.UserTokenService.GetUserToken().UserName;
                    auditEntity.CreateTime = DateTime.Now;
                }
                await Repository.InsertAsync(entity);
                var palle = await MapToEntityDto(entity);
                palle.ListPallet = printPalletTagDtos;
                //palle.BoxCount = boxCount;
                return palle;
            }
        }
    }
}