using EnglishLearning.Application.Common;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Commands.UpdateQuiz;

public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateQuizCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.Quizzes.GetByIdAsync(request.Id);
        if (quiz == null)
            return Result<Guid>.Failure(QuizErrorMessages.NotFound);

        quiz.Title = request.Title;
        quiz.Description = request.Description;
        quiz.Difficulty = request.Difficulty;
        quiz.TimeLimitMinutes = request.TimeLimitMinutes;
        quiz.PassingScore = request.PassingScore;
        quiz.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Quizzes.Update(quiz);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(quiz.Id);
    }
}
