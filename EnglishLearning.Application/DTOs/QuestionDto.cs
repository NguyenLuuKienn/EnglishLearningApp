using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs;

public class QuestionDto
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public QuestionType QuestionType { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public List<ChoiceDto>? Choices { get; set; }
}
