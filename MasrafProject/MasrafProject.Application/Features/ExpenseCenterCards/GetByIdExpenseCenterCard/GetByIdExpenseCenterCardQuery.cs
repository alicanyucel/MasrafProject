using MasrafProject.Domain.Entities;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseCenterCards.GetByIdExpenseCenterCard;

public sealed record GetExpenseCenterCardByIdQuery(Guid Id) : IRequest<Result<ExpenseCenterCard>>;
