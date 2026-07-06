using EnglishLearning.Application.Common;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Commands.DeleteQuiz;

public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteQuizCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.Quizzes.GetByIdAsync(request.Id);
        if (quiz == null)
            return Result.Failure(QuizErrorMessages.NotFound);

        _unitOfWork.Quizzes.Delete(quiz);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
