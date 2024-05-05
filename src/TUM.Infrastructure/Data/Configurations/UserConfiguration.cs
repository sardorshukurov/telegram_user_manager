using Microsoft.EntityFrameworkCore;
using TUM.Domain.Models;

namespace TUM.Infrastructure.Data.Configurations;

public static class UserConfiguration
{
    public static void ConfigureUser(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");
    }
}