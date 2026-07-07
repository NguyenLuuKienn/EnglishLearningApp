using AutoMapper;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Queries.GetActiveAssignments;

public class GetActiveAssignmentsQueryHandler(
    IQuizAssignmentRepository _assignmentRepository,
    IMapper _mapper) : IRequestHandler<GetActiveAssignmentsQuery, List<QuizAssignmentDto>>
{
    public async Task<List<QuizAssignmentDto>> Handle(GetActiveAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var allAssignments = await _assignmentRepository.GetAllAsync();

        var activeAssignments = allAssignments
            .Where(a => a.Status != AssignmentStatus.Cancelled &&
                       a.StartTime <= now && a.EndTime >= now)
            .ToList();

        return _mapper.Map<List<QuizAssignmentDto>>(activeAssignments);
    }
}
