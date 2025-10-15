using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ServiceCards.CreateServiceCards;

public record CreateServiceCardCommand(
string HizmetKodu,
string HizmetAdi,
decimal KdvOrani
) : IRequest<Result<string>>;
