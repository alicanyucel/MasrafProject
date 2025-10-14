using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Expenses.CreateExpense;

internal sealed class CreateExpenseCommandHandler(IExpenseRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateExpenseCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        Expense expense = mapper.Map<Expense>(request);
        await customerRepository.AddAsync(expense, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Masraf kaydı yapıldı";
    }
}