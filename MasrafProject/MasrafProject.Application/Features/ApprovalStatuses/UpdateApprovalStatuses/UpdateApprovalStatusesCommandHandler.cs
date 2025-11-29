using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;
using MasrafProject.Application.Interfaces;

namespace MasrafProject.Application.Features.ApprovalStatuses.UpdateApprovalStatuses;

internal sealed class UpdateApprovalStatusCommandHandler(
    IApprovalStatusRepository approvalStatusRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ITenantProvider tenantProvider) : IRequestHandler<UpdateApprovalStatusCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateApprovalStatusCommand request, CancellationToken cancellationToken)
    {
        ApprovalStatus? approvalStatus = await approvalStatusRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
        if (approvalStatus is null)
        {
            return Result<string>.Failure("Approval Status bulunamadý.");
        }
        mapper.Map(request, approvalStatus);
        approvalStatus.TenantId = tenantProvider.TenantId;
        approvalStatusRepository.Update(approvalStatus);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Approval Status güncellendi.");
    }
}
