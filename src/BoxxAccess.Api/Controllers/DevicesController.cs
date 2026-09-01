using BoxxAccess.Application.DeviceDiagnostics;
using BoxxAccess.Contracts.Devices;
using Microsoft.AspNetCore.Mvc;

namespace BoxxAccess.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class DevicesController(IDeviceConnectionProbe deviceConnectionProbe) : ControllerBase
{
    [HttpGet("status")]
    public async Task<ActionResult<DeviceStatusResponse>> GetStatus(CancellationToken cancellationToken)
    {
        var probeResult = await deviceConnectionProbe.RunAsync(cancellationToken);

        var lastEvent = probeResult.FirstEvent is null
            ? null
            : new DeviceStatusEventResponse(
                probeResult.FirstEvent.VerificationMode.ToString(),
                probeResult.FirstEvent.Result.ToString(),
                probeResult.FirstEvent.OccurredAt);

        return Ok(new DeviceStatusResponse(
            probeResult.Identity.SerialNumber,
            probeResult.Identity.FirmwareVersion,
            probeResult.Identity.SdkVersion,
            lastEvent));
    }
}
