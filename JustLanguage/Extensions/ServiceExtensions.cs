using System.Reflection;
using JustLanguage.Constants;
using JustLanguage.DataBase;
using JustLanguage.Entities;
using JustLanguage.Interfaces;
using JustLanguage.Models;
using JustLanguage.Services;
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
        services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));
        services.AddScoped<IInitApp, InitApp>();
        services.AddSqlite();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // default http client , this simulate a firefox browser
        services.AddHttpClient(HttpClientConstants.DEFAULT_HTTP_CLIENT_NAME, client =>
        {
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Accept",
                "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "zh-TW,zh;q=0.8,en-US;q=0.5,en;q=0.3");
        });

        services.AddScoped<IHttpCrawler, HttpCrawler>();
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