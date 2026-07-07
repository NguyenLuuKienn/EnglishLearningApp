using AutoMapper;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Queries.GetUserAssignments;

public class GetUserAssignmentsQueryHandler(
    IQuizAssignmentRepository _assignmentRepository,
    IUserRepository _userRepository,
    IMapper _mapper) : IRequestHandler<GetUserAssignmentsQuery, List<QuizAssignmentDto>>
{
    public async Task<List<QuizAssignmentDto>> Handle(GetUserAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId));
        if (user == null)
            throw new KeyNotFoundException(AuthErrorMessages.UserNotFound);

        var allAssignments = await _assignmentRepository.GetAllAsync();

        var userAssignments = allAssignments
            .Where(a => a.Status != AssignmentStatus.Cancelled &&
                       (a.TargetUserId == request.UserId || a.TargetRole == user.Role))
            .OrderByDescending(a => a.StartTime)
            .ToList();

        return _mapper.Map<List<QuizAssignmentDto>>(userAssignments);
    }
}
