using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Commands.AssignQuiz;

public class AssignQuizCommandHandler(
    IQuizAssignmentRepository _assignmentRepository,
    IQuizRepository _quizRepository) : IRequestHandler<AssignQuizCommand, Guid>
{
    public async Task<Guid> Handle(AssignQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepository.GetByIdAsync(request.QuizId);
        if (quiz == null)
            throw new KeyNotFoundException(CommonErrorMessages.ResourceNotFound);

        if (request.TargetRole == null && string.IsNullOrEmpty(request.TargetUserId))
            throw new ArgumentException(AssignmentErrorMessages.TargetRequired);

        var assignment = new QuizAssignment
        {
            QuizId = request.QuizId,
            TargetRole = request.TargetRole,
            TargetUserId = request.TargetUserId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Status = AssignmentStatus.Scheduled
        };

        await _assignmentRepository.AddAsync(assignment);
        await _assignmentRepository.SaveChangesAsync(cancellationToken);

        return assignment.Id;
    }
}
