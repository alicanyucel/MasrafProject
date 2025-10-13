using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ApprovalStatuses.UpdateApprovalStatuses;

internal sealed class UpdateApprovalStatusCommandHandler(IApprovalStatusRepository approvalStatusRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateApprovalStatusCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateApprovalStatusCommand request, CancellationToken cancellationToken)
    {
        ApprovalStatus? approvalStatus = await approvalStatusRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (approvalStatus == null)
        {
            return Result<string>.Failure("Approval Status bulunamadi.");
        }
        mapper.Map(request, approvalStatus);
        approvalStatusRepository.Update(approvalStatus);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Approval Status güncellendi.";

    }
}