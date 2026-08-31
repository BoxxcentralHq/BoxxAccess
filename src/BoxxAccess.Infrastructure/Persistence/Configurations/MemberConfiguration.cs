using BoxxAccess.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoxxAccess.Infrastructure.Persistence.Configurations;

public sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.FullName).IsRequired().HasMaxLength(200);
        builder.Property(m => m.ExternalReferenceId).HasMaxLength(100);
        builder.Property(m => m.DeviceUserId).HasMaxLength(100);

        builder.HasIndex(m => m.DeviceUserId)
            .IsUnique()
            .HasFilter("\"DeviceUserId\" IS NOT NULL");
    }
}
