using BoxxAccess.Application.Abstractions;
using BoxxAccess.Domain.Entities;
using BoxxAccess.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BoxxAccess.Infrastructure.Queue;

public sealed class SqliteAccessEventQueue(BoxxAccessDbContext dbContext) : IAccessEventQueue
{
    public async Task<IReadOnlyList<AccessEvent>> GetPendingAsync(int maxCount, CancellationToken cancellationToken)
    {
        return await dbContext.AccessEvents
            .Where(e => !e.SyncedToBoxxCentral)
            .OrderBy(e => e.OccurredAt)
            .Take(maxCount)
            .ToListAsync(cancellationToken);
    }

    public async Task MarkSyncedAsync(Guid accessEventId, CancellationToken cancellationToken)
    {
        var accessEvent = await dbContext.AccessEvents.FirstOrDefaultAsync(e => e.Id == accessEventId, cancellationToken);
        accessEvent?.MarkSynced();
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
