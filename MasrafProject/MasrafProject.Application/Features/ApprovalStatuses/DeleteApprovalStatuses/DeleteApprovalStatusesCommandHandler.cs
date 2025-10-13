using GenericRepository;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ApprovalStatuses.DeleteApprovalStatuses;

public sealed class DeleteApprovalStatusCommandHandler : IRequestHandler<DeleteApprovalStatusCommand, Result<string>>
{
    private readonly IApprovalStatusRepository _approvalStatusRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteApprovalStatusCommandHandler(IApprovalStatusRepository ApprovalStatusRepository, IUnitOfWork unitOfWork)
    {
        _approvalStatusRepository= ApprovalStatusRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteApprovalStatusCommand request, CancellationToken cancellationToken)
    {
        var approvalStatus = await _approvalStatusRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );
        if (approvalStatus is null)
        return Result<string>.Failure("ApprovalStatus bulunamadı veya zaten silinmiş.");
        approvalStatus.IsDeleted = true;
        _approvalStatusRepository.Update(approvalStatus);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Approval Status başarıyla silindi (soft delete).");
    }
}