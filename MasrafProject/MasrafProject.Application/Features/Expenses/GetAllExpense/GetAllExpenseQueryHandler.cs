using MasrafProject.Application.Features.Expenses.GetByIdExpense;
using MasrafProject.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace MasrafProject.Application.Features.Expenses.GetAllExpense;

internal sealed class GetAllExpenseQueryHandler : IRequestHandler<GetAllExpenseQuery, Result<List<Expense>>>
{
    private readonly IExpenseRepository _expenseRepository;

    public GetAllExpenseQueryHandler(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<Result<List<Expense>>> Handle(GetAllExpenseQuery request, CancellationToken cancellationToken)
    {
        var expense = await _expenseRepository
            .GetAll()
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);
        return Result<List<Expense>>.Succeed(expense);
    }
}