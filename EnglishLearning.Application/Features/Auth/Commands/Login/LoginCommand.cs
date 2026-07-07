using MediatR;
using EnglishLearning.Application.DTOs;

namespace EnglishLearning.Application.Features.Auth.Commands.Login;

public record LoginCommand(
    string Username,
    string Password) : IRequest<TokenDto>;
