using Microsoft.EntityFrameworkCore;
using TUM.Domain.Models;
using TUM.Infrastructure.Data.Configurations;

namespace TUM.Infrastructure.Data;

public class MainDbContext : DbContext
{
    public DbSet<Bot> Bots { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BotUser> BotUsers { get; set; }
    
    public MainDbContext(DbContextOptions<MainDbContext> dbContextOptions) : base(dbContextOptions){ }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureBot();
        modelBuilder.ConfigureUser();
        base.OnModelCreating(modelBuilder);
    }
}