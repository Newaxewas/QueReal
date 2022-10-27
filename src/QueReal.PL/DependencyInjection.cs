using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using QueReal.PL.Filters;

namespace QueReal.PL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPl(this IServiceCollection services, IConfiguration config)
        {
            services.AddMvc(config =>
            {
                config.Filters.AddAuthorizeFilter();

                config.Filters.Add<CurrentUserServiceInitFilter>();
                config.Filters.Add<SaveChangesFilter>();
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Login";
                options.LogoutPath = "/User/Logout";
            });

            return services;
        }

        private static void AddAuthorizeFilter(this FilterCollection filters)
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            var authFilter = new AuthorizeFilter(policy);

            filters.Add(authFilter);
        }
    }
}
