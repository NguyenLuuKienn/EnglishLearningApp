using MediatR;

namespace EnglishLearning.Application.Features.Vocabulary.Commands.DeleteVocabulary;

public record DeleteVocabularyCommand(Guid Id) : IRequest;
