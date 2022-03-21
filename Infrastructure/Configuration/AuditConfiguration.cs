using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class AuditConfiguration : IEntityTypeConfiguration<Audit>
{
    public void Configure(EntityTypeBuilder<Audit> builder)
    {
        builder.ToTable("Audit", "log");
        builder.Property(e => e.Action)
            .HasMaxLength(15);
        builder.Property(e => e.TableName)
            .HasMaxLength(50);
        builder.Property(e => e.Id)
            .UseIdentityColumn()
            .IsRequired();
        builder.Property(e => e.CreateUser)
            .HasMaxLength(60)
            .IsRequired();
        builder.Property(e => e.CreateTmsTmp)
            .HasColumnType("DateTime")
            .IsRequired();
        builder.Property(e => e.ModifiedUser)
            .HasMaxLength(60);
        builder.Property(e => e.ModifiedTmsTmp)
            .HasColumnType("DateTime");
        builder.Property(e => e.RowGuid)
            .IsRowVersion()
            .IsRequired();
    }
}

