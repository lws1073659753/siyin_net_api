using SiyinPractice.Shared.Core.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SiyinPractice.Shared.BasicData.Dto.BasicData
{
    public class CreateAreaDto : CreateAuditEntityDto
    {


        /// <summary>
        /// 区域代码
        /// </summary>
        public string areaCode { get; set; }


        //public List<Guid> InvoiceIds { get; set; } = new List<Guid>();
    }
}
