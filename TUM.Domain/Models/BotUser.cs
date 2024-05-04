namespace TUM.Domain.Models;

public class BotUser
{
    public Guid UserId { get; set; }
    public Guid BotId { get; set; }
    public virtual Bot Bot { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public bool IsBanned { get; set; } = false;
    public bool HasPremium { get; set; } = false;
}