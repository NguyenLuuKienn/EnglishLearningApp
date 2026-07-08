using EnglishLearning.Domain.Enums;
using MediatR;

namespace EnglishLearning.Application.Features.Auth.Commands.Register;

public record RegisterCommand(
    string Username,
    string Email,
    string Password,
    UserRole Role) : IRequest<Guid>;
