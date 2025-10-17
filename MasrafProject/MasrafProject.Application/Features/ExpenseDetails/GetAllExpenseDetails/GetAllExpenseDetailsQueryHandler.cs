using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseDetails.GetAllExpenseDetails;

internal sealed class GetAllExpenseDetailQueryHandler : IRequestHandler<GetAllExpenseDetailsQuery, Result<List<ExpenseDetail>>>
{
    private readonly IExpenseDetailRepository _expenseDetailRepository;

    public GetAllExpenseDetailQueryHandler(IExpenseDetailRepository expenseDetailRepository)
    {
        _expenseDetailRepository = expenseDetailRepository;
    }

    public async Task<Result<List<ExpenseDetail>>> Handle(GetAllExpenseDetailsQuery request, CancellationToken cancellationToken)
    {
        var expenseDetail = await _expenseDetailRepository
            .GetAll()
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);
        return Result<List<ExpenseDetail>>.Succeed(expenseDetail);
    }
}
