using System.Collections;
using System.Linq.Expressions;
using TUM.Application.DTO;

namespace TUM.Application.Services.BotService;

public interface IBotService
{
    Task<BotDto?> GetAsync(long adminId);
    Task<IEnumerable<BotDto>> GetAllAsync(long adminId);
    Task AddUserAsync(long adminId, Guid botId, AddUserDto user, bool isAdmin);
    Task RemoveUserAsync(long adminId, Guid botId, long userId, bool isAdmin);
    Task ChangeBanStatusAsync(long adminId, Guid botId, long userId, bool ban);
}