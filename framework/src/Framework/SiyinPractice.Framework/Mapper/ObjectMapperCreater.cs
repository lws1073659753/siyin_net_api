using System;

namespace SiyinPractice.Framework.Mapper
{
    public class ObjectMapperCreater
    {
        public ObjectMapperCreater()
        {
        }

        public ObjectMapperCreater(Type sourceType, Type destinationType)
        {
            SourceType = sourceType;
            DestinationType = destinationType;
        }

        public Type SourceType { get; set; }

        public Type DestinationType { get; set; }

        public bool TwoWay { get; private set; }

        public virtual ObjectMapperCreater ReverseMap()
        {
            TwoWay = true;
            return this;
        }
    }

    public class ObjectMapperCreater<TSourceType, TDestinationType> : ObjectMapperCreater
    {
        public ObjectMapperCreater() : base(typeof(TSourceType), typeof(TDestinationType))
        {
        }
    }
}