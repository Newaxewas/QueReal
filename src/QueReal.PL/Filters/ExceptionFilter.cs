using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QueReal.BLL.Exceptions;

namespace QueReal.PL.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionMessage = context.Exception.Message;

            switch (context.Exception)
            {
                case AccessDeniedException:
                    context.ExceptionHandled = true;

                    context.Result = new ForbidResult(exceptionMessage);
                    break;

                case BadRequestException:
                    context.ExceptionHandled = true;

                    context.Result = new BadRequestObjectResult(exceptionMessage);
                    break;

                case NotFoundException:
                    context.ExceptionHandled = true;

                    context.Result = new NotFoundObjectResult(exceptionMessage);
                    break;
            }
        }
    }
}
