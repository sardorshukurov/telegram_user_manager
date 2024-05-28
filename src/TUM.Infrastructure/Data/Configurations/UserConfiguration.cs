using Microsoft.EntityFrameworkCore;
using TUM.Domain.Entities;

namespace TUM.Infrastructure.Data.Configurations;

public static class UserConfiguration
{
    public static void ConfigureUser(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.BotUsers)
            .WithOne(bu => bu.User)
            .HasForeignKey(bu => bu.UserId);
        
        modelBuilder.Entity<User>().ToTable("users");
    }
}