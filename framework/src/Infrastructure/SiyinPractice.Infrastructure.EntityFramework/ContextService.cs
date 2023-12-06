using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiyinPractice.Infrastructure.EntityFramework
{
    public class ContextService : IContextService
    {
        /// <summary>
        /// 配置各种服务
        /// </summary>
        /// <returns></returns>

        //private static readonly IConfiguration _config;
        private readonly SiyinPracticeDbContext context;

        //public ContextService(IConfiguration configuration)
        //{
        //    _config = configuration
        //}
        public ContextService(SiyinPracticeDbContext context)
        {
            this.context = context;
        }

        public async Task<System.Data.DataTable> ExecuteQuery(string sqlQuery, params SqlParameter[] parameters)
        {
            using (var connection = context.Database.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = sqlQuery;
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var dataTable = new System.Data.DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }
                finally 
                {
                    connection.Close();
                }
                
            }
        }
        public async Task<int> ExecuteNonQuery(string sqlQuery, params SqlParameter[] parameters)
        {
            using (var connection = context.Database.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = sqlQuery;
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    // 执行存储过程
                    return await command.ExecuteNonQueryAsync();
                }
                catch (System.Exception)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        //public  void BulkInsert<TEntity1, TEntity2>(IEnumerable<TEntity1> entities1, IEnumerable<TEntity2> entities2)
        //where TEntity1 : class
        //where TEntity2 : class
        //{
        //    context.BulkInsert(entities1.ToList(), new BulkConfig { PreserveInsertOrder = true });
        //    context.BulkInsert(entities2.ToList(), new BulkConfig { PreserveInsertOrder = true });
        //}

    }
}