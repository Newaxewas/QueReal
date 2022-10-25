using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QueReal.DAL.Models;

namespace QueReal.DAL.EF
{
    internal class QueRealContext : IdentityDbContext<User,IdentityRole<Guid>,Guid>
    {
        public QueRealContext(DbContextOptions<QueRealContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(QueRealContext).Assembly);
        }
    }
}
