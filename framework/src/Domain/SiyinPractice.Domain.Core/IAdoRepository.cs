using System.Data;

namespace SiyinPractice.Domain.Core
{
    public interface IAdoRepository : IRepository
    {
        void ChangeOrSetDbConnection(IDbConnection dbConnection);

        void ChangeOrSetDbConnection(string connectionString, DbTypes dbType);

        bool HasDbConnection();
    }
}