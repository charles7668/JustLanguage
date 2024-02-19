using Microsoft.EntityFrameworkCore;

namespace JustLanguage.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ParseRule> ParseRule { get; set; } = null!;
    public DbSet<SupportDomain> SupportDomain { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}