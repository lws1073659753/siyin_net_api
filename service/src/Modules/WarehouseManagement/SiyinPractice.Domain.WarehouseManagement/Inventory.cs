using ConnmIntel.Domain.Business;
using System.ComponentModel;

namespace ConnmIntel.Domain.WarehouseManagement
{
    [Description("盘点")]
    public  class Inventory : BusinessEntity
    {
        /// <summary>
        /// 实物sn
        /// </summary>
        [Description("实物sn")]
        public virtual string ScanSn { get; set; }   // 实物sn

        /// <summary>
        /// 实物Pn
        /// </summary>
        [Description("实物Pn")]
        public virtual string ScanPn { get; set; }   // 实物Pn

        /// <summary>
        /// 批次号
        /// </summary>
        [Description("批次号")]
        public virtual string PiNum { get; set; }   // 批次号

        /// <summary>
        /// Sn部门
        /// </summary>
        [Description("Sn部门")]
        public virtual string PiDpt { get; set; }   // Sn部门

        /// <summary>
        /// 项号
        /// </summary>
        [Description("项号")]
        public virtual string PiProject { get; set; }   // 项号

        /// <summary>
        /// 希捷sn
        /// </summary>
        [Description("希捷sn")]
        public virtual string SysOrgSn { get; set; }   // 希捷sn

        /// <summary>
        /// 希捷sys_org_pn
        /// </summary>
        [Description("希捷sys_org_pn")]
        public virtual string SysOrgPn { get; set; }   // 希捷sys_org_pn
        /// <summary>
        /// 导入sn
        /// </summary>
        [Description("导入sn")]
        public virtual string SysSn { get; set; }   // 导入sn
        /// <summary>
        /// 导入pn
        /// </summary>
        [Description("导入pn")]
        public virtual string SysPn { get; set; }   // 导入pn
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
        /// 来源
        /// </summary>
        [Description("来源")]
        public virtual string Source { get; set; }   // 来源

        /// <summary>
        /// 账册编号
        /// </summary>
        [Description("账册编号")]
        public virtual string AccountBook { get; set; }   // 账册编号

        /// <summary>
        /// 备案编号
        /// </summary>
        [Description("备案编号")]
        public virtual string FilingNo { get; set; }   // 备案编号

        /// <summary>
        /// 导入部门
        /// </summary>
        [Description("导入部门")]
        public virtual string CreateDept { get; set; }   // 导入部门

        /// <summary>
        /// 扫入托盘
        /// </summary>
        [Description("扫入托盘")]
        public virtual string ScanPallet { get; set; }   // 扫入托盘

        /// <summary>
        /// 自动tag
        /// </summary>
        [Description("自动tag")]
        public virtual string AutomaticTag { get; set; }   // 自动tag

        /// <summary>
        /// box
        /// </summary>
        [Description("Box")]
        public virtual string BoxName { get; set; }   // box
        /// <summary>
        /// 项目号
        /// </summary>
        [Description("项目号")]
        public virtual string ProjectName { get; set; }   // 项目号
    }
}