using System.Collections.Generic;

namespace SiyinPractice.Framework.Mapper
{
    public interface IObjectMapperConfigration
    {
        IList<ObjectMapperCreater> ObjectMapperCreaterBuilder();
    }
}