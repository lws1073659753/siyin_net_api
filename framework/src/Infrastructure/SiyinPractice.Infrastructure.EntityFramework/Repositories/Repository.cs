using SiyinPractice.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiyinPractice.Infrastructure.EntityFramework.Repositories
{
    [Obsolete]
    public class Repository<TEntity> where TEntity : class, IEntity
    {
        private DbSet<TEntity> entities;

        protected readonly SiyinPracticeDbContext Context;

        protected IQueryable<TEntity> Table => Entities;

        //private IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        protected virtual DbSet<TEntity> Entities => Context.Set<TEntity>();
        //{
        //    get
        //    {
        //        if (entities == null)
        //            entities = Context.Set<TEntity>();

        //        return entities;
        //    }
        //}

        public Repository(SiyinPracticeDbContext dbContext)
        {
            Context = dbContext;
        }

        public virtual async ValueTask<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await Entities.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public virtual ValueTask<TEntity> GetAsync(Guid id)
        {
            return Entities.FindAsync(id);
        }

        public virtual Task<IQueryable<TEntity>> GetAsync(IEnumerable<Guid> ids)
        {
            return Task.FromResult(Entities.Where(x => ids.Contains(x.Id)));
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return Entities.ToListAsync();
        }

        public virtual Task<List<TEntity>> GetAllAsync(int beginIndex, int count)
        {
            return Entities.Skip(beginIndex).Take(count).ToListAsync();
        }

        //public virtual Task<IQueryable<TEntity>> Login(string username, string password)
        //{
        //    return Task.FromResult(Entities.Where(x => x.Name == username && x.Password == password));
        //}

        public virtual Task<int> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            AttachIfNot(entity);
            Entities.Update(entity);
            //Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
            //Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual Task<int> AttachAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Attach(entity);
            //Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
            //Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual Task<int> RemoveAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            AttachIfNot(entity);
            Entities.Remove(entity);
            return Context.SaveChangesAsync();
        }

        public virtual async Task<int> RemoveAsync(Guid id)
        {
            return await RemoveAsync(await GetAsync(id));
        }

        public virtual async Task<int> RemoveAllAsync(IEnumerable<TEntity> items)
        {
            var count = 0;
            foreach (var item in items)
            {
                count = count + await RemoveAsync(item);
            }
            return count;
        }

        public virtual async Task<int> RemoveAllAsync(IEnumerable<Guid> ids)
        {
            var count = 0;
            foreach (var id in ids)
            {
                count = count + await RemoveAsync(id);
            }
            return count;
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Context.Set<TEntity>().Attach(entity);
        }
    }
}