using ConnmIntel.Shared.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.AuxiliaryInventory
{
    public class AuxiliaryInventorySearchPagedDto : NamedSearchPagedDto
    {
        public string PiQty { get; set; }   // 实物bin
        public string SysBin { get; set; }   // 实物bin
        public string SysSn { get; set; }   // 实物sn
        public string SysPn { get; set; }   // 实物pn
        public string CreateDept { get; set; }
        public string BeginTime { get; set; }   //开始时间
        public string EndTime { get; set; }   // 结束时间
    }
}
