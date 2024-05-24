namespace TUM.Domain.Entities;

public class Bot : IBaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public virtual ICollection<User> Users { get; set; } = [];
    public virtual ICollection<BotUser> BotUsers { get; set; } = [];
    public virtual ICollection<User> Admins { get; set; } = [];
    public virtual ICollection<BotAdmin> BotAdmins { get; set; } = [];
}