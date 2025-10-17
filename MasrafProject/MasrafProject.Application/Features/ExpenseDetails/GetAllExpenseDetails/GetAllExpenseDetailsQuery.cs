using MasrafProject.Domain.Entities;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseDetails.GetAllExpenseDetails;

public sealed record GetAllExpenseDetailsQuery : IRequest<Result<List<ExpenseDetail>>>;