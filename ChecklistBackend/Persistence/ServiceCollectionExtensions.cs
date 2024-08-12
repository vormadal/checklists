using Domain.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ChecklistDbContext>(config =>
        {
            config.UseNpgsql(connectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IChecklistRepository, ChecklistRepository>();
        services.AddScoped<IChecklistItemRepository, ChecklistItemRepository>();
        return services;
    }

    public static AuthenticationBuilder AddIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        }).AddEntityFrameworkStores<ChecklistDbContext>();

        services.AddIdentityServer(options =>
        {
            options.Authentication.CookieSameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode.Unspecified;
            options.Authentication.CheckSessionCookieSameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode.Unspecified;
        })
            .AddApiAuthorization<ApplicationUser, ChecklistDbContext>();

        return services.AddAuthentication();
    }

    public static async Task AddDatabaseMigrations(this IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ChecklistDbContext>();
        await context.Database.MigrateAsync();
    }

    public static async Task AddDatabaseTestData(this IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ChecklistDbContext>();
       


        //await context.TaskTypes.AddRangeAsync(new[]
        //{

        //});

        //await context.SaveChangesAsync();
    }
}
