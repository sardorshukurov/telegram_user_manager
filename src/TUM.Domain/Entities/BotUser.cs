namespace TUM.Domain.Entities;

public class BotUser : IBaseEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BotId { get; set; }
    public Bot Bot { get; set; } = null!;
    public User User { get; set; } = null!;
    public bool IsBanned { get; set; } = false;
    public bool HasPremium { get; set; } = false;
}