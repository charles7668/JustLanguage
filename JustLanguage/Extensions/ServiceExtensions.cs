using JustLanguage.Entities;
using Microsoft.EntityFrameworkCore;

namespace JustLanguage.Extensions;

public static class ServiceExtensions
{
    /// <summary>
    /// init services
    /// </summary>
    /// <param name="services"></param>
    public static void InitServices(this IServiceCollection services)
    {
        services.AddSqlite();
    }

    /// <summary>
    /// configure sqlite as database
    /// </summary>
    /// <param name="services"></param>
    private static void AddSqlite(this IServiceCollection services)
    {
        services.AddDbContextPool<AppDbContext>(options => { options.UseSqlite("Data source=JustLanguage.db"); });
    }
}