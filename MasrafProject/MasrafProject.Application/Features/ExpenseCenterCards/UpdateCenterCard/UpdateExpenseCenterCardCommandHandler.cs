using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;
using MasrafProject.Application.Interfaces;

namespace MasrafProject.Application.Features.ExpenseCenterCards.UpdateCenterCard;

internal sealed class UpdateExpenseCenterCardCommandHandler(
    IExpenseCenterCardRepository expenseCenterCardRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ITenantProvider tenantProvider) : IRequestHandler<UpdateExpenseCenterCardCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateExpenseCenterCardCommand request, CancellationToken cancellationToken)
    {
        ExpenseCenterCard? expenseCenterCard = await expenseCenterCardRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
        if (expenseCenterCard is null)
        {
            return Result<string>.Failure("Expense Center Card bulunamadý.");
        }
        mapper.Map(request, expenseCenterCard);
        expenseCenterCard.TenantId = tenantProvider.TenantId;
        expenseCenterCardRepository.Update(expenseCenterCard);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Expense Center Card güncellendi.");
    }
}
