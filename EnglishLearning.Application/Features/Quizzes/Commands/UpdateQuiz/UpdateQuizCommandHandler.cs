using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Commands.UpdateQuiz;

public class UpdateQuizCommandHandler(IQuizRepository _quizRepository) : IRequestHandler<UpdateQuizCommand, Guid>
{
    public async Task<Guid> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepository.GetByIdAsync(request.Id);
        if (quiz == null)
            throw new KeyNotFoundException(QuizErrorMessages.NotFound);

        quiz.Title = request.Title;
        quiz.Description = request.Description;
        quiz.Difficulty = request.Difficulty;
        quiz.TimeLimitMinutes = request.TimeLimitMinutes;
        quiz.PassingScore = request.PassingScore;
        quiz.UpdatedAt = DateTime.UtcNow;

        _quizRepository.Update(quiz);
        await _quizRepository.SaveChangesAsync(cancellationToken);

        return quiz.Id;
    }
}
