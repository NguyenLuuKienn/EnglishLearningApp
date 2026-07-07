using AutoMapper;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Queries.GetAssignmentById;

public class GetAssignmentByIdQueryHandler(
    IQuizAssignmentRepository _assignmentRepository,
    IMapper _mapper) : IRequestHandler<GetAssignmentByIdQuery, QuizAssignmentDto>
{
    public async Task<QuizAssignmentDto> Handle(GetAssignmentByIdQuery request, CancellationToken cancellationToken)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(request.AssignmentId);
        if (assignment == null)
            throw new KeyNotFoundException(AssignmentErrorMessages.NotFound);

        return _mapper.Map<QuizAssignmentDto>(assignment);
    }
}
