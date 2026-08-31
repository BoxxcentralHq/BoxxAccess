using BoxxAccess.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoxxAccess.Infrastructure.Persistence.Configurations;

public sealed class DeviceProfileConfiguration : IEntityTypeConfiguration<DeviceProfile>
{
    public void Configure(EntityTypeBuilder<DeviceProfile> builder)
    {
        builder.ToTable("DeviceProfiles");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name).IsRequired().HasMaxLength(200);
        builder.Property(d => d.Host).IsRequired().HasMaxLength(200);
        builder.Property(d => d.SerialNumber).IsRequired().HasMaxLength(100);

        builder.HasIndex(d => d.SerialNumber).IsUnique();
    }
}
