using AutoMapper;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseCenterCards.GetByIdExpenseCenterCard;

public sealed class GetExpenseCenterCardByIdQueryHandler : IRequestHandler<GetExpenseCenterCardByIdQuery, Result<ExpenseCenterCard>>
{
    private readonly IExpenseCenterCardRepository _expenseCenterCardRepository;
    private readonly IMapper _mapper;

    public GetExpenseCenterCardByIdQueryHandler(IExpenseCenterCardRepository expenseCenterCardRepository, IMapper mapper)
    {
        _expenseCenterCardRepository = expenseCenterCardRepository;
        _mapper = mapper;
    }

    public async Task<Result<ExpenseCenterCard>> Handle(GetExpenseCenterCardByIdQuery request, CancellationToken cancellationToken)
    {
        var expenseCenterCardEntity = await _expenseCenterCardRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (expenseCenterCardEntity is null)
        return Result<ExpenseCenterCard>.Failure("Harcama merkezi kartı  bulunamadı veya silinmiş.");
        var expenseCenterCard = _mapper.Map<ExpenseCenterCard>(expenseCenterCardEntity);
        return Result<ExpenseCenterCard>.Succeed(expenseCenterCard);
    }
}