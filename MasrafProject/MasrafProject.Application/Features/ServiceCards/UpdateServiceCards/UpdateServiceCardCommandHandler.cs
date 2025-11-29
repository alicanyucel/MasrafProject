using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;
using MasrafProject.Application.Interfaces;

namespace MasrafProject.Application.Features.ServiceCards.UpdateServiceCards;

internal sealed class UpdateServiceCardCommandHandler(
    IServiceCardRepository customerRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ITenantProvider tenantProvider) : IRequestHandler<UpdateServiceCardCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateServiceCardCommand request, CancellationToken cancellationToken)
    {
        ServiceCard? serviceCard = await customerRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
        if (serviceCard is null)
        {
            return Result<string>.Failure("Service Card bulunamadý.");
        }
        mapper.Map(request, serviceCard);
        serviceCard.TenantId = tenantProvider.TenantId;
        customerRepository.Update(serviceCard);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Service Card güncellendi.");
    }
}
