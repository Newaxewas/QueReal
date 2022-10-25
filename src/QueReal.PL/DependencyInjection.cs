using QueReal.PL.Filters;

namespace QueReal.PL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPl(this IServiceCollection services, IConfiguration config)
        {
            services.AddMvc(config =>
            {
                config.Filters.Add<SaveChangesFilter>();
            });

            return services;
        }
    }
}
