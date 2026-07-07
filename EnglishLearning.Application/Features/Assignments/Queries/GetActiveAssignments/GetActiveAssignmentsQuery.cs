using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Queries.GetActiveAssignments;

public record GetActiveAssignmentsQuery : IRequest<List<QuizAssignmentDto>>;
