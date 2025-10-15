using MasrafProject.Domain.Entities;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ServiceCards.GetByIdServiceCards;

public sealed record GetServiceCardByIdQuery(Guid Id) : IRequest<Result<ServiceCard>>;
