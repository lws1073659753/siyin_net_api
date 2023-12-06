using System;

namespace SiyinPractice.Domain.Core
{
    public interface IEntity : IEntity<Guid>
    {
        //Guid Id { get; set; }
    }

    //public interface IEntity<TKey>
    //{
    //    public TKey Id { get; set; }
    //}

    public interface IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}