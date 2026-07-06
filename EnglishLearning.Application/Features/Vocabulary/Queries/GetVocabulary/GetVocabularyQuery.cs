using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Vocabulary.Queries.GetVocabulary;

public record GetVocabularyQuery(Guid Id) : IRequest<Result<VocabularyDto>>;
