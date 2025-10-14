using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Expenses.DeleteExpense;

public sealed record DeleteExpenseCommand(Guid Id) : IRequest<Result<string>>;
