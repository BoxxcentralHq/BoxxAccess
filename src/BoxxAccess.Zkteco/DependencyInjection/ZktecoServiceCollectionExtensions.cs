using BoxxAccess.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoxxAccess.Zkteco.DependencyInjection;

public static class ZktecoServiceCollectionExtensions
{
    public static IServiceCollection AddZkteco(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ZktecoOptions>()
            .Bind(configuration.GetSection(ZktecoOptions.SectionName));

        services.AddScoped<IAccessTerminalClient, ZktecoAccessTerminalClient>();

        return services;
    }
}
