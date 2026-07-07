using MediatR;
using EnglishLearning.Application.DTOs;

namespace EnglishLearning.Application.Features.Auth.Queries.GetProfile;

public record GetProfileQuery(Guid UserId) : IRequest<UserDto>;
