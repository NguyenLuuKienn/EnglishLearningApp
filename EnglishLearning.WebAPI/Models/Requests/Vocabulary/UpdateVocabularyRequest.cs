using EnglishLearning.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Vocabulary;

public class UpdateVocabularyRequest
{
    [Required]
    [StringLength(200)]
    public string Word { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Definition { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Example { get; set; }

    [StringLength(50)]
    public string? PartOfSpeech { get; set; }

    public DifficultyLevel Difficulty { get; set; }
}
