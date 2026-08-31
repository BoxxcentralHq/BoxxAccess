using BoxxAccess.Application.Abstractions;
using BoxxAccess.Domain.Entities;

namespace BoxxAccess.Infrastructure.Persistence.Repositories;

public sealed class AccessEventStore(BoxxAccessDbContext dbContext) : IAccessEventStore
{
    public async Task AddAsync(AccessEvent accessEvent, CancellationToken cancellationToken) =>
        await dbContext.AccessEvents.AddAsync(accessEvent, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        dbContext.SaveChangesAsync(cancellationToken);
}
