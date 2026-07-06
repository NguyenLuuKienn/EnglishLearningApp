using EnglishLearning.Application.Common;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Commands.CreateQuiz;

public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateQuizCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = new Quiz
        {
            Title = request.Title,
            Description = request.Description,
            Difficulty = request.Difficulty,
            TimeLimitMinutes = request.TimeLimitMinutes,
            PassingScore = request.PassingScore
        };

        foreach (var q in request.Questions)
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

        await _unitOfWork.Quizzes.AddAsync(quiz);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(quiz.Id);
    }
}
