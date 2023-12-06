using ConnmIntel.Shared.Core.Dto;
using System;
using System.Collections;
using System.ComponentModel;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.AuxiliaryInventory
{
    public class AuxiliaryInventoryHistoryDto : AuditEntityDto 
    {
 
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
        /// 状态
        /// </summary>
        [Description("状态")]
        public virtual string PnState { get; set; }   // 状态
        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门")]
        public virtual string CreateDept { get; set; }   // 部门
    }
}