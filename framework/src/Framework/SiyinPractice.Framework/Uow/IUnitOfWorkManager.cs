using System;
using System.Threading.Tasks;

namespace SiyinPractice.Framework.Uow
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWorkCompleteHandle Begin();

        Task<IUnitOfWorkCompleteHandle> BeginAsyc();
    }

    public interface IUnitOfWorkCompleteHandle : IDisposable
    {
        void Commit();

        Task CommitAsync();
    }
}