using Microsoft.EntityFrameworkCore;
using Shared.Settings;

namespace Infrastructure.Database;

public class DatabaseContext : DbContext
{
    private readonly DatabaseSettings _settings;
    private readonly DbContextOptions<DatabaseContext> _options;

    public DbContextOptions<DatabaseContext> Options { get { return _options; } }

    public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions, DatabaseSettings settings) 
        : base(dbContextOptions)
    {
        _settings = settings;
        _options = dbContextOptions;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableDetailedErrors(true);
        optionsBuilder.UseSqlServer(_settings.ConnectionString ?? string.Empty, s => s.CommandTimeout(60));
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
}
