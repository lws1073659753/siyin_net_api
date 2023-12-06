using AutoMapper;
using System.Linq;

namespace SiyinPractice.Mapper
{
    public class AutoMapperObjectMapper : Framework.Mapper.IObjectMapper
    {
        private readonly IMapper mapper;

        public AutoMapperObjectMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            if (source == null) return default(TDestination);
            return mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return mapper.Map(source, destination);
        }

        public TDestination? Map<TSource, TDestination>(TSource source)
        {
            return mapper.Map<TSource, TDestination>(source);
        }

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source)
        {
            return mapper.ProjectTo<TDestination>(source);
        }
    }
}