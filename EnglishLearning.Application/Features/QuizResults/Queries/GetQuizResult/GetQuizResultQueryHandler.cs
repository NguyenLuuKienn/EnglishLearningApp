using AutoMapper;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.QuizResults.Queries.GetQuizResult;

public class GetQuizResultQueryHandler : IRequestHandler<GetQuizResultQuery, Result<QuizResultDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetQuizResultQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<QuizResultDto>> Handle(GetQuizResultQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.QuizResults.GetByIdAsync(request.Id);
        if (entity == null)
            return Result<QuizResultDto>.Failure(QuizResultErrorMessages.NotFound);

        return Result<QuizResultDto>.Success(_mapper.Map<QuizResultDto>(entity));
    }
}
