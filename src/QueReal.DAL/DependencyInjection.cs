using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using QueReal.DAL.EF;
using QueReal.DAL.Interfaces;

namespace QueReal.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDal(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<QueRealContext>(GetDbContextConfigurator(config));
            services.AddScoped<DbContext>(provider => provider.GetService<QueRealContext>());

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            services.AddIdentity<User, IdentityRole<Guid>>(ConfigureIdentity)
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<QueRealContext>();

            return services;
        }

        private static Action<DbContextOptionsBuilder> GetDbContextConfigurator(IConfiguration config)
        {
            string connectionString = config.GetConnectionString("DbConnection");

            return options => options.UseSqlServer(connectionString);
        }

        private static void ConfigureIdentity(IdentityOptions options)
        {
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredLength = ModelConstants.User_Password_MinLength;

            options.User.RequireUniqueEmail = true;
        }
    }
}
