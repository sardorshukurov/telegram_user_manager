using System.Collections;
using System.Linq.Expressions;
using TUM.Application.DTO;

namespace TUM.Application.Services.BotService;

public interface IBotService
{
    Task<BotDto?> GetAsync(Guid adminId);
    Task<IEnumerable<BotDto>> GetAllAsync(Guid adminId);
    Task AddUserAsync(Guid adminId, Guid botId, AddUserDto user, bool isAdmin);
    Task RemoveUserAsync(Guid adminId, Guid botId, Guid userId, bool isAdmin);
    Task ChangeBanStatusAsync(Guid adminId, Guid botId, Guid userId, bool ban);
}