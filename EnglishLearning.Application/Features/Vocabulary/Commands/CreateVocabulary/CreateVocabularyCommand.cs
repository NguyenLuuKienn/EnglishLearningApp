using EnglishLearning.Application.Common;
using EnglishLearning.Domain.Enums;
using MediatR;

namespace EnglishLearning.Application.Features.Vocabulary.Commands.CreateVocabulary;

public record CreateVocabularyCommand(
    string Word,
    string Definition,
    string? Example,
    string? PartOfSpeech,
    DifficultyLevel Difficulty
) : IRequest<Result<Guid>>;
