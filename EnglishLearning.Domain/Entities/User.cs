using EnglishLearning.Domain.Common;
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public UserRole Role { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public static User Create(string username, string email, string passwordHash, UserRole role = UserRole.User)
    {
        return new User
        {
            Username = username,
            Email = email,
            PasswordHash = passwordHash,
            Role = role,
            IsActive = true
        };
    }
}
