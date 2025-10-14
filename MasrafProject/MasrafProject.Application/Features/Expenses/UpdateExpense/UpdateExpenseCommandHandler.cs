using AutoMapper;
using GenericRepository;
using MasrafProject.Application.Features.Expenses.UpdateExpense;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

internal sealed class UpdateExpenseCommandHandler(
    IExpenseRepository expenseRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateExpenseCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        Expense? expense = await expenseRepository.GetByExpressionWithTrackingAsync(
            P => P.Id == request.Id, cancellationToken);
        if (expense == null)
        {
            return Result<string>.Failure("Expense bulunamadı.");
        }
        mapper.Map(request, expense);
        decimal kdvOrani = 0.20m;
        expense.ToplamKdvTutar = expense.ToplamTutar * kdvOrani;
        expense.GenelToplam = expense.ToplamTutar + expense.ToplamKdvTutar;
        expenseRepository.Update(expense);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return $"Expense güncellendi. KDV: {expense.ToplamKdvTutar:C2}, Genel Toplam: {expense.GenelToplam:C2}";
    }
}

