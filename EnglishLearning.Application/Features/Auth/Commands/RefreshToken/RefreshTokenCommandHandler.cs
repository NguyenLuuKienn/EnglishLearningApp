using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    ITokenService _tokenService,
    IUserRepository _userRepository,
    IUnitOfWork _unitOfWork) : IRequestHandler<RefreshTokenCommand, TokenDto>
{
    public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null)
            throw new UnauthorizedAccessException(AuthErrorMessages.InvalidAccessToken);

        var email = principal.FindFirst("email")?.Value;
        var user = await _userRepository.GetByEmailAsync(email!);
        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiry <= DateTime.UtcNow)
            throw new UnauthorizedAccessException(AuthErrorMessages.InvalidRefreshToken);

        var newTokens = await _tokenService.GenerateTokensAsync(user);

        user.RefreshToken = newTokens.RefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddHours(720);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newTokens;
    }
}
