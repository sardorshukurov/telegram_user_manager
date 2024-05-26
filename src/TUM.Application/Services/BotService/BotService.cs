using System.Linq.Expressions;
using TUM.Application.DTO;
using TUM.Domain.Entities;
using TUM.Infrastructure.Repository;

namespace TUM.Application.Services.BotService;

public class BotService : IBotService
{
    private readonly IRepository<Bot> _repository;
    private readonly IRepository<BotUser> _botUserRepository;
    private readonly IRepository<BotAdmin> _botAdminRepository;
    private readonly IRepository<User> _userRepository;

    public BotService(IRepository<Bot> repository, 
        IRepository<BotUser> botUserRepository, 
        IRepository<BotAdmin> botAdminRepository, 
        IRepository<User> userRepository)
    {
        _repository = repository;
        _botUserRepository = botUserRepository;
        _botAdminRepository = botAdminRepository;
        _userRepository = userRepository;
    }

    public async Task<BotDto?> GetAsync(Guid adminId)
    {
        var bot = await _repository.GetOneAsync(
            b => b.BotAdmins
            .Select(ba => ba.AdminId)
            .Contains(adminId));

        return bot?.AsDto();
    }

    public async Task<IEnumerable<BotDto>> GetAllAsync(Guid adminId)
    {
        var bots = await _repository.GetAllByFilterAsync(
            b => b.BotAdmins
                .Select(ba => ba.AdminId)
                .Contains(adminId));

        return bots.Select(b => b.AsDto());
    }

    public async Task AddUserAsync(Guid adminId, Guid botId, AddUserDto user, bool isAdmin)
    {
        if (!await IsEligible(botId, adminId)) return;
        
        var bot = await _repository.GetOneAsync(b => b.Id == botId);
        if (bot is null) return;

        bool userExists = await _userRepository.GetOneAsync(u => u.UserId == user.UserId) is not null;
        if (!userExists)
            await _userRepository.CreateAsync(user.AsEntity());

        var userToAdd = (await _userRepository.GetOneAsync(u => u.UserId == user.UserId))!;
        
        if(isAdmin) bot.Admins.Add(userToAdd);
        else bot.Users.Add(userToAdd);
        
        await _repository.UpdateAsync(botId, bot);
    }

    public async Task RemoveUserAsync(Guid adminId, Guid botId, Guid userId, bool isAdmin)
    {
        if (!await IsEligible(botId, adminId)) return;
        
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

    public async Task ChangeBanStatusAsync(Guid adminId, Guid botId, Guid userId, bool ban)
    {
        if (!await IsEligible(botId, adminId)) return;
        
        var bot = await _repository.GetOneAsync(b => b.Id == botId);
        if (bot is null) return;

        var botUser = await _botUserRepository.GetOneAsync(bu => 
            bu.BotId == botId && bu.UserId == userId);
        
        botUser!.IsBanned = ban;
        await _botUserRepository.UpdateAsync(botUser.Id, botUser);
    }

    private async Task<bool> IsEligible(Guid botId, Guid userId)
    {
        var botAdmin = await _botAdminRepository
            .GetAllByFilterAsync(ba => ba.BotId == botId);
        return botAdmin.Select(ba => ba.AdminId).Contains(userId);
    }
}