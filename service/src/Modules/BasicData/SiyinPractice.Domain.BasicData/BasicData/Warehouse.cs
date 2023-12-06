using SiyinPractice.Domain.Business;
using System.Collections.Generic;
using System.ComponentModel;

namespace SiyinPractice.Domain.BasicData
{
    /// <summary>
    /// 库位
    /// </summary>
    [Description("库位")]
    public class Warehouse : BusinessEntity
    {
        /// <summary>
        /// 库位代码
        /// </summary>
        [Description("客户代码")]
        public virtual string? warehouseCode { get; set; }

     


        //public virtual ICollection<Project> Projects { get; set; }
        //public virtual IList<ProjectSettlement> ProjectSettlements { get; set; } = new List<ProjectSettlement>();

    }
}
