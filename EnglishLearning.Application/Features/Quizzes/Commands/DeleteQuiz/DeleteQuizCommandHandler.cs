using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Commands.DeleteQuiz;

public class DeleteQuizCommandHandler(IQuizRepository _quizRepository) : IRequestHandler<DeleteQuizCommand>
{
    public async Task Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepository.GetByIdAsync(request.Id);
        if (quiz == null)
            throw new KeyNotFoundException(QuizErrorMessages.NotFound);

        _quizRepository.Delete(quiz);
        await _quizRepository.SaveChangesAsync(cancellationToken);
    }
}
