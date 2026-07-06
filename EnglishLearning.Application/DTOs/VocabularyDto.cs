using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs;

public class VocabularyDto
{
    public Guid Id { get; set; }
    public string Word { get; set; } = string.Empty;
    public string Definition { get; set; } = string.Empty;
    public string? Example { get; set; }
    public string? PartOfSpeech { get; set; }
    public DifficultyLevel Difficulty { get; set; }
}
