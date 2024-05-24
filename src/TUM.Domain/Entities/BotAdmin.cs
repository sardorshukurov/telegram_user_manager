namespace TUM.Domain.Entities;

public class BotAdmin
{
    public Guid AdminId { get; set; }
    public Guid BotId { get; set; }
    public virtual Bot Bot { get; set; } = null!;
    public virtual User Admin { get; set; } = null!;
}