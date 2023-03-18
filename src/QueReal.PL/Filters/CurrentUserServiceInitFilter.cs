using Microsoft.AspNetCore.Mvc.Filters;

namespace QueReal.PL.Filters
{
    public class CurrentUserServiceInitFilter : IAsyncResourceFilter
    {
        private readonly ICurrentUserService currentUserService;

        public CurrentUserServiceInitFilter(ICurrentUserService currentUserService)
        {
            this.currentUserService = currentUserService;
        }

        public Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            currentUserService.Init(context.HttpContext.User);

            return next();
        }
    }
}
