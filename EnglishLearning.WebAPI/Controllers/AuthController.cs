using System.Security.Claims;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Auth.Commands.Login;
using EnglishLearning.Application.Features.Auth.Commands.RefreshToken;
using EnglishLearning.Application.Features.Auth.Commands.Register;
using EnglishLearning.Application.Features.Auth.Queries.GetProfile;
using EnglishLearning.WebAPI.Models.Common;
using EnglishLearning.WebAPI.Models.Requests.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator _mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var command = new RegisterCommand(request.Username, request.Email, request.Password, request.Role);
        var id = await _mediator.Send(command);

        return Ok(ApiResponse<Guid>.Ok(id, "Registration successful"));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand(request.Username, request.Password);
        var tokens = await _mediator.Send(command);

        return Ok(ApiResponse<TokenDto>.Ok(tokens));
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var command = new RefreshTokenCommand(request.AccessToken, request.RefreshToken);
        var tokens = await _mediator.Send(command);

        return Ok(ApiResponse<TokenDto>.Ok(tokens));
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var query = new GetProfileQuery(userId);
        var dto = await _mediator.Send(query);

        return Ok(ApiResponse<UserDto>.Ok(dto));
    }
}
