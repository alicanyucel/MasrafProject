using AutoMapper;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseDetails.GetByIdExpenseDetails;

public sealed class GetByIdExpenseDetailQueryHandler : IRequestHandler<GetByIdExpenseDetailQuery, Result<ExpenseDetail>>
{
    private readonly IExpenseDetailRepository _expenseDetailRepository;
    private readonly IMapper _mapper;

    public GetByIdExpenseDetailQueryHandler(IExpenseDetailRepository expenseDetailRepository, IMapper mapper)
    {
        _expenseDetailRepository = expenseDetailRepository;
        _mapper = mapper;
    }
    public async Task<Result<ExpenseDetail>> Handle(GetByIdExpenseDetailQuery request, CancellationToken cancellationToken)
    {
        var expenseDetailEntity = await _expenseDetailRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );
        if (expenseDetailEntity is null)
        return Result<ExpenseDetail>.Failure("Expense Detail bulunamadı veya silinmiş.");
        var expenseDetail = _mapper.Map<ExpenseDetail>(expenseDetailEntity);
        return Result<ExpenseDetail>.Succeed(expenseDetail);
    }
}
