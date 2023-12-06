using ConnmIntel.Shared.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.Auxiliary
{
    public class AuxiliaryAtchDto : AuditEntityDto
    {    /// <summary>
         /// 批次号状态
         /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string DeptName { get; set; }
    }
}
