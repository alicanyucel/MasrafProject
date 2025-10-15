using GenericRepository;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseCenterCards.DeleteExpenseCenterCard;

public sealed class DeleteExpenseCenterCardCommandHandler : IRequestHandler<DeleteExpenseCenterCardCommand, Result<string>>
{
    private readonly IExpenseCenterCardRepository _expenseCenterCardRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpenseCenterCardCommandHandler(IExpenseCenterCardRepository expenseCenterCardRepository, IUnitOfWork unitOfWork)
    {
        _expenseCenterCardRepository = expenseCenterCardRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteExpenseCenterCardCommand request, CancellationToken cancellationToken)
    {
        var expenseCenterCard = await _expenseCenterCardRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (expenseCenterCard is null)
        return Result<string>.Failure("Harcama merkezi kartı bulunamadı veya zaten silinmiş.");
        expenseCenterCard.IsDeleted = true;
        _expenseCenterCardRepository.Update(expenseCenterCard);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Harcama merkezi kartı başarıyla silindi (soft delete).");
    }
}