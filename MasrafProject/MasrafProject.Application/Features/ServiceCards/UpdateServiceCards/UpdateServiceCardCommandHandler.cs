using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ServiceCards.UpdateServiceCards;

internal sealed class UpdateServiceCardCommandHandler(IServiceCardRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateServiceCardCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateServiceCardCommand request, CancellationToken cancellationToken)
    {
        ServiceCard? serviceCard = await customerRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (serviceCard == null)
        {
            return Result<string>.Failure("Service Card bulunamadi.");
        }
        mapper.Map(request, serviceCard);
        customerRepository.Update(serviceCard);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Service Card  güncellendi.";

    }
}
