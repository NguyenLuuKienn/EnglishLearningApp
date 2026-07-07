using BCrypt.Net;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler(
    IUserRepository _userRepository, 
    IUnitOfWork _unitOfWork) : IRequestHandler<RegisterCommand, Guid>
{
    public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
        if (existingUser != null)
            throw new InvalidOperationException(AuthErrorMessages.UsernameExists);

        existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            throw new InvalidOperationException(AuthErrorMessages.EmailExists);

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = User.Create(request.Username, request.Email, passwordHash, UserRole.User);
        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
