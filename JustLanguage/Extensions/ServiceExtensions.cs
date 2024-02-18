using JustLanguage.DataBase;
using JustLanguage.Entities;
using JustLanguage.Interfaces;
using JustLanguage.Models;
using JustLanguage.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JustLanguage.Extensions;

public static class ServiceExtensions
{
    /// <summary>
    /// init services
    /// </summary>
    /// <param name="services"></param>
    public static void InitServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));
        services.AddScoped<IInitApp, InitApp>();
        services.AddSqlite();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    /// <summary>
    /// configure sqlite as database
    /// </summary>
    /// <param name="services"></param>
    private static void AddSqlite(this IServiceCollection services)
    {
        services.AddDbContextPool<AppDbContext>(options =>
        {
            options.UseSqlite("Data source=JustLanguage.db;foreign keys=true;");
            options.EnableSensitiveDataLogging(false);
        });
    }
}