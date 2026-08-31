using BoxxAccess.Domain.Entities;

namespace BoxxAccess.Application.Abstractions;

public interface IAccessEventQueue
{
    Task<IReadOnlyList<AccessEvent>> GetPendingAsync(int maxCount, CancellationToken cancellationToken);

    Task MarkSyncedAsync(Guid accessEventId, CancellationToken cancellationToken);
}
