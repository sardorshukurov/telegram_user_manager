namespace TUM.Application;

public record BotDto(Guid Id, string Name, string UserName, 
    ICollection<UserDto> Users, ICollection<UserDto> Admins);
public record UserDto(Guid Id, long UserId, string UserName);