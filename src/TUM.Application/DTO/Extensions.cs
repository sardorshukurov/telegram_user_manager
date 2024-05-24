using TUM.Domain.Entities;

namespace TUM.Application.DTO;

public static class Extensions
{
    public static BotDto AsDto(this Bot bot)
    {
        var users = bot.Users.Select(u => u.AsDto()).ToList();
        var admins = bot.Admins.Select(a => a.AsDto()).ToList();
        return new BotDto(bot.Id, bot.Name, bot.UserName, users, admins);
    }

    public static UserDto AsDto(this User user)
    {
        return new UserDto(user.Id, user.UserId, user.UserName);
    }

    public static User AsEntity(this AddUserDto dto)
    {
        return new User
        {
            UserId = dto.UserId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.UserName,
            PhoneNumber = dto.PhoneNumber
        };
    }
    public static Bot AsEntity(this CreateBotDto dto)
    {
        return new Bot
        {
            Name = dto.Name,
            UserName = dto.UserName
        };
    }
    
    public static Bot AsEntity(this UpdateBotDto dto)
    {
        return new Bot
        {
            Name = dto.Name,
            UserName = dto.UserName
        };
    }
}