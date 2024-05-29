using Microsoft.Extensions.DependencyInjection;
using TUM.Application.DTO;
using TUM.Application.Services.BotService;
using TUM.Domain.Entities;
using TUM.Infrastructure.Repository;

namespace Application.FunctionalTests.Bots;

[TestFixture]
public class AddBotTests : BaseTextFixture
{
    [Test]
    public void AddBot_Should_Add_Bot()
    {
        using var scope = ServiceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var botRepository = scopedServices.GetRequiredService<IRepository<Bot>>();
        var botService = scopedServices.GetRequiredService<IBotService>();

        var bot = new CreateBotDto("Pipisa", "PipisaBot", 53759504);
            
        // Act
        botService.AddBotAsync(bot);
            
        // Assert
        var addedBot = botRepository.GetOneAsync(b => b.UserName == "PipisaBot").Result;
        Assert.IsNotNull(addedBot);
        Assert.That(bot.UserName, Is.EqualTo(addedBot.UserName));
    }
    
    [Test]
    public void AddExistingBot_ShouldNot_Add_Bot()
    {
        using var scope = ServiceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var botRepository = scopedServices.GetRequiredService<IRepository<Bot>>();
        var botService = scopedServices.GetRequiredService<IBotService>();

        var bot = new CreateBotDto("Pipisa", "PipisaBot", 53759504);

        // Act
        botService.AddBotAsync(bot);
        botService.AddBotAsync(bot);

        // Assert
        var addedBot = botRepository.GetAllByFilterAsync(b => b.UserName == "PipisaBot").Result.ToList();
        Assert.IsNotNull(addedBot.Count == 1);
    }
}