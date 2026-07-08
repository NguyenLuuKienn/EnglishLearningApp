using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Auth.Commands.Login;
using EnglishLearning.Application.Features.Auth.Commands.Register;
using EnglishLearning.Application.Features.Auth.Commands.RefreshToken;
using EnglishLearning.Application.Features.Auth.Queries.GetProfile;
using EnglishLearning.Domain.Enums;
using EnglishLearning.WebAPI.Controllers;
using EnglishLearning.WebAPI.Models.Requests.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearnningApp.UnitTest.WebAPI.Controllers;

public class AuthControllerTests
{
    [Fact]
    public async Task Register_ValidRequest_ShouldReturnOkWithGuid()
    {
        var mediator = new Mock<IMediator>();
        var userId = Guid.NewGuid();
        mediator.Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(userId);

        var controller = new AuthController(mediator.Object);
        var request = new RegisterRequest
        {
            Username = "newuser",
            Email = "new@test.com",
            Password = "Password123!",
            Role = UserRole.Student
        };

        var result = await controller.Register(request);

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var response = okResult.Value.Should().BeOfType<EnglishLearning.WebAPI.Models.Common.ApiResponse<Guid>>().Subject;
        response.Success.Should().BeTrue();
        response.Data.Should().Be(userId);
    }

    [Fact]
    public async Task Login_ValidCredentials_ShouldReturnOkWithTokens()
    {
        var mediator = new Mock<IMediator>();
        var tokenDto = new TokenDto { AccessToken = "access", RefreshToken = "refresh", ExpiresIn = 3600 };
        mediator.Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tokenDto);

        var controller = new AuthController(mediator.Object);
        var request = new LoginRequest { Username = "testuser", Password = "Password123!" };

        var result = await controller.Login(request);

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var response = okResult.Value.Should().BeOfType<EnglishLearning.WebAPI.Models.Common.ApiResponse<TokenDto>>().Subject;
        response.Success.Should().BeTrue();
        response.Data!.AccessToken.Should().Be("access");
    }

    [Fact]
    public async Task RefreshToken_ValidRequest_ShouldReturnOkWithTokens()
    {
        var mediator = new Mock<IMediator>();
        var tokenDto = new TokenDto { AccessToken = "new-access", RefreshToken = "new-refresh", ExpiresIn = 3600 };
        mediator.Setup(m => m.Send(It.IsAny<RefreshTokenCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tokenDto);

        var controller = new AuthController(mediator.Object);
        var request = new RefreshTokenRequest { AccessToken = "old-access", RefreshToken = "old-refresh" };

        var result = await controller.RefreshToken(request);

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var response = okResult.Value.Should().BeOfType<EnglishLearning.WebAPI.Models.Common.ApiResponse<TokenDto>>().Subject;
        response.Success.Should().BeTrue();
    }

    [Fact]
    public async Task Profile_ShouldReturnOkWithUserDto()
    {
        var mediator = new Mock<IMediator>();
        var userDto = new UserDto { Id = Guid.NewGuid(), Username = "testuser", Email = "test@test.com" };
        mediator.Setup(m => m.Send(It.IsAny<GetProfileQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(userDto);

        var controller = new AuthController(mediator.Object);
        controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
        {
            HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext
            {
                User = new System.Security.Claims.ClaimsPrincipal(
                    new System.Security.Claims.ClaimsIdentity(new[]
                    {
                        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
                    }))
            }
        };

        var result = await controller.GetProfile();

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var response = okResult.Value.Should().BeOfType<EnglishLearning.WebAPI.Models.Common.ApiResponse<UserDto>>().Subject;
        response.Data!.Username.Should().Be("testuser");
    }
}
