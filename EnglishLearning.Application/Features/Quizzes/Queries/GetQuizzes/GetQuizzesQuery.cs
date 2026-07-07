using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Enums;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Queries.GetQuizzes;

public record GetQuizzesQuery(
    int PageNumber,
    int PageSize,
    DifficultyLevel? Difficulty
) : IRequest<PagedResult<QuizDto>>;
