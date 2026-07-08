using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Queries.GetQuestion;

public record GetQuestionQuery(Guid QuizId, Guid QuestionId) : IRequest<QuestionDto>;
