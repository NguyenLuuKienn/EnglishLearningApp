using AutoMapper;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.QuizResults.Queries.GetQuizResult;

public class GetQuizResultQueryHandler(
    IQuizResultRepository _quizResultRepository, 
    IMapper _mapper) : IRequestHandler<GetQuizResultQuery, QuizResultDto>
{
    public async Task<QuizResultDto> Handle(GetQuizResultQuery request, CancellationToken cancellationToken)
    {
        var entity = await _quizResultRepository.GetByIdAsync(request.Id);
        if (entity == null)
            throw new KeyNotFoundException(QuizResultErrorMessages.NotFound);

        return _mapper.Map<QuizResultDto>(entity);
    }
}
