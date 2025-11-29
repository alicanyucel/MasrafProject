using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;
using MasrafProject.Application.Interfaces;

namespace MasrafProject.Application.Features.Expenses.CreateExpense;

internal sealed class CreateExpenseCommandHandler(
    IExpenseRepository customerRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ITenantProvider tenantProvider) : IRequestHandler<CreateExpenseCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        Expense expense = mapper.Map<Expense>(request);
        expense.TenantId = tenantProvider.TenantId;

        decimal kdvOrani = 0.20m;
        decimal kdvTutari = expense.ToplamTutar * kdvOrani;
        decimal genelToplam = expense.ToplamTutar + kdvTutari;
        expense.ToplamKdvTutar = kdvTutari;
        expense.GenelToplam = genelToplam;
        await customerRepository.AddAsync(expense, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return $"Masraf kaydý yapýldý. KDV: {kdvTutari:C2}, Genel Toplam: {genelToplam:C2}";
    }
}
