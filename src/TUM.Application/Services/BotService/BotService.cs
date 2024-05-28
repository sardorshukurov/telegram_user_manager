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

    // public async Task<BotDto?> GetAsync(long adminId)
    // {
    //     var bot = await _repository.GetOneAsync(
    //         b => b.BotAdmins
    //         .Select(ba => ba.Admin.UserId)
    //         .Contains(adminId));
    //
    //     return bot?.AsDto();
    // }

    public async Task<IEnumerable<BotDto>> GetAllAsync(long adminId)
    {
        var bots = await _repository.GetAllByFilterAsync(
            b => b.BotAdmins
                .Select(ba => ba.Admin.UserId)
                .Contains(adminId),
            b => b.BotAdmins, b => b.BotUsers);

        return bots.Select(b => b.AsDto());
    }

    public async Task AddUserAsync(long adminId, Guid botId, AddUserDto user, bool isAdmin)
    {
        if (!await IsEligible(botId, adminId)) return;
        
        var bot = await _repository.GetOneAsync(b => b.Id == botId, 
            b => b.BotAdmins, b => b.BotUsers);
        if (bot is null) return;

        bool userExists = await _userRepository.GetOneAsync(u => u.UserId == user.UserId) is not null;
        if (!userExists)
            await _userRepository.CreateAsync(user.AsEntity());

        var userToAdd = (await _userRepository.GetOneAsync(u => u.UserId == user.UserId,
            u => u.BotUsers))!;

        if (isAdmin)
        {
            await _botAdminRepository.CreateAsync(new BotAdmin()
            {
                Id = Guid.NewGuid(),
                AdminId = userToAdd.Id,
                BotId = bot.Id,
                Bot = bot,
                Admin = userToAdd
            });
            await _botAdminRepository.SaveChangesAsync();
        }
        else
        {
            await _botUserRepository.CreateAsync(new BotUser()
            {
                Id = Guid.NewGuid(),
                UserId = userToAdd.Id,
                BotId = bot.Id,
                Bot = bot,
                User = userToAdd,
                IsBanned = false,
                HasPremium = false
            });
            await _botUserRepository.SaveChangesAsync();
        }
    }

    public async Task RemoveUserAsync(long adminId, Guid botId, long userId, bool isAdmin)
    {
        if (!await IsEligible(botId, adminId)) return;
        
        var bot = await _repository.GetOneAsync(b => b.Id == botId,
            b => b.BotAdmins, b => b.BotUsers);
        if (bot is null) return;

        if (isAdmin)
        {
            var adminToRemove = 
                (await _botAdminRepository.GetOneAsync(ba => ba.Admin.UserId == userId,
                    ba => ba.Admin, ba => ba.Bot))!;
            await _botAdminRepository.DeleteAsync(adminToRemove.Id);
            await _botAdminRepository.SaveChangesAsync();
        }
        else
        {
            var userToRemove = (await _botUserRepository.GetOneAsync(bu => bu.User.UserId == userId,
                bu => bu.User, bu => bu.Bot))!;
            await _botUserRepository.DeleteAsync(userToRemove.UserId);
            await _botUserRepository.SaveChangesAsync();
        }
    }

    public async Task ChangeBanStatusAsync(long adminId, Guid botId, long userId, bool ban)
    {
        if (!await IsEligible(botId, adminId)) return;
        
        var bot = await _repository.GetOneAsync(b => b.Id == botId,
            b => b.BotAdmins, b => b.BotUsers);
        if (bot is null) return;

        var botUser = (await _botUserRepository.GetOneAsync(bu => 
            bu.BotId == botId && bu.User.UserId == userId,
            bu => bu.User, bu => bu.Bot))!;
        
        botUser.IsBanned = ban;
        await _botUserRepository.UpdateAsync(botUser.Id, botUser);
        await _botUserRepository.SaveChangesAsync();
    }

    public async Task AddBotAsync(CreateBotDto bot)
    {
        if (await _repository.GetOneAsync(b => b.UserName == bot.UserName) is null)
        {
            await _repository.CreateAsync(bot.AsEntity());
            await _repository.SaveChangesAsync();
        }

        var addedBot = (await _repository.GetOneAsync(b => b.UserName == bot.UserName,
            b => b.BotAdmins, b => b.BotUsers))!; 

        var admin = await _userRepository.GetOneAsync(u => u.UserId == bot.AdminId);
        if (admin is null)
        {
            // TODO: fix user creation
            var user = new AddUserDto(Guid.NewGuid(),
                    bot.AdminId,
                string.Empty,
                $"admin of {bot.Name}",
                string.Empty,
                string.Empty);
            
            await _userRepository.CreateAsync(user.AsEntity());
            await _userRepository.SaveChangesAsync();
        }
        
            
        await _botAdminRepository.CreateAsync(new BotAdmin()
        {
            Id = Guid.NewGuid(),
            AdminId = admin!.Id,
            BotId = addedBot.Id
        });
        await _botAdminRepository.SaveChangesAsync();
    }

    private async Task<bool> IsEligible(Guid botId, long userId)
    {
        var botAdmin = await _botAdminRepository
            .GetAllByFilterAsync(ba => ba.BotId == botId,
                ba => ba.Admin,
                ba => ba.Bot);
        return botAdmin.Select(ba => ba.Admin.UserId).Contains(userId);
    }
}