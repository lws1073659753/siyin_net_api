using ConnmIntel.Domain.Business;
using System.ComponentModel;

namespace ConnmIntel.Domain.WarehouseManagement
{
    /// <summary>
    /// 批次号
    /// </summary>
    [Description("批次号")]
    public class AtchNo : BusinessEntity
    {
        /// <summary>
        /// 批次号状态
        /// </summary>
        public virtual string State { get; set; }   
        /// <summary>
        /// 是否自动生成
        /// </summary>
        public virtual bool AutomaticGeneration { get; set; }
        /// <summary>
        /// 总盘点量
        /// </summary>
        public virtual int TotalInventory { get; set; }
        /// <summary>
        /// 待盘数量
        /// </summary>
        public virtual int ToBeCounted { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public virtual string DeptName { get; set; }
    }
}