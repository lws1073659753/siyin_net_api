using SiyinPractice.Domain.BasicData;
using SiyinPractice.Interface.Core;
using SiyinPractice.Shared.BasicData.Dto.Project;
using SiyinPractice.Shared.Maintenance.Dto;

namespace SiyinPractice.Interface.BasicData
{
    public interface IProjectService : INamedEntityService<ProjectDto, ProjectSearchPagedDto, CreateProjectDto>
    {
        List<Project> GetProjectState(List<string> strings);
        Task<int> GetProjects(List<string> strings);
        Task<List<DictDto>> GetItem();

    }
}