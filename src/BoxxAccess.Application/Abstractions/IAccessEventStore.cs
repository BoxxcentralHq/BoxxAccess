using BoxxAccess.Domain.Entities;

namespace BoxxAccess.Application.Abstractions;

public interface IAccessEventStore
{
    Task AddAsync(AccessEvent accessEvent, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
