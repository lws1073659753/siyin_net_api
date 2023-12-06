
using SiyinPractice.Interface.BasicData.BasicData;
using SiyinPractice.Shared.BasicData.Dto.BasicData;
using SiyinPractice.Web.Core.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;

namespace SiyinPractice.web.host.controllers.basicdata
{
    [Route("basicData/area")]
    [ApiController]
    [Authorize]
    public class AreaController : NamedEntityCRUDController<IAreaService, AreaDto, AreaSearchPagedDto, CreateAreaDto>
    {
        public AreaController(IAreaService service) : base(service)
        {
        }
    }
}