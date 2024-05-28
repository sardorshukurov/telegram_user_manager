using TUM.Application.DTO;
using TUM.Domain.Entities;
using TUM.Infrastructure.Repository;

namespace TUM.Application.Services.UserService;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;

    public UserService(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync(Guid botId)
    {
        var users= await _repository.GetAllByFilterAsync(
            a => a.BotUsers.Select(b => b.BotId).Contains(botId));

        return users.Select(u => u.AsDto());
    }

    public async Task UpdateUserActivity(long userId)
    {
        var user = (await _repository.GetOneAsync(u => u.UserId == userId))!;
        user.LastTimeActive = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(user.Id, user);
        await _repository.SaveChangesAsync();
    }
}