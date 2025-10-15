using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseCenterCards.UpdateCenterCard;

internal sealed class UpdateExpenseCenterCardCommandHandler(IExpenseCenterCardRepository expenseCenterCardRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateExpenseCenterCardCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateExpenseCenterCardCommand request, CancellationToken cancellationToken)
    {
        ExpenseCenterCard? expenseCenterCard = await expenseCenterCardRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (expenseCenterCard == null)
        {
            return Result<string>.Failure("Expense Center Card bulunamadi.");
        }
        mapper.Map(request, expenseCenterCard);
        expenseCenterCardRepository.Update(expenseCenterCard);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Expense Center Card güncellendi.";
    }
}