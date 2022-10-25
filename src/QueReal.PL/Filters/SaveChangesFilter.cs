using Microsoft.AspNetCore.Mvc.Filters;
using QueReal.BLL.Interfaces;

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
            await next();
            await databaseService.SaveChangesAsync();
        }
    }
}
