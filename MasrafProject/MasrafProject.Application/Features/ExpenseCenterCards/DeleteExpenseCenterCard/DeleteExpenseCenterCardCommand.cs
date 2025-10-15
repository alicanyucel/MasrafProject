using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseCenterCards.DeleteExpenseCenterCard;

public sealed record DeleteExpenseCenterCardCommand(Guid Id) : IRequest<Result<string>>;
