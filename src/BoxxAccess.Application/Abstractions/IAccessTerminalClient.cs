using BoxxAccess.Domain.Entities;
using BoxxAccess.Domain.ValueObjects;

namespace BoxxAccess.Application.Abstractions;

public interface IAccessTerminalClient
{
    Task ConnectAsync(CancellationToken cancellationToken);

    Task<DeviceIdentity> GetIdentityAsync(CancellationToken cancellationToken);

    IAsyncEnumerable<AccessEvent> ListenForEventsAsync(CancellationToken cancellationToken);

    Task DisconnectAsync(CancellationToken cancellationToken);
}
