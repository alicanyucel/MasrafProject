using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseCenterCards.GetAllExpenseCenterCard;

internal sealed class GetAllExpenseCenterCardQueryHandler : IRequestHandler<GetAllExpenseCenterCardQuery, Result<List<ExpenseCenterCard>>>
{
    private readonly IExpenseCenterCardRepository _expenseCenterCardRepository;

    public GetAllExpenseCenterCardQueryHandler(IExpenseCenterCardRepository customerRepository)
    {
        _expenseCenterCardRepository = customerRepository;
    }

    public async Task<Result<List<ExpenseCenterCard>>> Handle(GetAllExpenseCenterCardQuery request, CancellationToken cancellationToken)
    {
        var expenseCenterCard = await _expenseCenterCardRepository
            .GetAll()
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);
        return Result<List<ExpenseCenterCard>>.Succeed(expenseCenterCard);
    }
}