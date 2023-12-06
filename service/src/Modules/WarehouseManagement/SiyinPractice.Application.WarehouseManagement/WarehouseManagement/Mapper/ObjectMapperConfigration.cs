using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Framework.Mapper;
using ConnmIntel.Shared.WarehouseManagement.Dto.AtchNo;
using ConnmIntel.Shared.WarehouseManagement.Dto.Auxiliary;
using ConnmIntel.Shared.WarehouseManagement.Dto.AuxiliaryInventory;
using ConnmIntel.Shared.WarehouseManagement.Dto.Box;
using ConnmIntel.Shared.WarehouseManagement.Dto.Inventory;
using ConnmIntel.Shared.WarehouseManagement.Dto.InventoryHistory;
using ConnmIntel.Shared.WarehouseManagement.Dto.Pallet;
using ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas;
using System.Collections.Generic;

namespace ConnmIntel.Application.WarehouseManagement.WarehouseManagement.Mapper
{
    public class ObjectMapperConfigration : IObjectMapperConfigration
    {
        public IList<ObjectMapperCreater> ObjectMapperCreaterBuilder()
        {
            var mappingData = new List<ObjectMapperCreater>();
            mappingData.Add(new ObjectMapperCreater(typeof(PrimaryDataDto), typeof(PrimaryData)).ReverseMap());
            mappingData.Add(new ObjectMapperCreater(typeof(CreatePrimaryDataDto), typeof(PrimaryData)));
            mappingData.Add(new ObjectMapperCreater(typeof(AtchNoDto), typeof(AtchNo)).ReverseMap());
            mappingData.Add(new ObjectMapperCreater(typeof(CreateAtchNoDto), typeof(AtchNo)));
            mappingData.Add(new ObjectMapperCreater(typeof(InventoryDto), typeof(Inventory)).ReverseMap());
            mappingData.Add(new ObjectMapperCreater(typeof(CreateInventoryDto), typeof(Inventory)));
            mappingData.Add(new ObjectMapperCreater(typeof(InventoryHistoryDto), typeof(InventoryHistory)).ReverseMap());
            mappingData.Add(new ObjectMapperCreater(typeof(CreateInventoryHistoryDto), typeof(InventoryHistory)));
            mappingData.Add(new ObjectMapperCreater(typeof(BoxDto), typeof(Box)).ReverseMap());
            mappingData.Add(new ObjectMapperCreater(typeof(CreateBoxDto), typeof(Box)));
            mappingData.Add(new ObjectMapperCreater(typeof(PalletDto), typeof(Pallet)).ReverseMap());
            mappingData.Add(new ObjectMapperCreater(typeof(CreatePalletDto), typeof(Pallet)));
            mappingData.Add(new ObjectMapperCreater(typeof(AuxiliaryDto), typeof(Auxiliary)).ReverseMap());
            mappingData.Add(new ObjectMapperCreater(typeof(CreateAuxiliaryDto), typeof(Auxiliary)).ReverseMap());
            mappingData.Add(new ObjectMapperCreater(typeof(AuxiliaryAtchDto), typeof(AuxiliaryAtch)).ReverseMap());
            mappingData.Add(new ObjectMapperCreater(typeof(CreateAuxiliaryAtchDto), typeof(AuxiliaryAtch)));
            mappingData.Add(new ObjectMapperCreater(typeof(AuxiliaryInventoryDto), typeof(AuxiliaryInventory)).ReverseMap());
            mappingData.Add(new ObjectMapperCreater(typeof(CreateAuxiliaryInventoryDto), typeof(AuxiliaryInventory)));
            return mappingData;
        }
    }
}