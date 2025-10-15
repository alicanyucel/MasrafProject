using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ServiceCards.DeleteServiceCards;

public sealed record DeleteServiceCardCommand(Guid Id) : IRequest<Result<string>>;
