using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs;

public class QuestionForTakeDto
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public QuestionType QuestionType { get; set; }
    public List<ChoiceForTakeDto>? Choices { get; set; }
}
