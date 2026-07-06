using AutoMapper;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Application.Common;

public class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        // Vocabulary
        CreateMap<Vocabulary, VocabularyDto>();

        // Choice
        CreateMap<Choice, ChoiceDto>();

        // Question → QuestionDto (includes nested Choices)
        CreateMap<Question, QuestionDto>()
            .ForMember(dest => dest.Choices, opt => opt.MapFrom(src => src.Choices));

        // Quiz → QuizDto (includes nested Questions)
        CreateMap<Quiz, QuizDto>()
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));

        // QuizResult
        CreateMap<QuizResult, QuizResultDto>();
    }
}
