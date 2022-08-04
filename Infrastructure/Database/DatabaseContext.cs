using Application.Interfaces.Providers;
using Domain.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Settings;
using System.Reflection;
using System.Text.Json;

namespace Infrastructure.Database;

public class DatabaseContext : DbContext
{
    private readonly DatabaseSqlSettings _settings;
    private readonly DbContextOptions<DatabaseContext>? _options;
    private readonly IUserProvider _userProvider;

    public DbContextOptions<DatabaseContext>? Options { get { return _options; } }

    public DbSet<Audit> Audits { get; set; }

    public DatabaseContext(
       DatabaseSqlSettings settings,
       IUserProvider userProvider)
    {
        _settings = settings;
        _userProvider = userProvider;
    }


    public DatabaseContext(
        DbContextOptions<DatabaseContext> dbContextOptions,
        DatabaseSqlSettings settings,
        IUserProvider userProvider)
        : base(dbContextOptions)
    {
        _settings = settings;
        _userProvider = userProvider;
        _options = dbContextOptions;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableDetailedErrors(true);
        optionsBuilder.UseSqlServer(_settings.ConnectionString ?? string.Empty, s => s.CommandTimeout(60));
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var TemporaryAuditEntities = await AuditNonTemporaryProperties();

        var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

        AddedEntities.ForEach(E =>
        {
            if (E.Property("CreateUser").CurrentValue == null)
                E.Property("CreateUser").CurrentValue = _userProvider.UserName;
            E.Property("CreateTmsTmp").CurrentValue = DateTime.Now;
        });

        var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

        EditedEntities.ForEach(E =>
        {
            if (!E.Property("ModifiedUser").IsModified || E.Property("ModifiedUser").CurrentValue == null)
                E.Property("ModifiedUser").CurrentValue = _userProvider.UserName;
            E.Property("ModifiedTmsTmp").CurrentValue = DateTime.Now;
        });

        await AuditTemporaryProperties(TemporaryAuditEntities);

        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private async Task<IEnumerable<Tuple<EntityEntry, Audit>>> AuditNonTemporaryProperties()
    {
        ChangeTracker.DetectChanges();
        var entitiesToTrack = ChangeTracker.Entries().Where(e => e.Entity is not Audit && e.State != EntityState.Detached && e.State != EntityState.Unchanged);

        await Audits.AddRangeAsync(
            entitiesToTrack.Where(e => !e.Properties.Any(p => p.IsTemporary)).Select(e => new Audit()
            {
                TableName = e.Metadata.GetTableName() ?? string.Empty,
                Action = Enum.GetName(typeof(EntityState), e.State) ?? string.Empty,
                CreateTmsTmp = DateTime.Now.ToUniversalTime(),
                CreateUser = _userProvider.UserName,
                KeyValues = JsonSerializer.Serialize(e.Properties.Where(p => p.Metadata.IsPrimaryKey()).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty()),
                NewValues = JsonSerializer.Serialize(e.Properties.Where(p => e.State == EntityState.Added || e.State == EntityState.Modified).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty()),
                OldValues = JsonSerializer.Serialize(e.Properties.Where(p => e.State == EntityState.Deleted || e.State == EntityState.Modified).ToDictionary(p => p.Metadata.Name, p => p.OriginalValue).NullIfEmpty())
            }).ToList()
        );

        return entitiesToTrack.Where(e => e.Properties.Any(p => p.IsTemporary))
             .Select(e => new Tuple<EntityEntry, Audit>(
                 e,
             new Audit()
             {
                 TableName = e.Metadata.GetTableName() ?? string.Empty,
                 Action = Enum.GetName(typeof(EntityState), e.State) ?? string.Empty,
                 CreateTmsTmp = DateTime.Now.ToUniversalTime(),
                 CreateUser = _userProvider.UserName,
                 NewValues = JsonSerializer.Serialize(e.Properties.Where(p => !p.Metadata.IsPrimaryKey()).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty())
             }
             )).ToList();
    }

    private async Task AuditTemporaryProperties(IEnumerable<Tuple<EntityEntry, Audit>> temporatyEntities)
    {
        if (temporatyEntities != null && temporatyEntities.Any())
        {
            await Audits.AddRangeAsync(
            temporatyEntities.ForEach(t => t.Item2.KeyValues = JsonSerializer.Serialize(t.Item1.Properties.Where(p => p.Metadata.IsPrimaryKey()).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty()))
                .Select(t => t.Item2)
            );
        }
        await Task.CompletedTask;
    }
}
