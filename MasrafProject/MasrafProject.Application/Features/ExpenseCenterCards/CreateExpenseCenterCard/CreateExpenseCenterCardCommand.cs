using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseCenterCards.CreateExpenseCenterCard;

public record CreateExpenseCenterCardCommand(
 string MasrafMerkeziKodu,
 string MasrafMerkeziAdi
) : IRequest<Result<string>>;
