namespace EnglishLearning.Application.DTOs;

public class ChoiceDto
{
    public Guid Id { get; set; }
    public string ChoiceText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
