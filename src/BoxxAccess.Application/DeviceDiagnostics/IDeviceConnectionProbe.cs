namespace BoxxAccess.Application.DeviceDiagnostics;

public interface IDeviceConnectionProbe
{
    Task<DeviceProbeResult> RunAsync(CancellationToken cancellationToken);
}
