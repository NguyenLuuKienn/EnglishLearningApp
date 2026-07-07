using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Queries.GetQuiz;

public record GetQuizQuery(Guid Id) : IRequest<QuizDto>;
