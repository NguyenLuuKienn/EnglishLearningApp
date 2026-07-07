using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Commands.CancelAssignment;

public class CancelAssignmentCommandHandler(
    IQuizAssignmentRepository _assignmentRepository) : IRequestHandler<CancelAssignmentCommand>
{
    public async Task Handle(CancelAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(request.AssignmentId);
        if (assignment == null)
            throw new KeyNotFoundException(AssignmentErrorMessages.NotFound);

        if (assignment.Status == AssignmentStatus.Cancelled)
            throw new InvalidOperationException(AssignmentErrorMessages.AlreadyCancelled);

        assignment.Status = AssignmentStatus.Cancelled;
        _assignmentRepository.Update(assignment);
        await _assignmentRepository.SaveChangesAsync(cancellationToken);
    }
}
