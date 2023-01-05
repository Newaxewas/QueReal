using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QueReal.BLL.Exceptions;

namespace QueReal.PL.Filters
{
	public class ExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			switch (context.Exception)
			{
				case AccessDeniedException:
					context.ExceptionHandled = true;

					context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Exception", action = "AccessDenied" }));
					break;

				case NotFoundException:
					context.ExceptionHandled = true;

					context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Exception", action = "NotFound" }));
					break;
			}
		}
	}
}
