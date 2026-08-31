using BoxxAccess.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoxxAccess.Infrastructure.Persistence.Configurations;

public sealed class AccessEventConfiguration : IEntityTypeConfiguration<AccessEvent>
{
    public void Configure(EntityTypeBuilder<AccessEvent> builder)
    {
        builder.ToTable("AccessEvents");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.DeviceSerialNumber).IsRequired().HasMaxLength(100);

        builder.HasIndex(e => e.SyncedToBoxxCentral);
        builder.HasIndex(e => e.OccurredAt);
    }
}
