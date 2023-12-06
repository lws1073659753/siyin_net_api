namespace SiyinPractice.Shared.Core.Dto;

/// <summary>
/// 查询名称条件
/// </summary>
public interface INamedSearchPagedDto : ISearchPagedDto
{
    public string Name { get; set; }
}