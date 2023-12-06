using ConnmIntel.Domain.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Domain.WarehouseManagement

{
    public class AuxiliaryAtch :BusinessEntity
    {
        /// <summary>
        /// 批次号
        /// </summary>
        [Description("批次号")]
        public override string Name { get; set; }   // 批次号
        /// <summary>
        /// 状态
        /// </summary>
        [Description("状态")]
        public virtual string State { get; set; }   // 状态
        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门")]
        public virtual string DeptName { get; set; }   // 部门

    
    }
}
