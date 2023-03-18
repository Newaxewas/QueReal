using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueReal.BLL.Services;

namespace QueReal.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBll(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IQuestService, QuestService>();

            return services;
        }
    }
}
