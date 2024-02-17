using JustLanguage.Entities;
using Microsoft.EntityFrameworkCore;

namespace JustLanguage.Services;

public static class InitApp
{
    /// <summary>
    /// Migrate database
    /// </summary>
    /// <param name="app"></param>
    public static async void MigrateDb(WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider;
        //get db context
        var context = service.GetRequiredService<AppDbContext>();
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        // migrate db
        if (pendingMigrations.Any())
            await context.Database.MigrateAsync();
    }
}