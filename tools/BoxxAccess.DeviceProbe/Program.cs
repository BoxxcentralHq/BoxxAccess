using BoxxAccess.Application.DependencyInjection;
using BoxxAccess.Application.DeviceDiagnostics;
using BoxxAccess.Zkteco.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
{
    Args = args,
    ContentRootPath = AppContext.BaseDirectory,
});

builder.Services.AddApplication();
builder.Services.AddZkteco(builder.Configuration);

using var host = builder.Build();

Console.WriteLine("BoxxAccess Device Probe - read-only connection test.");
Console.WriteLine("Connects, reads device identity, listens for one verification event, then disconnects.");
Console.WriteLine("Does not create users, edit access policies, enrol biometrics, or unlock doors.");
Console.WriteLine();

using var cancellationTokenSource = new CancellationTokenSource();
Console.CancelKeyPress += (_, args) =>
{
    args.Cancel = true;
    cancellationTokenSource.Cancel();
};

var deviceConnectionProbe = host.Services.GetRequiredService<IDeviceConnectionProbe>();

try
{
    var result = await deviceConnectionProbe.RunAsync(cancellationTokenSource.Token);

    Console.WriteLine($"Serial number:    {result.Identity.SerialNumber}");
    Console.WriteLine($"Firmware version: {result.Identity.FirmwareVersion}");
    Console.WriteLine($"SDK version:      {result.Identity.SdkVersion}");

    Console.WriteLine(result.FirstEvent is { } firstEvent
        ? $"Verification event: {firstEvent.VerificationMode} / {firstEvent.Result} at {firstEvent.OccurredAt}"
        : "No verification event was received before disconnecting.");
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Device probe failed: {ex.Message}");
    Environment.ExitCode = 1;
}
