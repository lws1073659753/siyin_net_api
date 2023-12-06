using ConnmIntel.Domain.WarehouseManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas
{
    public class ReturnTemplate 
    {
        public int State { get; set; }
        public int InsertCount { get; set; }
        public int UpdateCount { get; set; }
        public string Message { get; set; }
       public List<string> ListStrings { get; set; }
        public List<PrimaryData> PrimaryOldDatas { get; set; }
        public  List<PrimaryData> PrimaryNewDatas { get; set; }
        public  List<string> diffPrimaryDataDtos { get; set; }
        public  List<string> inventoryDataDtos { get; set; }
    }
}
