using TUM.Application.DTO;

namespace TUM.Application.Services.UserService;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync(Guid botId);
    Task UpdateUserActivity(long userId);
}