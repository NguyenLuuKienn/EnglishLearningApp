using AutoMapper;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Queries.GetQuiz;

public class GetQuizQueryHandler(
    IQuizRepository _quizRepository, 
    IMapper _mapper) : IRequestHandler<GetQuizQuery, QuizDto>
{
    public async Task<QuizDto> Handle(GetQuizQuery request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepository.GetQuizWithQuestionsAsync(request.Id);
        if (quiz == null)
            throw new KeyNotFoundException(QuizErrorMessages.NotFound);

        return _mapper.Map<QuizDto>(quiz);
    }
}
