using BoxxAccess.Application.Abstractions;
using BoxxAccess.Infrastructure.Persistence;
using BoxxAccess.Infrastructure.Persistence.Repositories;
using BoxxAccess.Infrastructure.Queue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoxxAccess.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("BoxxAccess") ?? "Data Source=boxxaccess.db";

        services.AddDbContext<BoxxAccessDbContext>(options => options.UseSqlite(connectionString));

        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IAccessEventStore, AccessEventStore>();
        services.AddScoped<IAccessEventQueue, SqliteAccessEventQueue>();

        return services;
    }
}
