using SiyinPractice.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SiyinPractice.Infrastructure.EntityFramework.Repositories
{
    /// <summary>
    /// Ef简单的、基础的，初级的仓储接口
    /// 适合DDD开发模式,实体必须继承AggregateRoot
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BasicRepository<TEntity> : AbstractBaseRepository<DbContext, TEntity>, IBasicRepository<TEntity>
            where TEntity : Entity, IEntity<Guid>
    {
        public BasicRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public virtual async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            this.DbContext.UpdateRange(entities);
            return await this.DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.DbContext.Remove(entity);
            return await this.DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            this.DbContext.RemoveRange(entities);
            return await this.DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<TEntity?> GetAsync(Guid keyValue,
                                                     IEnumerable<Expression<Func<TEntity, object>>> navigationPropertyPaths = null,
                                                     bool writeDb = false,
                                                     CancellationToken cancellationToken = default)
        {
            var query = this.GetDbSet(writeDb, false).Where(t => t.Id == keyValue);
            if (navigationPropertyPaths is not null)
            {
                foreach (var navigationPropertyPath in navigationPropertyPaths)
                {
                    query = query.Include(navigationPropertyPath);
                }
            }
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}