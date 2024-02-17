using JustLanguage.Entities;
using JustLanguage.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JustLanguage.Models;

public class InitApp : IInitApp
{
    public InitApp(IServiceProvider serviceProvider, ILogger<InitApp> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    private readonly ILogger<InitApp> _logger;

    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Init application
    /// </summary>
    public void Init()
    {
        _logger.LogInformation("start init application");
        MigrateDb();
        _logger.LogInformation("finish init application");
    }

    private async void MigrateDb()
    {
        _logger.LogInformation("start migrate db");
        IServiceProvider service = _serviceProvider;
        //get db context
        var context = service.GetRequiredService<AppDbContext>();
        IEnumerable<string> pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        // migrate db
        if (pendingMigrations.Any())
        {
            await context.Database.MigrateAsync();
        }

        _logger.LogInformation("finish migrate db");
    }
}