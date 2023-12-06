using SiyinPractice.Domain.Core;
using SiyinPractice.Shared.Core.Exceptions;
using SiyinPractice.Shared.Core.Utility;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using NPOI.POIFS.Crypt.Dsig;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace SiyinPractice.Infrastructure.EntityFramework.Repositories
{
    /// <summary>
    /// Ef仓储的基类实现,抽象类
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class AbstractBaseRepository<TDbContext, TEntity> : IBaseRepository<TEntity>
       where TDbContext : DbContext
       where TEntity : Entity, IEntity<Guid>
    {
        protected virtual TDbContext DbContext { get; }

        protected AbstractBaseRepository(TDbContext dbContext) => DbContext = dbContext;

        protected virtual IQueryable<TEntity> GetDbSet(bool writeDb, bool noTracking)
        {
            if (noTracking && writeDb)
                return DbContext.Set<TEntity>().AsNoTracking().TagWith(RepositoryConsts.MAXSCALE_ROUTE_TO_MASTER);
            else if (noTracking)
                return DbContext.Set<TEntity>().AsNoTracking();
            else if (writeDb)
                return DbContext.Set<TEntity>().TagWith(RepositoryConsts.MAXSCALE_ROUTE_TO_MASTER);
            else
                return DbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression,
                                                 IEnumerable<Expression<Func<TEntity, object>>> navigationPropertyPaths = null,
                                                 bool writeDb = false,
                                                 bool noTracking = true)
        {
            var query = GetDbSet(writeDb, noTracking).Where(expression);
            if (navigationPropertyPaths is not null)
            {
                foreach (var navigationPropertyPath in navigationPropertyPaths)
                {
                    query = query.Include(navigationPropertyPath);
                }
            }
            return query;
        }

        public virtual async Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression, bool writeDb = false, CancellationToken cancellationToken = default)
        {
            var dbSet = DbContext.Set<TEntity>().AsNoTracking();
            if (writeDb)
                dbSet = dbSet.TagWith(RepositoryConsts.MAXSCALE_ROUTE_TO_MASTER);
            return await dbSet.AnyAsync(whereExpression, cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression, bool writeDb = false, CancellationToken cancellationToken = default)
        {
            var dbSet = DbContext.Set<TEntity>().AsNoTracking();
            if (writeDb)
                dbSet = dbSet.TagWith(RepositoryConsts.MAXSCALE_ROUTE_TO_MASTER);
            return await dbSet.CountAsync(whereExpression, cancellationToken);
        }

        public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            AttachIfNot(entity);
            DbContext.Set<TEntity>().Update(entity);
            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = DbContext.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }
            DbContext.Set<TEntity>().Attach(entity);
        }
        public virtual async Task<bool> BulkInsertAsync(IEnumerable<TEntity> entities,CancellationToken cancellationToken = default)
        {
            try
            {
                await DbContext.BulkInsertAsync(entities.ToList(), new BulkConfig { PreserveInsertOrder = true });
            }
            catch (Exception ex)
            {
                Validate.Assert(true, ex.Message);
                throw;
            }
            return true;
            //await CommitAsync();
        }
        public virtual async Task<bool> BulkUpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                await DbContext.BulkUpdateAsync(entities.ToList(), new BulkConfig {});
                await DbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Validate.Assert(true, ex.Message);
                throw;
            }
            return true;
            //await CommitAsync();
        }
        //public List<T> Query<T>(Expression<Func<T, bool>> predicate) where T : class
        //{
        //    var query = DbContext.Set<T>().AsQueryable().Where(predicate);

        //    return query.ToList();
        //}

        //public virtual async Task<int> BulkInsertAsyncByTable(DataTable dataTable, CancellationToken cancellationToken = default)
        //{

        //    await DbContext.BulkInsertAsync(dataTable.Rows.ToListDynamic(), new BulkConfig { PreserveInsertOrder = true });
        //    //await DbContext.BulkInsertAsync(entities.ToList());
        //    return await DbContext.SaveChangesAsync(cancellationToken);
        //    //await CommitAsync();
        //}
    }
}