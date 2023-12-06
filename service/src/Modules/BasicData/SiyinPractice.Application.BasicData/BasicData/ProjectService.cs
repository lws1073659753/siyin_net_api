using SiyinPractice.Application.Core;
using SiyinPractice.Domain.BasicData;
using SiyinPractice.Domain.Core;
using SiyinPractice.Domain.Maintenance;
using SiyinPractice.Framework.Mapper;
using SiyinPractice.Interface.AccessControl;
using SiyinPractice.Interface.BasicData;
using SiyinPractice.Shared.BasicData.Dto.BasicData;
using SiyinPractice.Shared.BasicData.Dto.Project;
using SiyinPractice.Shared.Core.Dto;
using SiyinPractice.Shared.Core.Exceptions;
using SiyinPractice.Shared.Core.Utility;
using SiyinPractice.Shared.Maintenance.Dto;
using NPOI.Util;
using System.Linq.Expressions;
using System.Net.NetworkInformation;

namespace SiyinPractice.Application.BasicData.BasicData
{
    public class ProjectService : NamedEntityService<Project, ProjectDto, ProjectSearchPagedDto, CreateProjectDto>, IProjectService
    {
        private readonly IUserAppService _userAppService;
        private string PROJECTDEPARTMENTMAP = "ProjectDepartmentMap";
        private readonly IEfRepository<SysDict> _dictionaryRepository;


        public ProjectService(IEfRepository<Project> repository, IObjectMapper objectMapper, IUserAppService userAppService, IEfRepository<SysDict> dictionaryRepository) : base(repository, objectMapper)
        {
            _dictionaryRepository=dictionaryRepository;
            _userAppService = userAppService;
        }
        public List<Project> GetProjectState(List<string> strings)
        {
            var results = Repository.Where( e => strings.Contains(e.Name)).ToList();//查数据重复sn
            return results;
        }
        public async Task<int> GetProjects(List<string> strings)
        {
            int a=0;
            for(int i = 0; i < strings.Count; i++)
            {
                var exits = await Repository.AnyAsync(x => x.Name == strings[i]);

                if (exits == false)
                {
                    Project project = new();
                    project.Name = strings[i];
                    project.Id = Guid.NewGuid();
                    project.Creator = Framework.Security.UserTokenService.GetUserToken().UserName;
                    project.CreateTime = DateTime.Now;
                    var userinfo = await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId);
                    project.CreateDept = userinfo?.Profile?.DeptFullName;
                    a +=await Repository.InsertAsync(project);
                    
                }
               
            }
            return a;
            
        }

        public override async Task<IList<ProjectDto>> GetAllAsync()
        {
            var userinfo = await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId);
            var DeptFullName = userinfo?.Profile?.DeptFullName;
            var domains = Repository.Where(x=>x.CreateDept == DeptFullName).ToList();
            var dtoTasks = domains.Select(x => MapToEntityDto(x));
            return await Task.WhenAll(dtoTasks);
        }
        public override async Task<ProjectDto> AddAsync(CreateProjectDto createProject)
        {
            var userinfo = await _userAppService.GetUserInfoAsync(Framework.Security.UserTokenService.GetUserToken().UserId);
            //createProject.CreateDept = userinfo?.Profile?.DeptFullName;
            var exits = await Repository.AnyAsync(x => x.Name == createProject.Name && x.CreateDept== createProject.CreateDept);
            Validate.Assert(exits, SiyinPracticeMessage.ENTITY_EXIST, createProject.Name);

            var entity = await MapToEntity(createProject);
            if (entity is AuditEntity auditEntity)
            {
                auditEntity.Creator = Framework.Security.UserTokenService.GetUserToken().UserName;
                auditEntity.CreateTime = DateTime.Now;
            }
            await Repository.InsertAsync(entity);
            return await MapToEntityDto(entity);
        }

        protected override Expression<Func<Project, bool>> BuildWhereExpression(Expression<Func<Project, bool>> whereExpression, ProjectSearchPagedDto search)
        {
            return base.BuildWhereExpression(whereExpression, search)
                 .AndIf(search.CreateDept.IsNotNullOrWhiteSpace(), x => x.CreateDept.Contains($"{search.CreateDept}"))
                  .AndIf(search.Name.IsNotNullOrWhiteSpace(), x => x.Name.Contains($"{search.Name}"));
        }
        /// <summary>
        /// 根据字典找部门
        /// </summary>
        /// <returns></returns>
        public async Task<List<DictDto>> GetItem()
        {
            var status = new List<DictDto>();
            var dictioary = await _dictionaryRepository.FindAsync(x => x.Name == PROJECTDEPARTMENTMAP);
            if (dictioary != null)
            {
                var items = _dictionaryRepository.Where(x => x.Pid == dictioary.Id).ToList();
                status = ObjectMapper.Map<List<DictDto>>(items);
            }
            return status;
        }

        //判断更改项目号名称是否重复
        public override async Task<int> UpdateAsync(ProjectDto pdto)
        {
            Validate.Assert(pdto == null, SiyinPracticeMessage.DTO_IS_NULL);
            var nameExist = await Repository.AnyAsync(x =>x.Id !=pdto.Id.Value&& x.Name==pdto.Name && x.CreateDept ==pdto.CreateDept);
            Validate.Assert(nameExist, SiyinPracticeMessage.ENTITY_EXIST, pdto.Name);
            return await base.UpdateAsync(pdto);
        }
    }
}