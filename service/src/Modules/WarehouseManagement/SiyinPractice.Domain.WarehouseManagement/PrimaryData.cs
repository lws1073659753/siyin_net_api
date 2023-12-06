using ConnmIntel.Domain.Business;
using System.ComponentModel;

namespace ConnmIntel.Domain.WarehouseManagement
{
    /// <summary>
    /// 主料数据e
    /// </summary>
    [Description("主料数据")]
    public class PrimaryData : BusinessEntity
    {
        /// <summary>
        /// 批次号
        /// </summary>
        [Description("批次号")]
        public virtual string PiNum { get; set; }   // 批次号
        /// <summary>
        /// 部门关联部门表
        /// </summary>
        [Description("部门关联部门表")]
        public virtual string PiDpt { get; set; }   // 部门关联部门表
        
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
        /// 导入bin
        /// </summary>
        [Description("导入bin")]
        public virtual string SysBin { get; set; }   // 导入bin
        /// <summary>
        /// 区域
        /// </summary>
        [Description("区域")]
        public virtual string SysLocation { get; set; }   // 区域
        ///// <summary>
        ///// 厂内sn
        ///// </summary>
        //[Description("厂内sn")]
        //public virtual string PlantSn { get; set; }   // 厂内sn
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
        /// 项目号
        /// </summary>
        [Description("项目号")]
        public virtual string PiProject { get; set; }   // 项目号
        /// <summary>
        /// 备案编号
        /// </summary>
        [Description("备案编号")]
        public virtual string FilingNo { get; set; }   // 备案编号
        /// <summary>
        /// sn状态
        /// </summary>
        [Description("sn状态")]
        public virtual string SnState { get; set; }   // sn状态
        /// <summary>
        /// 导入部门
        /// </summary>
        [Description("导入部门")]
        public virtual string CreateDept { get; set; }   // 导入部门
        ///// <summary>
        ///// 项目号
        ///// </summary>
        //[Description("项目号")]
        //public virtual string ProjectName { get; set; }   // 项目号
    }
}