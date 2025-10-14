using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Expenses.GetByIdExpense;

public sealed record GetByIdExpenseQuery(Guid Id) : IRequest<Result<Expense>>;