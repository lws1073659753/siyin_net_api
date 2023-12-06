using SiyinPractice.Framework.Uow;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace SiyinPractice.Infrastructure.EntityFramework.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SiyinPracticeDbContext context;

        private IDbContextTransaction transaction;

        public UnitOfWork(SiyinPracticeDbContext context)
        {
            this.context = context;
        }

        public void Begin()
        {
            //context.beg
            transaction = context.Database.BeginTransaction();
        }

        public async Task BeginAsync()
        {
            transaction = await context.Database.BeginTransactionAsync();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public Task CommitAsync()
        {
            return transaction.CommitAsync();
        }

        public void Dispose()
        {
            if (transaction != null)
            {
                //if (!transaction.WasCommitted && transaction.IsActive)
                //{
                //    transaction.Rollback();
                //}
            }
            transaction?.Dispose();
        }
    }
}