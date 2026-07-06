using AutoMapper;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Queries.GetQuiz;

public class GetQuizQueryHandler : IRequestHandler<GetQuizQuery, Result<QuizDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetQuizQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<QuizDto>> Handle(GetQuizQuery request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.Quizzes.GetQuizWithQuestionsAsync(request.Id);
        if (quiz == null)
            return Result<QuizDto>.Failure(QuizErrorMessages.NotFound);

        return Result<QuizDto>.Success(_mapper.Map<QuizDto>(quiz));
    }
}
