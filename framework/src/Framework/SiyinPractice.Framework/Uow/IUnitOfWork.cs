using System.Threading.Tasks;

namespace SiyinPractice.Framework.Uow
{
    public interface IUnitOfWork : IUnitOfWorkCompleteHandle
    {
        void Begin();

        Task BeginAsync();
    }
}