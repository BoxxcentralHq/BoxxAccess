using BoxxAccess.Application.DeviceDiagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace BoxxAccess.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDeviceConnectionProbe, DeviceConnectionProbe>();

        return services;
    }
}
