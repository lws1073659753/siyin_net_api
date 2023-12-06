using ConnmIntel.Application.Core;
using ConnmIntel.Domain.Core;
using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Framework.Extensions;
using ConnmIntel.Framework.Mapper;
using ConnmIntel.Infrastructure.EntityFramework.Repositories;
using ConnmIntel.Interface.AccessControl;
using ConnmIntel.Interface.WarehouseManagement;
using ConnmIntel.Shared.Core.Dto;
using ConnmIntel.Shared.Core.Exceptions;
using ConnmIntel.Shared.Core.Utility;
using ConnmIntel.Shared.WarehouseManagement.Dto.Auxiliary;
using ConnmIntel.Shared.WarehouseManagement.Dto.AuxiliaryInventory;
using ConnmIntel.Shared.WarehouseManagement.Dto.Box;
using ConnmIntel.Shared.WarehouseManagement.Dto.Inventory;
using ConnmIntel.Shared.WarehouseManagement.Dto.InventoryHistory;
using ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas;
using Microsoft.AspNetCore.Http;
using NetTopologySuite.Index.HPRtree;
using NPOI.HPSF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConnmIntel.Application.WarehouseManagement.WarehouseManagement
{
    public class AuxiliaryInventoryService : NamedEntityService<AuxiliaryInventory, AuxiliaryInventoryDto, AuxiliaryInventorySearchPagedDto, CreateAuxiliaryInventoryDto>, IAuxiliaryInventoryService
    {
        private readonly IUserAppService _userAppService;
        private readonly IAuxiliaryAtchService _AuxiliaryAtchService;
        private readonly IEfRepository<Auxiliary> _efRepository;
    
   
        public AuxiliaryInventoryService(IEfRepository<AuxiliaryInventory> repository, IObjectMapper objectMapper, IAuxiliaryAtchService AuxiliaryAtchService, IUserAppService userAppService, IEfRepository<Auxiliary> efRepository) : base(repository, objectMapper)
        {
            _userAppService = userAppService;
            _AuxiliaryAtchService = AuxiliaryAtchService;
            _efRepository = efRepository;
         
  
        }
        protected override Expression<Func<AuxiliaryInventory, bool>> BuildWhereExpression(Expression<Func<AuxiliaryInventory, bool>> whereExpression, AuxiliaryInventorySearchPagedDto search)
        {
            return base.BuildWhereExpression(whereExpression, search)
                 .AndIf(search.BeginTime.IsNotNullOrWhiteSpace(), x => x.CreateTime >= Convert.ToDateTime(search.BeginTime))
                  .AndIf(search.EndTime.IsNotNullOrWhiteSpace(), x => x.CreateTime <= Convert.ToDateTime(search.EndTime))
                  .AndIf(search.SysPn.IsNotNullOrWhiteSpace(), x => x.SysPn.Contains($"{search.SysPn}"))
                  .AndIf(search.Name.IsNotNullOrWhiteSpace(), x => x.Name.Contains($"{search.Name}"));
  
        }



        public async Task<List<Auxiliary>> IsRrepeatName(List<Auxiliary> auxiliary)
        {
         
            foreach(var item in auxiliary)
            {
             var result= await Repository.FindAsync(x => x.Name == item.Name && x.SysPn == item.SysPn);
                if (result == null)
                {

                    return auxiliary;
                }
                else
                {
                    auxiliary = null;
                    return auxiliary;
                }

            }
            return auxiliary;        

        }
        public async Task BulkInsertAsync(List<AuxiliaryInventory> auxiliaryNewList)
        {
            await Repository.BulkInsertAsync(auxiliaryNewList);
        }

        public async Task<PageModelDto<AuxiliaryInventoryDto>> GetPagedAsyncByExport(AuxiliaryInventorySearchPagedDto search)
        {
            var whereExpression = BuildWhereExpression(ExpressionCreator.New<AuxiliaryInventory>(), search);

            var total = await Repository.CountAsync(whereExpression);
            if (total == 0) return new PageModelDto<AuxiliaryInventoryDto>(search);

            var includeProperties = BuildIncludeProperties();

            var entities = Repository.Where(whereExpression, includeProperties)
                                     .OrderBy(x => x.Id)
                                     .ToList();

            var entityDtos = await MapToEntityDto(entities);
            return new PageModelDto<AuxiliaryInventoryDto>(search, entityDtos, total);
        }
    }
}
