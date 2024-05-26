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
        services.AddTransient<IRepository<User>, Repository<User>>();
        services.AddTransient<IRepository<Bot>, Repository<Bot>>();
        services.AddTransient<IRepository<BotAdmin>, Repository<BotAdmin>>();
        services.AddTransient<IRepository<BotUser>, Repository<BotUser>>();

        return services;
    }
}