
using SiyinPractice.Interface.BasicData.BasicData;
using SiyinPractice.Shared.BasicData.Dto.BasicData;
using SiyinPractice.Web.Core.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;

namespace SiyinPractice.web.host.controllers.basicdata
{
    [Route("basicData/warehouse")]
    [ApiController]
    [Authorize]
    public class WarehouseController : NamedEntityCRUDController<IWarehouseService, WarehouseDto, WarehouseSearchPagedDto, CreateWarehouseDto>
    {
        public WarehouseController(IWarehouseService service) : base(service)
        {
        }
    }
}