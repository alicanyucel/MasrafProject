using AutoMapper;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Expenses.GetByIdExpense;

public sealed class GetByIdExpenseQueryHandler : IRequestHandler<GetByIdExpenseQuery, Result<Expense>>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public GetByIdExpenseQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }
    public async Task<Result<Expense>> Handle(GetByIdExpenseQuery request, CancellationToken cancellationToken)
    {
        var expenseEntity= await _expenseRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );
        if (expenseEntity is null)
        return Result<Expense>.Failure("Expense bulunamadı veya silinmiş.");
        var expense = _mapper.Map<Expense>(expenseEntity);
        return Result<Expense>.Succeed(expense);
    }
}