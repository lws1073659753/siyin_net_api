using ConnmIntel.Domain.Business;
using System.ComponentModel;

namespace ConnmIntel.Domain.WarehouseManagement
{
    /// <summary>
    /// Pallet
    /// </summary>
    [Description("Pallet")]
    public class Pallet : BusinessEntity
    {
        /// <summary>
        /// Pallet状态
        /// </summary>
        public virtual string State { get; set; }
        ///name ===boxname
        /// <summary>
        /// bin
        /// </summary>c string PiNum { get; set; }     //批次号
        public virtual string Bin { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public virtual string Location { get; set; }
        /// <summary>
        /// 是否自动生成
        /// </summary>
        public virtual bool AutomaticGeneration { get; set; }
        /// <summary>
        /// box数量
        /// </summary>
        public virtual int PalletCount { get; set; }
        /// <summary>
        /// Tag
        /// </summary>
        public virtual string TagName { get; set; }
    }
}