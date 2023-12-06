using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SiyinPractice.Infrastructure.EntityFramework
{
    public interface IContextService
    {
        Task<System.Data.DataTable> ExecuteQuery(string sqlQuery, params SqlParameter[] parameters);
        Task<int> ExecuteNonQuery(string sqlQuery, params SqlParameter[] parameters);
    }
}