using SiyinPractice.Shared.Core.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SiyinPractice.Shared.BasicData.Dto.BasicData
{
    public class CreateWarehouseDto : CreateAuditEntityDto
    {


        /// <summary>
        /// 库位代码
        /// </summary>
        public string warehouseCode { get; set; }


        //public List<Guid> InvoiceIds { get; set; } = new List<Guid>();
    }
}
