using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SiyinPractice.Domain.Core
{
    /// <summary>
    /// Ef简单的、基础的，初级的仓储接口
    /// 适合DDD开发模式,实体必须继承AggregateRoot
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBasicRepository<TEntity> : IBaseRepository<TEntity>
               where TEntity : Entity, IEntity<Guid>
    {
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        Task<int> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    }
}