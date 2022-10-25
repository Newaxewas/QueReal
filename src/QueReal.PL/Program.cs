using QueReal.BLL;
using QueReal.BLL.Interfaces;
using QueReal.DAL;
using QueReal.PL;

namespace Queral.Pl;

internal static class Program 
{
    public static void Main(string[] args)
    {
        WebApplication
            .CreateBuilder(args)
            .ConfigureBuilder()
            .Build()
            .Configure()
            .Run();
    }

    private static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddDal(builder.Configuration)
            .AddBll(builder.Configuration)
            .AddPl(builder.Configuration);

        return builder;
    }

    private static WebApplication Configure(this WebApplication app)
    {
        app.InitDatabase();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        return app;
    }

    private static void InitDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var databaseService = scope.ServiceProvider.GetService<IDatabaseService>();

        databaseService.InitDatabaseAsync().Wait();
    }
}