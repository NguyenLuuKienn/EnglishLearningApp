using AutoMapper;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.History.Commands.RecordHistory;
using EnglishLearning.Application.Features.Leaderboard.Commands.UpdateLeaderboard;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.QuizResults.Commands.SubmitQuizResult;

public class SubmitQuizResultCommandHandler(
    IQuizRepository _quizRepository,
    IQuizResultRepository _quizResultRepository,
    IMapper _mapper,
    IMediator _mediator) : IRequestHandler<SubmitQuizResultCommand, QuizResultDto>
{
    public async Task<QuizResultDto> Handle(SubmitQuizResultCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepository.GetQuizWithQuestionsAsync(request.QuizId);
        if (quiz == null)
            throw new KeyNotFoundException(QuizErrorMessages.NotFound);

        int correctAnswers = 0;

        foreach (var answer in request.Answers)
        {
            var question = quiz.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
            if (question == null) continue;

            if (question.QuestionType == QuestionType.MultipleChoice)
            {
                var correctChoice = question.Choices.FirstOrDefault(c => c.IsCorrect);
                if (correctChoice != null && answer.SelectedChoiceId == correctChoice.Id)
                    correctAnswers++;
            }
            else if (question.QuestionType == QuestionType.FillInBlank)
            {
                if (string.Equals(answer.AnswerText, question.CorrectAnswer, StringComparison.OrdinalIgnoreCase))
                    correctAnswers++;
            }
        }

        int totalQuestions = quiz.Questions.Count;
        var score = totalQuestions > 0 ? (decimal)Math.Round((correctAnswers / (double)totalQuestions) * 100, 2) : 0m;
        var result = new QuizResult
        {
            QuizId = request.QuizId,
            UserId = request.UserId,
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            DurationMinutes = request.DurationMinutes,
            Score = score
        };

        await _quizResultRepository.AddAsync(result);
        await _quizResultRepository.SaveChangesAsync(cancellationToken);

        // Record history
        await _mediator.Send(new RecordHistoryCommand(
            request.UserId,
            ActionType.CompleteQuiz,
            request.QuizId,
            $"Quiz completed with score {result.Score}%",
            result.Score), cancellationToken);

        // Update leaderboard
        await _mediator.Send(new UpdateLeaderboardCommand(
            request.UserId,
            result.Score), cancellationToken);

        return _mapper.Map<QuizResultDto>(result);
    }
}
