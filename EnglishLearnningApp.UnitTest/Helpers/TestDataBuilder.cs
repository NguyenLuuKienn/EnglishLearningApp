using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;

namespace EnglishLearnningApp.UnitTest.Helpers;

public static class TestDataBuilder
{
    public static User CreateValidUser(string? username = null, UserRole role = UserRole.Student)
    {
        return new User
        {
            Username = username ?? "testuser",
            Email = "test@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"),
            Role = role,
            IsActive = true
        };
    }

    public static Vocabulary CreateValidVocabulary(string? word = null)
    {
        return new Vocabulary
        {
            Word = word ?? "Hello",
            Definition = "A greeting",
            Example = "Hello, how are you?",
            PartOfSpeech = "Interjection",
            Difficulty = DifficultyLevel.Beginner
        };
    }

    public static Quiz CreateValidQuiz(string? title = null)
    {
        return new Quiz
        {
            Title = title ?? "Test Quiz",
            Description = "A test quiz",
            Difficulty = DifficultyLevel.Beginner,
            TimeLimitMinutes = 30,
            PassingScore = 50m
        };
    }

    public static Quiz CreateQuizWithQuestions(int questionCount = 3)
    {
        var quiz = CreateValidQuiz();

        for (int i = 0; i < questionCount; i++)
        {
            quiz.Questions.Add(CreateQuestion(quiz.Id, i));
        }

        return quiz;
    }

    public static Question CreateQuestion(Guid? quizId = null, int index = 0)
    {
        var question = new Question
        {
            QuestionText = $"Question {index}",
            QuestionType = QuestionType.MultipleChoice,
            Difficulty = DifficultyLevel.Beginner,
            CorrectAnswer = "A",
            QuizId = quizId ?? Guid.NewGuid()
        };

        question.Choices.Add(new Choice { ChoiceText = "A", IsCorrect = true, QuestionId = question.Id });
        question.Choices.Add(new Choice { ChoiceText = "B", IsCorrect = false, QuestionId = question.Id });
        question.Choices.Add(new Choice { ChoiceText = "C", IsCorrect = false, QuestionId = question.Id });

        return question;
    }

    public static QuizResult CreateValidQuizResult(Guid? quizId = null, string? userId = null)
    {
        int totalQuestions = 10;
        int correctAnswers = 7;
        var score = totalQuestions > 0 ? (decimal)Math.Round((correctAnswers / (double)totalQuestions) * 100, 2) : 0m;
        return new QuizResult
        {
            QuizId = quizId ?? Guid.NewGuid(),
            UserId = userId ?? "test-user-id",
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            DurationMinutes = 15,
            Score = score
        };
    }

    public static QuizAssignment CreateValidAssignment(Guid? quizId = null, UserRole? targetRole = null)
    {
        return new QuizAssignment
        {
            QuizId = quizId ?? Guid.NewGuid(),
            TargetRole = targetRole ?? UserRole.Student,
            TargetUserId = null,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddDays(7),
            Status = AssignmentStatus.Scheduled
        };
    }

    public static Notification CreateValidNotification(string? userId = null)
    {
        return new Notification
        {
            UserId = userId ?? "test-user-id",
            Type = NotificationType.QuizAssigned,
            Title = "New Quiz",
            Message = "You have a new quiz assigned",
            Data = null
        };
    }

    public static LearningHistory CreateValidHistory(string? userId = null)
    {
        return new LearningHistory
        {
            UserId = userId ?? "test-user-id",
            ActionType = ActionType.CompleteQuiz,
            TargetId = Guid.NewGuid(),
            Details = "Completed quiz",
            Score = 85m
        };
    }

    public static Leaderboard CreateValidLeaderboard(string? userId = null)
    {
        return new Leaderboard
        {
            UserId = userId ?? "test-user-id",
            TotalScore = 0m,
            QuizzesCompleted = 0,
            AverageScore = 0m,
            Streak = 0,
            LastActiveDate = DateTime.UtcNow
        };
    }
}
