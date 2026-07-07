using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Queries.GetUserAssignments;

public record GetUserAssignmentsQuery(string UserId) : IRequest<List<QuizAssignmentDto>>;
