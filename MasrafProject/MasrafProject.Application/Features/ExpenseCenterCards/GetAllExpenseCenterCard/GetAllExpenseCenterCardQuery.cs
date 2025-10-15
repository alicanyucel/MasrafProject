using MasrafProject.Domain.Entities;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseCenterCards.GetAllExpenseCenterCard;

public sealed record GetAllExpenseCenterCardQuery : IRequest<Result<List<ExpenseCenterCard>>>;
