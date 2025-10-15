using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ServiceCards.UpdateServiceCards;

public record UpdateServiceCardCommand(
Guid Id,
string HizmetKodu,
string HizmetAdi,
decimal KdvOrani
) : IRequest<Result<string>>;
