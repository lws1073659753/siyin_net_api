using SiyinPractice.Domain.BasicData;

using SiyinPractice.Framework.Mapper;
using SiyinPractice.Shared.BasicData.Dto.BasicData;
using SiyinPractice.Shared.BasicData.Dto.Project;


namespace SiyinPractice.Application.BasicData.BasicData.Mapper
{
    public class ObjectMapperConfigration : IObjectMapperConfigration
    {
        public IList<ObjectMapperCreater> ObjectMapperCreaterBuilder()
        {
            var mappingData = new List<ObjectMapperCreater>();
            mappingData.Add(new ObjectMapperCreater(typeof(WarehouseDto), typeof(Warehouse)).ReverseMap());
            mappingData.Add(new ObjectMapperCreater(typeof(CreateWarehouseDto), typeof(Warehouse)));
            mappingData.Add(new ObjectMapperCreater(typeof(AreaDto), typeof(Area)).ReverseMap());
            mappingData.Add(new ObjectMapperCreater(typeof(CreateAreaDto), typeof(Area)));
            mappingData.Add(new ObjectMapperCreater(typeof(ProjectDto), typeof(Project)).ReverseMap());
            mappingData.Add(new ObjectMapperCreater(typeof(CreateProjectDto), typeof(Project)));
            //mappingData.Add(new ObjectMapperCreater(typeof(WarehouseDto), typeof(Warehouse)).ReverseMap());
            return mappingData;
        }
    }
}