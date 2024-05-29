using Microsoft.Extensions.DependencyInjection;
using TUM.Application.DTO;
using TUM.Application.Services.BotService;
using TUM.Application.Services.UserService;
using TUM.Domain.Entities;
using TUM.Infrastructure.Data;
using TUM.Infrastructure.Repository;

namespace Application.FunctionalTests.Bots;

[TestFixture]
public class AddUserTests : BaseTextFixture
{
    [Test]
    public void AddUserToBot_Should_Add_User()
    {
        using var scope = ServiceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var botRepository = scopedServices.GetRequiredService<IRepository<Bot>>();
        var botService = scopedServices.GetRequiredService<IBotService>();
        var userService = scopedServices.GetRequiredService<IUserService>();

        long adminId = 53759504;
        var bot = new CreateBotDto("Pipisa", "PipisaBot", adminId);
        var userGuid = Guid.NewGuid();
        var user = new AddUserDto(userGuid, 1, "testUserName",
            "testFirstName", "testLastName", "testPhoneNumber");

        botService.AddBotAsync(bot);
        var createdBot = botRepository.GetOneAsync(b => b.UserName == "PipisaBot").Result;
        
        // Act
        botService.AddUserAsync(adminId, createdBot!.Id, user, false);
        var users = userService.GetAllAsync(createdBot.Id).Result;

        // Assert
        Assert.That(users.Select(u => u.Id).Contains(userGuid));
    }
}