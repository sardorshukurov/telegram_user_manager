using System.Collections;
using System.Linq.Expressions;
using TUM.Application.DTO;

namespace TUM.Application.Services.BotService;

public interface IBotService
{
    Task<BotDto?> GetAsync(Guid adminId, 
        Expression<Func<BotDto, bool>> filter);
    Task<IEnumerable<BotDto>> GetAllAsync(Guid adminId,
        Expression<Func<BotDto, bool>> filter);
    Task AddUserAsync(Guid botId, AddUserDto user, bool isAdmin);
    Task RemoveUserAsync(Guid botId, Guid userId, bool isAdmin);
    Task ChangeBanStatusAsync(Guid botId, Guid userId, bool ban);
}