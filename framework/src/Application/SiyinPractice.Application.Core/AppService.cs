using SiyinPractice.Framework;
using SiyinPractice.Framework.Mapper;
using SiyinPractice.Interface.Core;
using System;
using System.Linq.Expressions;

namespace SiyinPractice.Application.Core
{
    public abstract class AppService : IAppService
    {
        public IObjectMapper Mapper => App.GetService<IObjectMapper>();

        //protected AppSrvResult AppSrvResult() => new();

        //protected AppSrvResult<TValue> AppSrvResult<TValue>([NotNull] TValue value) => new(value);

        //protected ProblemDetails Problem(HttpStatusCode? statusCode = null, string detail = null, string title = null, string instance = null, string type = null) => new(statusCode, detail, title, instance, type);

        protected Expression<Func<TEntity, object>>[] UpdatingProps<TEntity>(params Expression<Func<TEntity, object>>[] expressions) => expressions;
    }
}