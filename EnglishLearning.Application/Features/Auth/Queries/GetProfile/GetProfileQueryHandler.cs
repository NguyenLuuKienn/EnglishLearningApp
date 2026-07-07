using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Auth.Queries.GetProfile;

public class GetProfileQueryHandler(
    IUserRepository _userRepository) : IRequestHandler<GetProfileQuery, UserDto>
{
    public async Task<UserDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            throw new KeyNotFoundException(AuthErrorMessages.UserNotFound);

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            AvatarUrl = user.AvatarUrl,
            CreatedAt = user.CreatedAt
        };
    }
}
