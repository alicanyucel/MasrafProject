using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Expenses.UpdateExpense;

internal sealed class UpdateExpenseCommandHandler(IExpenseRepository expenseRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateExpenseCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        Expense? expense = await expenseRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (expense == null)
        {
            return Result<string>.Failure("Expense bulunamadi.");
        }
        mapper.Map(request, expense);
        expenseRepository.Update(expense);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Expense güncellendi.";
    }
}