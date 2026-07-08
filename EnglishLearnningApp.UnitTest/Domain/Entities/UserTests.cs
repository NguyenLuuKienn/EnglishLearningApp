using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;

namespace EnglishLearnningApp.UnitTest.Domain.Entities;

public class UserTests
{
    [Fact]
    public void Create_ShouldSetAllProperties()
    {
        var user = new User
        {
            Username = "testuser",
            Email = "test@example.com",
            PasswordHash = "hashed_password",
            Role = UserRole.Teacher,
            IsActive = true
        };

        user.Id.Should().NotBe(Guid.Empty);
        user.Username.Should().Be("testuser");
        user.Email.Should().Be("test@example.com");
        user.PasswordHash.Should().Be("hashed_password");
        user.Role.Should().Be(UserRole.Teacher);
        user.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveToFalse()
    {
        var user = new User
        {
            Username = "user",
            Email = "u@test.com",
            PasswordHash = "hash",
            Role = UserRole.Student,
            IsActive = true
        };
        user.IsActive = false;
        user.IsActive.Should().BeFalse();
    }

    [Fact]
    public void Activate_ShouldSetIsActiveToTrue()
    {
        var user = new User
        {
            Username = "user",
            Email = "u@test.com",
            PasswordHash = "hash",
            Role = UserRole.Student,
            IsActive = true
        };
        user.IsActive = false;
        user.IsActive = true;
        user.IsActive.Should().BeTrue();
    }
}
