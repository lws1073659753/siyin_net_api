using ConnmIntel.Domain.Business;
using System.ComponentModel;

namespace ConnmIntel.Domain.WarehouseManagement
{
    /// <summary>
    /// Box
    /// </summary>
    [Description("Box")]
    public class Box : BusinessEntity
    {
        /// <summary>
        /// Box状态
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
        public virtual int BoxCount { get; set; }   
        /// <summary>
        /// paller拖号
        /// </summary>
        public virtual string Pallet { get; set; }   
    }
}