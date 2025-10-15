using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ServiceCards.CreateServiceCards;

internal sealed class CreateServiceCardComamndHandler(IServiceCardRepository serviceCardRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateServiceCardCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateServiceCardCommand request, CancellationToken cancellationToken)
    {
        ServiceCard serviceCard = mapper.Map<ServiceCard>(request);
        await serviceCardRepository.AddAsync(serviceCard, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Service Card kaydı yapıldı";
    }
}