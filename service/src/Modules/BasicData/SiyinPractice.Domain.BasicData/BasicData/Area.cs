using SiyinPractice.Domain.Business;
using System.Collections.Generic;
using System.ComponentModel;

namespace SiyinPractice.Domain.BasicData
{
    /// <summary>
    /// 区域
    /// </summary>
    [Description("区域")]
    public class Area : BusinessEntity
    {
        /// <summary>
        /// 区域代码
        /// </summary>
        [Description("区域代码")]
        public virtual string? areaCode { get; set; }

     


        //public virtual ICollection<Project> Projects { get; set; }
        //public virtual IList<ProjectSettlement> ProjectSettlements { get; set; } = new List<ProjectSettlement>();

    }
}
