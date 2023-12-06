using SiyinPractice.Domain.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.Common;

namespace SiyinPractice.Infrastructure.EntityFramework
{
    public class BaseDbContext : DbContext
    {
        //private string connectionString;

        //public BaseDbContext()
        //{
        //}

        //public BaseDbContext(string connectionString)
        //{
        //    this.connectionString = connectionString;
        //}

        public BaseDbContext(DbContextOptions<BaseDbContext> options)
        : base(options)
        {
        }

        #region Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //var loggerFactory = new Microsoft.Extensions.Logging.LoggerFactory();
            //loggerFactory.AddProvider(new EntityFrameworkLoggerProvider());
            //optionsBuilder.UseLoggerFactory(loggerFactory);
            //if (!string.IsNullOrEmpty(connectionString))
            //{
            //    optionsBuilder.UseOracle(connectionString, b => b.UseOracleSQLCompatibility("11"));
            //}
            //optionsBuilder.UseSqlServer("server=connm-intel.cn,1433;User ID=ccf;Password=Password000;database=PSMTemp;");
        }

        protected virtual string CreateSqlWithParameters(string sql, params object[] parameters)
        {
            //add parameters to sql
            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (!(parameters[i] is DbParameter parameter))
                    continue;

                sql = $"{sql}{(i > 0 ? "," : string.Empty)} @{parameter.ParameterName}";

                //whether parameter is output
                if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
                    sql = $"{sql} output";
            }

            return sql;
        }

        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A set for the given entity type</returns>
        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : Entity
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Generate a script to create all tables for the current model
        /// </summary>
        /// <returns>A SQL script</returns>
        public virtual string GenerateCreateScript()
        {
            return Database.GenerateCreateScript();
        }

        /// <summary>
        /// Creates a LINQ query for the query type based on a raw SQL query
        /// </summary>
        /// <typeparam name="TQuery">Query type</typeparam>
        /// <param name="sql">The raw SQL query</param>
        /// <param name="parameters">The values to be assigned to parameters</param>
        /// <returns>An IQueryable representing the raw SQL query</returns>
        //public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql, params object[] parameters) where TQuery : class
        //{
        //    return Query<TQuery>().FromSql(CreateSqlWithParameters(sql, parameters), parameters);
        //}

        /// <summary>
        /// Creates a LINQ query for the entity based on a raw SQL query
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="sql">The raw SQL query</param>
        /// <param name="parameters">The values to be assigned to parameters</param>
        /// <returns>An IQueryable representing the raw SQL query</returns>
        //public virtual IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : Entity
        //{
        //    return Set<TEntity>().FromSql(CreateSqlWithParameters(sql, parameters), parameters);
        //}

        /// <summary>
        /// Executes the given SQL against the database
        /// </summary>
        /// <param name="sql">The SQL to execute</param>
        /// <param name="doNotEnsureTransaction">true - the transaction creation is not ensured; false - the transaction creation is ensured.</param>
        /// <param name="timeout">The timeout to use for command. Note that the command timeout is distinct from the connection timeout, which is commonly set on the database connection string</param>
        /// <param name="parameters">Parameters to use with the SQL</param>
        /// <returns>The number of rows affected</returns>
        //public virtual int ExecuteSqlCommand(RawSqlString sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        //{
        //    //set specific command timeout
        //    var previousTimeout = Database.GetCommandTimeout();
        //    Database.SetCommandTimeout(timeout);

        //    var result = 0;
        //    if (!doNotEnsureTransaction)
        //    {
        //        //use with transaction
        //        using (var transaction = Database.BeginTransaction())
        //        {
        //            result = Database.ExecuteSqlCommand(sql, parameters);
        //            transaction.Commit();
        //        }
        //    }
        //    else
        //        result = Database.ExecuteSqlCommand(sql, parameters);

        //    //return previous timeout back
        //    Database.SetCommandTimeout(previousTimeout);

        //    return result;
        //}

        /// <summary>
        /// Detach an entity from the context
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        public virtual void Detach<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityEntry = Entry(entity);
            if (entityEntry == null)
                return;

            //set the entity is not being tracked by the context
            entityEntry.State = EntityState.Detached;
        }

        public System.Data.DataTable ExecuteQuery(string sqlQuery, params SqlParameter[] parameters)
        {
            using (var connection = Database.GetDbConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sqlQuery;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new System.Data.DataTable();
                    dataTable.Load(reader);
                    return dataTable;
                }
            }
        }

        //public int ExecuteCommand(string sqlCommand, params object[] parameters)
        //{
        //    return Database.ExecuteSqlCommand(sqlCommand, parameters);
        //}

        #endregion Methods
    }
}