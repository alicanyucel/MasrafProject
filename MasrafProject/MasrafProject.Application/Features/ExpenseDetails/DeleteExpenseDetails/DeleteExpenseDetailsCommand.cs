using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseDetails.DeleteExpenseDetails;

public sealed record DeleteExpenseDetailsCommand(Guid Id) : IRequest<Result<string>>;
