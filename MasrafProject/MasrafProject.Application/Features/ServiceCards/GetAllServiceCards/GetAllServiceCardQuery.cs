using MasrafProject.Domain.Entities;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ServiceCards.GetAllServiceCards;

public sealed record GetAllServiceCardQuery : IRequest<Result<List<ServiceCard>>>;
