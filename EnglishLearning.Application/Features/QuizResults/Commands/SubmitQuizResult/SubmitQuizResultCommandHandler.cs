using AutoMapper;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.QuizResults.Commands.SubmitQuizResult;

public class SubmitQuizResultCommandHandler : IRequestHandler<SubmitQuizResultCommand, Result<QuizResultDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SubmitQuizResultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<QuizResultDto>> Handle(SubmitQuizResultCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.Quizzes.GetQuizWithQuestionsAsync(request.QuizId);
        if (quiz == null)
            return Result<QuizResultDto>.Failure(QuizErrorMessages.NotFound);

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

        var result = QuizResult.Create(
            request.QuizId,
            request.UserId,
            quiz.Questions.Count,
            correctAnswers,
            request.DurationMinutes
        );

        await _unitOfWork.QuizResults.AddAsync(result);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<QuizResultDto>.Success(_mapper.Map<QuizResultDto>(result));
    }
}
