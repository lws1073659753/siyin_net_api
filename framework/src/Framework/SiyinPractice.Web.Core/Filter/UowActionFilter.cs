using SiyinPractice.Framework.Uow;
using SiyinPractice.Web.Core.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SiyinPractice.Web.Core.Filter
{
    public class UowActionFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWorkManager unitOfWorkManager;

        public UowActionFilter(IUnitOfWorkManager unitOfWorkManager)
        {
            this.unitOfWorkManager = unitOfWorkManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                await next();
                return;
            }
            var method = context.ActionDescriptor.GetMethodInfo();
            var transactionAttribute = method.GetTransactionAttribute();
            if (transactionAttribute == null || transactionAttribute.ReadOnly)
            {
                await next();
                return;
            }

            using (var uow = unitOfWorkManager.Begin())
            {
                var result = await next();
                if (result.Exception == null || result.ExceptionHandled)
                {
                    await uow.CommitAsync();
                }
            }
        }
    }
}