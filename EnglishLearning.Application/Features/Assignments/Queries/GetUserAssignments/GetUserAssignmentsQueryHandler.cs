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
        if (!Guid.TryParse(request.UserId, out var userId))
            throw new ArgumentException("Invalid user ID format", nameof(request.UserId));

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException(AuthErrorMessages.UserNotFound);

        var allAssignments = await _assignmentRepository.GetAllWithQuizAsync();

        var now = DateTime.Now;
        var userIdStr = userId.ToString();
        var userAssignments = allAssignments
            .Where(a => a.Status != AssignmentStatus.Cancelled &&
                       (a.TargetUserId == userIdStr || a.TargetRole == user.Role))
            .OrderByDescending(a => a.StartTime)
            .ToList();

        // Compute effective status based on current time
        var dtos = _mapper.Map<List<QuizAssignmentDto>>(userAssignments);
        foreach (var dto in dtos)
        {
            if (dto.Status == AssignmentStatus.Scheduled && dto.StartTime <= now)
                dto.Status = AssignmentStatus.Active;
            else if (dto.Status == AssignmentStatus.Active && dto.EndTime < now)
                dto.Status = AssignmentStatus.Completed;
        }

        return dtos;
    }
}
