using AutoMapper;
using GenericRepository;
using MasrafProject.Application.Features.Expenses.UpdateExpense;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;
using MasrafProject.Application.Interfaces;

internal sealed class UpdateExpenseCommandHandler(
    IExpenseRepository expenseRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ITenantProvider tenantProvider) : IRequestHandler<UpdateExpenseCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        Expense? expense = await expenseRepository.GetByExpressionWithTrackingAsync(
            p => p.Id == request.Id, cancellationToken);
        if (expense is null)
        {
            return Result<string>.Failure("Expense bulunamadý.");
        }
        mapper.Map(request, expense);
        expense.TenantId = tenantProvider.TenantId;
        decimal kdvOrani = 0.20m;
        expense.ToplamKdvTutar = expense.ToplamTutar * kdvOrani;
        expense.GenelToplam = expense.ToplamTutar + expense.ToplamKdvTutar;
        expenseRepository.Update(expense);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed($"Expense güncellendi. KDV: {expense.ToplamKdvTutar:C2}, Genel Toplam: {expense.GenelToplam:C2}");
    }
}
