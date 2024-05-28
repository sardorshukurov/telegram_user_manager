namespace TUM.Domain.Entities;

public class BotAdmin : IBaseEntity
{
    public Guid Id { get; set; }
    public Guid AdminId { get; set; }
    public Guid BotId { get; set; }
    public Bot Bot { get; set; } = null!;
    public User Admin { get; set; } = null!;
}