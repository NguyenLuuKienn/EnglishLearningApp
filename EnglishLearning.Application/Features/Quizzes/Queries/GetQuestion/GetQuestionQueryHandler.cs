using AutoMapper;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Queries.GetQuestion;

public class GetQuestionQueryHandler(
    IQuizRepository _quizRepository,
    IMapper _mapper) : IRequestHandler<GetQuestionQuery, QuestionDto>
{
    public async Task<QuestionDto> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepository.GetQuizWithQuestionsAsync(request.QuizId);
        if (quiz == null)
            throw new KeyNotFoundException(QuizErrorMessages.NotFound);

        var question = quiz.Questions.FirstOrDefault(q => q.Id == request.QuestionId);
        if (question == null)
            throw new KeyNotFoundException(QuestionErrorMessages.NotFound);

        return _mapper.Map<QuestionDto>(question);
    }
}
