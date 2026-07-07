using BCrypt.Net;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler(
    IUserRepository _userRepository, 
    ITokenService _tokenService, 
    IUnitOfWork _unitOfWork) : IRequestHandler<LoginCommand, TokenDto>
{
    public async Task<TokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException(AuthErrorMessages.InvalidCredentials);

        if (!user.IsActive)
            throw new InvalidOperationException(AuthErrorMessages.AccountDeactivated);

        var tokens = await _tokenService.GenerateTokensAsync(user);

        user.RefreshToken = tokens.RefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddHours(720);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return tokens;
    }
}
