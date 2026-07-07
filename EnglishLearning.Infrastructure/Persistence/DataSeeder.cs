using BCrypt.Net;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.Infrastructure.Persistence;

public static class DataSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        // Seed Admin User
        var adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123");

        builder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Username = "admin",
                Email = "admin@englishlearning.com",
                PasswordHash = adminPasswordHash,
                Role = UserRole.Admin,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
