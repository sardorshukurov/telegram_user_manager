namespace TUM.Domain.Entities;

public class User : IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public long UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public virtual ICollection<Bot> Bots { get; set; } = [];
    public virtual ICollection<BotUser> BotUsers { get; set; } = [];
}