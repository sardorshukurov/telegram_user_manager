using System.Linq.Expressions;
using TUM.Application.DTO;
using TUM.Domain.Entities;
using TUM.Infrastructure.Repository;

namespace TUM.Application.Services.BotService;

// TODO: add admin checks where needed (only admins of the bots can use the service
public class BotService : IBotService
{
    private readonly IRepository<Bot> _repository;
    private readonly IRepository<BotUser> _botUserRepository;
    
    public BotService(IRepository<Bot> repository, 
        IRepository<BotUser> botUserRepository)
    {
        _repository = repository;
        _botUserRepository = botUserRepository;
    }

    public async Task<BotDto?> GetAsync(Guid adminId, 
        Expression<Func<BotDto, bool>> filter)
    {
        var bot = await _repository.GetOneAsync(
            b => b.BotAdmins
            .Select(ba => ba.AdminId)
            .Contains(adminId));

        return bot?.AsDto();
    }

    public async Task<IEnumerable<BotDto>> GetAllAsync(Guid adminId, Expression<Func<BotDto, bool>> filter)
    {
        var bots = await _repository.GetAllByFilterAsync(
            b => b.BotAdmins
                .Select(ba => ba.AdminId)
                .Contains(adminId));

        return bots.Select(b => b.AsDto());
    }

    // TODO: instead of creating user here, use repository for users
    public async Task AddUserAsync(Guid botId, AddUserDto user, bool isAdmin)
    {
        var bot = await _repository.GetOneAsync(b => b.Id == botId);
        if (bot is null) return;
        
        if(isAdmin) bot.Admins.Add(user.AsEntity());
        else bot.Users.Add(user.AsEntity());
        
        await _repository.UpdateAsync(botId, bot);
    }

    public async Task RemoveUserAsync(Guid botId, Guid userId, bool isAdmin)
    {
        var bot = await _repository.GetOneAsync(b => b.Id == botId);
        if (bot is null) return;

        if (isAdmin)
        {
            var adminToRemove = bot.Admins.First(a => a.Id == userId);
            bot.Admins.Remove(adminToRemove);
        }
        else
        {
            var userToRemove = bot.Users.First(u => u.Id == userId);
            bot.Users.Remove(userToRemove);
        }
        
        await _repository.UpdateAsync(botId, bot);
    }

    public async Task ChangeBanStatusAsync(Guid botId, Guid userId, bool ban)
    {
        var bot = await _repository.GetOneAsync(b => b.Id == botId);
        if (bot is null) return;

        var botUser = await _botUserRepository.GetOneAsync(bu => 
            bu.BotId == botId && bu.UserId == userId);
        
        botUser!.IsBanned = ban;
        await _botUserRepository.UpdateAsync(botUser.Id, botUser);
    }
}