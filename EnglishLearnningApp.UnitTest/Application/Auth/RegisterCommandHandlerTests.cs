using EnglishLearning.Application.Features.Auth.Commands.Register;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using EnglishLearnningApp.UnitTest.Helpers;
using UserEntity = EnglishLearning.Domain.Entities.User;

namespace EnglishLearnningApp.UnitTest.Application.Auth;

public class RegisterCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_ShouldRegisterUser()
    {
        var userRepo = new Mock<IUserRepository>();
        var uow = new Mock<IUnitOfWork>();
        userRepo.Setup(r => r.GetByUsernameAsync("newuser")).ReturnsAsync((UserEntity?)null);
        userRepo.Setup(r => r.GetByEmailAsync("new@test.com")).ReturnsAsync((UserEntity?)null);

        var handler = new RegisterCommandHandler(userRepo.Object, uow.Object);
        var command = new RegisterCommand("newuser", "new@test.com", "Password123!", UserRole.Student);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBe(Guid.Empty);
        userRepo.Verify(r => r.AddAsync(It.IsAny<UserEntity>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_ExistingUsername_ShouldThrowInvalidOperationException()
    {
        var userRepo = new Mock<IUserRepository>();
        var uow = new Mock<IUnitOfWork>();
        userRepo.Setup(r => r.GetByUsernameAsync("existing")).ReturnsAsync(TestDataBuilder.CreateValidUser("existing"));

        var handler = new RegisterCommandHandler(userRepo.Object, uow.Object);
        var command = new RegisterCommand("existing", "new@test.com", "Password123!", UserRole.Student);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
        exception.Message.Should().Contain("already exists");
    }

    [Fact]
    public async Task Handle_ExistingEmail_ShouldThrowInvalidOperationException()
    {
        var userRepo = new Mock<IUserRepository>();
        var uow = new Mock<IUnitOfWork>();
        userRepo.Setup(r => r.GetByUsernameAsync("newuser")).ReturnsAsync((UserEntity?)null);
        userRepo.Setup(r => r.GetByEmailAsync("existing@test.com")).ReturnsAsync(TestDataBuilder.CreateValidUser());

        var handler = new RegisterCommandHandler(userRepo.Object, uow.Object);
        var command = new RegisterCommand("newuser", "existing@test.com", "Password123!", UserRole.Student);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
        exception.Message.Should().Contain("already exists");
    }
}
