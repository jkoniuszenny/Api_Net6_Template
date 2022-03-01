
using Application.Settings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly DatabaseSettings _settings;

        public DatabaseContext(
            DbContextOptions<DatabaseContext> dbContextOptions,
            DatabaseSettings settings) : base(dbContextOptions)
        {

            _settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors(true);
            optionsBuilder.UseSqlServer(_settings.ConnectionString ?? String.Empty, s => s.CommandTimeout(60));
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}
