using ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas;
using System.Collections.Generic;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.Inventory
{
    public class InventoryListDto
    {
        public string Bin { get; set; }
        public string Location { get; set; }
        public string Pallet { get; set; }
        public string AutomaticTag { get; set; }
        public string BoxName { get; set; }
        public string ProjectName { get; set; }
        public int SnRules { get; set; } //sn规则
        public int PnRules { get; set; }//pn规则
        public string SnReplace { get; set; }  //sn替换
        public string EndShield { get; set; }//末尾屏蔽
        public string BoxPrefix { get; set; } //box前缀
        public string TagPrefix { get; set; } //tag前缀
        public bool IsPlantSn { get; set; } //是否厂内sn
        public bool IsBoxAutomatic { get; set; } //是否厂内sn
       // public List<PrimaryDataDto> primaryOldDataDtos { get; set; }//old
        public List<InventoryDto> primaryNewDataDtos { get; set; }//old
    }
}