using SiyinPractice.Shared.Core.Dto;

namespace SiyinPractice.Shared.Core.Exceptions
{
    public static class SearchPagedDtoExtension
    {
        /// <summary>
        /// 计算查询需要跳过的行数
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static int SkipRows(this ISearchPagedDto dto) => (dto.PageIndex - 1) * dto.PageSize;
    }
}