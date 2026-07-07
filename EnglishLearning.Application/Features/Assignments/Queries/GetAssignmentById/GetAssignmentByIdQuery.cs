using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Queries.GetAssignmentById;

public record GetAssignmentByIdQuery(Guid AssignmentId) : IRequest<QuizAssignmentDto>;
