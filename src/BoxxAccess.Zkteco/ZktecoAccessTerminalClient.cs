using BoxxAccess.Application.Abstractions;
using BoxxAccess.Domain.Entities;
using BoxxAccess.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace BoxxAccess.Zkteco;

public sealed class ZktecoAccessTerminalClient(IOptions<ZktecoOptions> options) : IAccessTerminalClient
{
    private readonly ZktecoOptions _options = options.Value;

    public Task ConnectAsync(CancellationToken cancellationToken) => throw NotAvailable();

    public Task<DeviceIdentity> GetIdentityAsync(CancellationToken cancellationToken) => throw NotAvailable();

    public IAsyncEnumerable<AccessEvent> ListenForEventsAsync(CancellationToken cancellationToken) => throw NotAvailable();

    public Task DisconnectAsync(CancellationToken cancellationToken) => throw NotAvailable();

    private PlatformNotSupportedException NotAvailable() =>
        new($"The ZKTeco SDK is not installed for terminal {_options.Host}:{_options.Port}. Install it locally per docs/device-setup.md before using this adapter.");
}
