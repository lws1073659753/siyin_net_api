using SiyinPractice.Shared.Core.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiyinPractice.Shared.BasicData.Dto.Project
{
    public class ProjectSearchPagedDto : NamedSearchPagedDto
    {
        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门")]
        public string? CreateDept { get; set; }
    }
}
