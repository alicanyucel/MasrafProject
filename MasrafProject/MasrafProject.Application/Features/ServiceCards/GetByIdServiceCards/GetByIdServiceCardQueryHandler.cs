using AutoMapper;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ServiceCards.GetByIdServiceCards;

public sealed class GetServiceCardByIdQueryHandler : IRequestHandler<GetServiceCardByIdQuery, Result<ServiceCard>>
{
    private readonly IServiceCardRepository _serviceCardRepository;
    private readonly IMapper _mapper;

    public GetServiceCardByIdQueryHandler(IServiceCardRepository serviceCardRepository, IMapper mapper)
    {
        _serviceCardRepository = serviceCardRepository;
        _mapper = mapper;
    }

    public async Task<Result<ServiceCard>> Handle(GetServiceCardByIdQuery request, CancellationToken cancellationToken)
    {
        var serviceCardEntity = await _serviceCardRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (serviceCardEntity is null)
        return Result<ServiceCard>.Failure("Hizmet kartı bulunamadı veya silinmiş.");
        var serviceCard = _mapper.Map<ServiceCard>(serviceCardEntity);
        return Result<ServiceCard>.Succeed(serviceCard);
    }
}