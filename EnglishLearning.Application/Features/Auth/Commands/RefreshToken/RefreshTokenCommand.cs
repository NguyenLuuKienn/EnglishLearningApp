using MediatR;
using EnglishLearning.Application.DTOs;

namespace EnglishLearning.Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(
    string AccessToken,
    string RefreshToken) : IRequest<TokenDto>;
