using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TUM.Domain.Entities;
using TUM.Infrastructure.Data;
using TUM.Infrastructure.Repository;

namespace TUM.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MainDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PgSQL")));
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<User>, Repository<User>>();
        services.AddScoped<IRepository<Bot>, Repository<Bot>>();
        services.AddScoped<IRepository<BotAdmin>, Repository<BotAdmin>>();
        services.AddScoped<IRepository<BotUser>, Repository<BotUser>>();

        return services;
    }
}