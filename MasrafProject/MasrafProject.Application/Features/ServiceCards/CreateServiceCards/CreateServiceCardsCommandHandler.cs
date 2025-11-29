using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;
using MasrafProject.Application.Interfaces;

namespace MasrafProject.Application.Features.ServiceCards.CreateServiceCards;

internal sealed class CreateServiceCardComamndHandler(
    IServiceCardRepository serviceCardRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ITenantProvider tenantProvider) : IRequestHandler<CreateServiceCardCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateServiceCardCommand request, CancellationToken cancellationToken)
    {
        ServiceCard serviceCard = mapper.Map<ServiceCard>(request);
        serviceCard.TenantId = tenantProvider.TenantId;
        await serviceCardRepository.AddAsync(serviceCard, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Service Card kaydý yapýldý";
    }
}
