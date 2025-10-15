using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace MasrafProject.Application.Features.ServiceCards.GetAllServiceCards;

internal sealed class GetAllServiceCarQueryHandler : IRequestHandler<GetAllServiceCardQuery, Result<List<ServiceCard>>>
{
    private readonly IServiceCardRepository _serviceCardRepository;

    public GetAllServiceCarQueryHandler(IServiceCardRepository serviceCardRepository)
    {
        _serviceCardRepository = serviceCardRepository;
    }

    public async Task<Result<List<ServiceCard>>> Handle(GetAllServiceCardQuery request, CancellationToken cancellationToken)
    {
        var serviceCards = await _serviceCardRepository 
            .GetAll()
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);                        
           return Result<List<ServiceCard>>.Succeed(serviceCards);
    }
}