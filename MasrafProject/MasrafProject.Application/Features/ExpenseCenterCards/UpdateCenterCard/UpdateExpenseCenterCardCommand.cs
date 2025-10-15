using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseCenterCards.UpdateCenterCard;

public record UpdateExpenseCenterCardCommand(
 Guid Id,
 string MasrafMerkeziKodu,
 string MasrafMerkeziAdi
) : IRequest<Result<string>>;
