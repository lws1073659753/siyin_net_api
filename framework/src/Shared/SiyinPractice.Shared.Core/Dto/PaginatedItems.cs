using System.Collections.Generic;

namespace SiyinPractice.Shared.Core.Dto
{
    public class PaginatedItems<T> where T : IEntityDto
    {
        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public long Count { get; private set; }

        public IEnumerable<T> Data { get; private set; }

        public PaginatedItems(int pageIndex, int pageSize, long count, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}