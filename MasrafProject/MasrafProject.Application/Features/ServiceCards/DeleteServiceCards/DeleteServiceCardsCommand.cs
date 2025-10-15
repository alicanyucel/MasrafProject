using GenericRepository;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ServiceCards.DeleteServiceCards;

public sealed class DeleteServiceCardCommandHandler : IRequestHandler<DeleteServiceCardCommand, Result<string>>
{
    private readonly IServiceCardRepository _serviceCardRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteServiceCardCommandHandler(IServiceCardRepository serviceCardRepository, IUnitOfWork unitOfWork)
    {
        _serviceCardRepository = serviceCardRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteServiceCardCommand request, CancellationToken cancellationToken)
    {
        var serviceCard = await _serviceCardRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (serviceCard is null)
        return Result<string>.Failure("Hizmet kartı bulunamadı veya zaten silinmiş.");
        serviceCard.IsDeleted = true;
        _serviceCardRepository.Update(serviceCard);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Hizmet kartı başarıyla silindi (soft delete).");
    }
}