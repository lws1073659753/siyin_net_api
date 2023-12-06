using SiyinPractice.Framework.Uow;
using System.Threading.Tasks;

namespace SiyinPractice.Infrastructure.EntityFramework.Uow
{
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWorkManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IUnitOfWorkCompleteHandle Begin()
        {
            unitOfWork.Begin();
            return unitOfWork;
        }

        public async Task<IUnitOfWorkCompleteHandle> BeginAsyc()
        {
            await unitOfWork.BeginAsync();
            return unitOfWork;
        }
    }
}