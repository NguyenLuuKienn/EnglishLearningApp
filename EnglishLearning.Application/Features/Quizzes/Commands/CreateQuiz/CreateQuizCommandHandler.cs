using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Commands.CreateQuiz;

public class CreateQuizCommandHandler(IQuizRepository _quizRepository) : IRequestHandler<CreateQuizCommand, Guid>
{
    public async Task<Guid> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = new Quiz
        {
            Title = request.Title,
            Description = request.Description,
            Difficulty = request.Difficulty,
            TimeLimitMinutes = request.TimeLimitMinutes,
            PassingScore = request.PassingScore
        };

        foreach (var q in request.Questions ?? new List<QuestionCommand>())
        {
            var question = new Question
            {
                QuestionText = q.QuestionText,
                QuestionType = q.QuestionType,
                Difficulty = q.Difficulty,
                CorrectAnswer = q.CorrectAnswer,
                QuizId = quiz.Id
            };

            foreach (var c in q.Choices)
            {
                question.Choices.Add(new Choice
                {
                    ChoiceText = c.ChoiceText,
                    IsCorrect = c.IsCorrect,
                    QuestionId = question.Id
                });
            }

            quiz.Questions.Add(question);
        }

        await _quizRepository.AddAsync(quiz);
        await _quizRepository.SaveChangesAsync(cancellationToken);

        return quiz.Id;
    }
}
