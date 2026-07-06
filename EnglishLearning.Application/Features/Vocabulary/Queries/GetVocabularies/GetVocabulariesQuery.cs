using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Enums;
using MediatR;

namespace EnglishLearning.Application.Features.Vocabulary.Queries.GetVocabularies;

public record GetVocabulariesQuery(
    int PageNumber,
    int PageSize,
    DifficultyLevel? Difficulty
) : IRequest<Result<PagedResult<VocabularyDto>>>;
