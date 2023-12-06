using SiyinPractice.Application.BasicData.BasicData;
using SiyinPractice.Interface.BasicData;
using SiyinPractice.Interface.BasicData.BasicData;
using SiyinPractice.Shared.BasicData.Dto.BasicData;
using SiyinPractice.Shared.BasicData.Dto.Project;
using SiyinPractice.Shared.Maintenance.Dto;
using SiyinPractice.Web.Core.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiyinPractice.web.host.controllers.BasicData
{ 
    [Route("basicData/item")]
    [ApiController]
    [Authorize]

    public class ProjectController : NamedEntityCRUDController<IProjectService, ProjectDto, ProjectSearchPagedDto, CreateProjectDto>
    {
        public ProjectController(IProjectService service) : base(service)
        {
        }

        [HttpGet]
        [Route("GetItem")]
        public Task<List<DictDto>> GetItem()
        {
            return EntityService.GetItem();
        }
    }
}