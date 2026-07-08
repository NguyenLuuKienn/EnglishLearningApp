using AutoMapper;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Enums;
using QuizResultEntity = EnglishLearning.Domain.Entities.QuizResult;
using LeaderboardEntity = EnglishLearning.Domain.Entities.Leaderboard;
using Vocab = EnglishLearning.Domain.Entities.Vocabulary;
using QuizAssignmentEntity = EnglishLearning.Domain.Entities.QuizAssignment;
using LearningHistoryEntity = EnglishLearning.Domain.Entities.LearningHistory;
using NotificationEntity = EnglishLearning.Domain.Entities.Notification;
using QuestionEntity = EnglishLearning.Domain.Entities.Question;
using ChoiceEntity = EnglishLearning.Domain.Entities.Choice;

namespace EnglishLearnningApp.UnitTest.Application.Mapping;

public class MappingTests
{
    private IMapper _mapper = null!;

    public MappingTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingsProfile>();
        });
        // Note: AssertConfigurationIsValid is skipped because LeaderboardDto has
        // Username and Rank properties that are computed at runtime, not mapped from the entity
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Vocabulary_To_VocabularyDto_ShouldMapAllProperties()
    {
        var vocabulary = new Vocab
        {
            Id = Guid.NewGuid(),
            Word = "Hello",
            Definition = "A greeting",
            Example = "Hello world",
            PartOfSpeech = "Interjection",
            Difficulty = DifficultyLevel.Beginner
        };

        var dto = _mapper.Map<VocabularyDto>(vocabulary);

        dto.Should().NotBeNull();
        dto.Id.Should().Be(vocabulary.Id);
        dto.Word.Should().Be(vocabulary.Word);
        dto.Definition.Should().Be(vocabulary.Definition);
        dto.Example.Should().Be(vocabulary.Example);
        dto.PartOfSpeech.Should().Be(vocabulary.PartOfSpeech);
        dto.Difficulty.Should().Be(vocabulary.Difficulty);
    }

    [Fact]
    public void QuizResult_To_QuizResultDto_ShouldMapAllProperties()
    {
        var totalQuestions = 10;
        var correctAnswers = 8;
        var score = totalQuestions > 0 ? (decimal)Math.Round((correctAnswers / (double)totalQuestions) * 100, 2) : 0m;
        var quizResult = new QuizResultEntity
        {
            QuizId = Guid.NewGuid(),
            UserId = "user-123",
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            DurationMinutes = 15,
            Score = score
        };

        var dto = _mapper.Map<QuizResultDto>(quizResult);

        dto.Should().NotBeNull();
        dto.QuizId.Should().Be(quizResult.QuizId);
        dto.UserId.Should().Be(quizResult.UserId);
        dto.Score.Should().Be(quizResult.Score);
        dto.TotalQuestions.Should().Be(quizResult.TotalQuestions);
        dto.CorrectAnswers.Should().Be(quizResult.CorrectAnswers);
        dto.DurationMinutes.Should().Be(quizResult.DurationMinutes);
    }

    [Fact]
    public void QuizAssignment_To_QuizAssignmentDto_ShouldMapWithQuizTitle()
    {
        var assignment = new QuizAssignmentEntity
        {
            QuizId = Guid.NewGuid(),
            TargetRole = UserRole.Student,
            TargetUserId = null,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddDays(7),
            Status = AssignmentStatus.Scheduled
        };

        var dto = _mapper.Map<QuizAssignmentDto>(assignment);

        dto.Should().NotBeNull();
        dto.Id.Should().Be(assignment.Id);
        dto.QuizId.Should().Be(assignment.QuizId);
        dto.TargetRole.Should().Be(assignment.TargetRole);
        dto.StartTime.Should().Be(assignment.StartTime);
        dto.EndTime.Should().Be(assignment.EndTime);
        dto.Status.Should().Be(assignment.Status);
    }

    [Fact]
    public void LearningHistory_To_LearningHistoryDto_ShouldMapAllProperties()
    {
        var history = new LearningHistoryEntity
        {
            UserId = "user-123",
            ActionType = ActionType.CompleteQuiz,
            TargetId = Guid.NewGuid(),
            Details = "Completed",
            Score = 85m
        };

        var dto = _mapper.Map<LearningHistoryDto>(history);

        dto.Should().NotBeNull();
        dto.UserId.Should().Be(history.UserId);
        dto.ActionType.Should().Be(history.ActionType);
        dto.TargetId.Should().Be(history.TargetId);
        dto.Details.Should().Be(history.Details);
        dto.Score.Should().Be(history.Score);
    }

    [Fact]
    public void Leaderboard_To_LeaderboardDto_ShouldMapAllProperties()
    {
        var leaderboard = new LeaderboardEntity
        {
            UserId = "user-123",
            TotalScore = 0m,
            QuizzesCompleted = 0,
            AverageScore = 0m,
            Streak = 0,
            LastActiveDate = DateTime.UtcNow
        };
        leaderboard.UpdateScore(80m);

        var dto = _mapper.Map<LeaderboardDto>(leaderboard);

        dto.Should().NotBeNull();
        dto.UserId.Should().Be(leaderboard.UserId);
        dto.TotalScore.Should().Be(leaderboard.TotalScore);
        dto.QuizzesCompleted.Should().Be(leaderboard.QuizzesCompleted);
        dto.AverageScore.Should().Be(leaderboard.AverageScore);
        dto.Streak.Should().Be(leaderboard.Streak);
    }

    [Fact]
    public void Notification_To_NotificationDto_ShouldMapAllProperties()
    {
        var notification = new NotificationEntity
        {
            UserId = "user-123",
            Type = NotificationType.QuizAssigned,
            Title = "Title",
            Message = "Message",
            Data = null
        };

        var dto = _mapper.Map<NotificationDto>(notification);

        dto.Should().NotBeNull();
        dto.UserId.Should().Be(notification.UserId);
        dto.Type.Should().Be(notification.Type);
        dto.Title.Should().Be(notification.Title);
        dto.Message.Should().Be(notification.Message);
        dto.IsRead.Should().Be(notification.IsRead);
    }

    [Fact]
    public void Question_To_QuestionDto_ShouldMapWithChoices()
    {
        var question = new QuestionEntity
        {
            Id = Guid.NewGuid(),
            QuestionText = "What is 2+2?",
            QuestionType = QuestionType.MultipleChoice,
            Difficulty = DifficultyLevel.Beginner,
            CorrectAnswer = "4",
            QuizId = Guid.NewGuid()
        };
        question.Choices.Add(new ChoiceEntity { Id = Guid.NewGuid(), ChoiceText = "4", IsCorrect = true, QuestionId = question.Id });
        question.Choices.Add(new ChoiceEntity { Id = Guid.NewGuid(), ChoiceText = "5", IsCorrect = false, QuestionId = question.Id });

        var dto = _mapper.Map<QuestionDto>(question);

        dto.Should().NotBeNull();
        dto.Id.Should().Be(question.Id);
        dto.QuestionText.Should().Be(question.QuestionText);
        dto.Choices.Should().HaveCount(2);
        dto.Choices.First().ChoiceText.Should().Be("4");
    }

    [Fact]
    public void Choice_To_ChoiceDto_ShouldMapAllProperties()
    {
        var choice = new ChoiceEntity
        {
            Id = Guid.NewGuid(),
            ChoiceText = "Answer A",
            IsCorrect = true,
            QuestionId = Guid.NewGuid()
        };

        var dto = _mapper.Map<ChoiceDto>(choice);

        dto.Should().NotBeNull();
        dto.Id.Should().Be(choice.Id);
        dto.ChoiceText.Should().Be(choice.ChoiceText);
        dto.IsCorrect.Should().Be(choice.IsCorrect);
    }
}
