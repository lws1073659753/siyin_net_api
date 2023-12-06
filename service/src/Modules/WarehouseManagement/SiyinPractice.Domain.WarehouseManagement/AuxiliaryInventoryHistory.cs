using ConnmIntel.Domain.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Domain.WarehouseManagement
{
    [Description("辅料数据盘点历史记录")]
    public class AuxiliaryInventoryHistory :BusinessEntity
    {
        /// <summary>
        /// 批次号
        /// </summary>
        [Description("批次号")]
        public override string Name { get; set; }   // 批次号
        /// <summary>
        /// SysPn
        /// </summary>
        [Description("SysPn")]
        public virtual string SysPn { get; set; }   // 批次号
        /// <summary>
        /// PnQty
        /// </summary>
        [Description("PnQty")]
        public virtual int PnQty { get; set; }   // PnQty
        /// <summary>
        /// 货位
        /// </summary>
        [Description("货位")]
        public virtual string SysBin { get; set; }   // 货位
        /// <summary>
        /// 区域
        /// </summary>
        [Description("区域")]
        public virtual string SysLocation { get; set; }   // 区域
        /// <summary>
        /// 盘点状态
        /// </summary>
        [Description("盘点状态")]
        public virtual string PnState { get; set; }   // 盘点状态
        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门")]
        public virtual string CreateDept { get; set; }   // 部门
    }
}
