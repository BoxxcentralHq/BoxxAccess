using BoxxAccess.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoxxAccess.Infrastructure.Persistence;

public sealed class BoxxAccessDbContext(DbContextOptions<BoxxAccessDbContext> options) : DbContext(options)
{
    public DbSet<Member> Members => Set<Member>();
    public DbSet<AccessEvent> AccessEvents => Set<AccessEvent>();
    public DbSet<DeviceProfile> DeviceProfiles => Set<DeviceProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoxxAccessDbContext).Assembly);
    }
}
