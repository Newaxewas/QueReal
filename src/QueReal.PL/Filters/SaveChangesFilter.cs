using Microsoft.AspNetCore.Mvc.Filters;

namespace QueReal.PL.Filters
{
    public class SaveChangesFilter : IAsyncActionFilter
    {
        private readonly IDatabaseService databaseService;

        public SaveChangesFilter(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next();

            if (executedContext.Exception == null) 
            {
                await databaseService.SaveChangesAsync();
			}
		}
    }
}
