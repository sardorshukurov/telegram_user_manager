namespace TUM.Application.DTO;

public record BotDto(Guid Id, string Name, string UserName, 
    ICollection<UserDto> Users, ICollection<UserDto> Admins);
public record UserDto(Guid Id, long UserId, string UserName, 
    DateTimeOffset LastTimeActive, DateTimeOffset AddedDate, 
    ICollection<string> BotUserNames);
public record AddUserDto(Guid Id, long UserId, string UserName,
    string FirstName, string LastName, string PhoneNumber);
public record CreateBotDto(string Name, string UserName);
public record UpdateBotDto(string Name, string UserName);