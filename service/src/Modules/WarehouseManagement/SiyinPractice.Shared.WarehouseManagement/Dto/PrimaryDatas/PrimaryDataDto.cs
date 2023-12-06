using ConnmIntel.Shared.Core.Dto;
using System;
using System.Collections;
using System.ComponentModel;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas
{
    public class PrimaryDataDto : AuditEntityDto 
   // , IEquatable<PrimaryDataDto>
    {
        /// <summary>
        /// 批次号
        /// </summary>
        [Description("批次号")]
        public string PiNum { get; set; }   // 批次号
        /// <summary>
        /// 部门关联部门表
        /// </summary>
        [Description("部门关联部门表")]
        public string PiDpt { get; set; }   // 部门关联部门表

        /// <summary>
        /// 希捷sn
        /// </summary>
        [Description("希捷sn")]
        public string SysOrgSn { get; set; }   // 希捷sn
        /// <summary>
        /// 希捷sys_org_pn
        /// </summary>
        [Description("希捷sys_org_pn")]
        public string SysOrgPn { get; set; }   // 希捷sys_org_pn
        /// <summary>
        /// 导入sn
        /// </summary>
        [Description("导入sn")]
        public string SysSn { get; set; }   // 导入sn
        /// <summary>
        /// 导入pn
        /// </summary>
        [Description("导入pn")]
        public string SysPn { get; set; }   // 导入pn
        /// <summary>
        /// 导入bin
        /// </summary>
        [Description("导入bin")]
        public string SysBin { get; set; }   // 导入bin
        /// <summary>
        /// 区域
        /// </summary>
        [Description("区域")]
        public string SysLocation { get; set; }   // 区域
        ///// <summary>
        ///// 厂内sn
        ///// </summary>
        //[Description("厂内sn")]
        //public string PlantSn { get; set; }   // 厂内sn
        /// <summary>
        /// 来源
        /// </summary>
        [Description("来源")]
        public string Source { get; set; }   // 来源
        /// <summary>
        /// 账册编号
        /// </summary>
        [Description("账册编号")]
        public string AccountBook { get; set; }   // 账册编号
        /// <summary>
        /// 项目号
        /// </summary>
        [Description("项目号")]
        public string PiProject { get; set; }   // 项目号
        /// <summary>
        /// 备案编号
        /// </summary>
        [Description("备案编号")]
        public string FilingNo { get; set; }   // 备案编号
        /// <summary>
        /// sn状态
        /// </summary>
        [Description("sn状态")]
        public string SnState { get; set; }   // sn状态
        /// <summary>
        /// 导入部门
        /// </summary>
        [Description("导入部门")]
        public string CreateDept { get; set; }   // 导入部门
        ///// <summary>
        ///// 项目号
        ///// </summary>
        //[Description("项目号")]
        //public string ProjectName { get; set; }   // 项目号

        //比较两个list是否相同
        //public bool Equals(PrimaryDataDto other)
        //{
        //    if (other == null) return false;
        //    if (ReferenceEquals(this, other)) return true;

        //    return PiNum == other.PiNum &&
        //        PiDpt == other.PiDpt &&
        //        SysOrgSn == other.SysOrgSn &&
        //        SysOrgPn == other.SysOrgPn &&
        //        SysSn == other.SysSn &&
        //        SysPn == other.SysPn &&
        //        SysBin == other.SysBin &&
        //        SysLocation == other.SysLocation &&
        //        PlantSn == other.PlantSn &&
        //        Source == other.Source &&
        //        AccountBook == other.AccountBook &&
        //        PiProject == other.PiProject &&
        //        FilingNo == other.FilingNo &&
        //        SnState == other.SnState &&
        //        CreateDept == other.CreateDept &&
        //        CreateTime == other.CreateTime &&
        //        EditTime == other.EditTime &&
        //        Creator == other.Creator &&
        //        Editor == other.Editor;
        //}

        //public override bool Equals(object obj)
        //{
        //    if (obj == null) return false;
        //    PrimaryDataDto other = obj as PrimaryDataDto;
        //    if (other == null) return false;
        //    else return Equals(other);
        //}

        //public override int GetHashCode()
        //{
        //    return (PiNum + PiDpt + SysOrgSn + SysOrgPn + SysSn + SysPn + SysBin + SysLocation + PlantSn + Source + AccountBook + PiProject + FilingNo + SnState + CreateDept).GetHashCode();
        //}
    }
}