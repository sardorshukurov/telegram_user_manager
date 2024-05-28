using Microsoft.EntityFrameworkCore;
using TUM.Domain.Entities;

namespace TUM.Infrastructure.Data.Configurations;

public static class BotConfiguration
{
    public static void ConfigureBot(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bot>()
            .HasMany(b => b.BotAdmins)
            .WithOne(ba => ba.Bot)
            .HasForeignKey(ba => ba.BotId);

        modelBuilder.Entity<Bot>()
            .HasMany(b => b.BotUsers)
            .WithOne(bu => bu.Bot)
            .HasForeignKey(bu => bu.BotId);

        modelBuilder.Entity<BotAdmin>()
            .HasKey(ba => new { ba.AdminId, ba.BotId });

        modelBuilder.Entity<BotAdmin>()
            .ToTable("bots_admins");

        modelBuilder.Entity<BotUser>()
            .HasKey(bu => new { bu.UserId, bu.BotId });

        modelBuilder.Entity<BotUser>()
            .ToTable("bots_users");

        modelBuilder.Entity<Bot>()
            .ToTable("bots");
    }
}