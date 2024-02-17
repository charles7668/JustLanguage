using JustLanguage.Entities;
using JustLanguage.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JustLanguage.Models;

public class InitApp : IInitApp
{
    public InitApp(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Init application
    /// </summary>
    public void Init()
    {
        MigrateDb();
    }

    private async void MigrateDb()
    {
        var service = _serviceProvider;
        //get db context
        var context = service.GetRequiredService<AppDbContext>();
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        // migrate db
        if (pendingMigrations.Any())
            await context.Database.MigrateAsync();
    }
}