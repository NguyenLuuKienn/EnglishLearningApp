using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Queries.GetQuizForTake;

public class GetQuizForTakeQueryHandler(
    IQuizRepository _quizRepository) : IRequestHandler<GetQuizForTakeQuery, QuizForTakeDto>
{
    public async Task<QuizForTakeDto> Handle(GetQuizForTakeQuery request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepository.GetQuizWithQuestionsAsync(request.Id);
        if (quiz == null)
            throw new KeyNotFoundException(QuizErrorMessages.NotFound);

        var random = new Random();
        return new QuizForTakeDto
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Description = quiz.Description,
            Difficulty = quiz.Difficulty,
            TimeLimitMinutes = quiz.TimeLimitMinutes,
            PassingScore = quiz.PassingScore,
            Questions = quiz.Questions.Select(q => new QuestionForTakeDto
            {
                Id = q.Id,
                QuestionText = q.QuestionText,
                QuestionType = q.QuestionType,
                Choices = q.Choices
                    .OrderBy(_ => random.Next())
                    .Select(c => new ChoiceForTakeDto
                    {
                        Id = c.Id,
                        ChoiceText = c.ChoiceText
                    }).ToList()
            }).ToList()
        };
    }
}
