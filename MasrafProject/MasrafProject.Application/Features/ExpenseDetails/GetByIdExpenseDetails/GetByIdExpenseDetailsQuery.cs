using MasrafProject.Domain.Entities;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseDetails.GetByIdExpenseDetails;

public sealed record GetByIdExpenseDetailQuery(Guid Id) : IRequest<Result<ExpenseDetail>>;
