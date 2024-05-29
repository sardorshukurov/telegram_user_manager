using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TUM.Application.Services.BotService;
using TUM.Application.Services.UserService;
using TUM.Infrastructure;
using TUM.Infrastructure.Data;

namespace Application.FunctionalTests;

[TestFixture]
public abstract class BaseTextFixture
{
    protected ServiceProvider ServiceProvider;
    
    [SetUp]
    public void TestSetup()
    {
        var services = new ServiceCollection();

        // Using In-Memory database for testing
        services.AddDbContext<MainDbContext>(options =>
            options.UseInMemoryDatabase("TestDb"));

        services.AddRepositories();
        services.AddScoped<IBotService, BotService>();
        services.AddScoped<IUserService, UserService>();
        ServiceProvider = services.BuildServiceProvider();
    }
}