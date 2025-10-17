using GenericRepository;
using MasrafProject.Application.Features.Expenses.DeleteExpense;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseDetails.DeleteExpenseDetails;

public sealed class DeleteExpenseDetailsCommandHandler : IRequestHandler<DeleteExpenseDetailsCommand, Result<string>>
{
    private readonly IExpenseDetailRepository _expenseDetailRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpenseDetailsCommandHandler(IExpenseDetailRepository expenseDetailRepository, IUnitOfWork unitOfWork)
    {
        _expenseDetailRepository = expenseDetailRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteExpenseDetailsCommand request, CancellationToken cancellationToken)
    {
        var expenseDetail = await _expenseDetailRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (expenseDetail is null)
        return Result<string>.Failure("Expense Detail bulunamadı veya zaten silinmiş.");
        expenseDetail.IsDeleted = true;
        _expenseDetailRepository.Update(expenseDetail);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Expense Detail başarıyla silindi (soft delete).");
    }
}
