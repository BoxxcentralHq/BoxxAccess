using BoxxAccess.Application.Abstractions;
using BoxxAccess.Domain.Entities;

namespace BoxxAccess.Application.DeviceDiagnostics;

public sealed class DeviceConnectionProbe(IAccessTerminalClient terminalClient) : IDeviceConnectionProbe
{
    public async Task<DeviceProbeResult> RunAsync(CancellationToken cancellationToken)
    {
        await terminalClient.ConnectAsync(cancellationToken);

        try
        {
            var identity = await terminalClient.GetIdentityAsync(cancellationToken);

            AccessEvent? firstEvent = null;
            await foreach (var accessEvent in terminalClient.ListenForEventsAsync(cancellationToken))
            {
                firstEvent = accessEvent;
                break;
            }

            return new DeviceProbeResult(identity, firstEvent);
        }
        finally
        {
            await terminalClient.DisconnectAsync(cancellationToken);
        }
    }
}
