using Microsoft.Extensions.DependencyInjection;
using TUM.Application.Services.BotService;
using TUM.Infrastructure.Data;

namespace Application.FunctionalTests.Bots;

[TestFixture]
public class AddUserTests : BaseTextFixture
{
    [Test]
    public void AddUserToBot_Should_Add_User()
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var botService = scopedServices.GetRequiredService<IBotService>();
            var dbContext = scopedServices.GetRequiredService<MainDbContext>();
            
            
        }
    }
}