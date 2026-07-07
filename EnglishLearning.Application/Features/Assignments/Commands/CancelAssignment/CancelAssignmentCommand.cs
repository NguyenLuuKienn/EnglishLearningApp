using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Commands.CancelAssignment;

public record CancelAssignmentCommand(Guid AssignmentId) : IRequest;
