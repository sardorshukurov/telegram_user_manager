using Microsoft.EntityFrameworkCore;
using TUM.Domain.Models;

namespace TUM.Infrastructure.Data.Configurations;

public static class BotConfiguration
{
    public static void ConfigureBot(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bot>()
            .HasMany(b => b.Admins)
            .WithMany(b => b.Bots)
            .UsingEntity<BotAdmin>(
                j =>
                {
                    j.HasKey(ba => new { ba.AdminId, ba.BotId });
                    j.ToTable("bots_admins");
                })
            .HasMany(b => b.Users)
            .WithMany(u => u.Bots)
            .UsingEntity<BotUser>(
                j =>
                {
                    j.HasKey(bu => new { bu.UserId, bu.BotId });
                    j.ToTable("bots_users");
                })
            .ToTable("bots");
    }
}