using SiyinPractice.Shared.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace SiyinPractice.Web.Core.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        public ILogger<ExceptionFilter> Logger { get; set; }

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            Logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                var e = context.Exception;
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }
                Logger.LogError(e.ToString());

                var message = e is SiyinPracticeException contelWorksException ?
                                    string.Format(contelWorksException.ResourceName, contelWorksException.Parameters) :
                                    e.Message;

                context.Result = new ObjectResult(message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.ExceptionHandled = true;
            }
        }
    }
}