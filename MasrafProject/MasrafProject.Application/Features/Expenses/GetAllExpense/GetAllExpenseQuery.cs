using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Expenses.GetAllExpense;

public sealed record GetAllExpenseQuery : IRequest<Result<List<Expense>>>;
