using SiyinPractice.Shared.Core.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SiyinPractice.Shared.BasicData.Dto.BasicData
{
    public class WarehouseDto : AuditEntityDto
    {

        /// <summary>
        /// 库位代码
        /// </summary>
        public string warehouseCode { get; set; }

    }
}
