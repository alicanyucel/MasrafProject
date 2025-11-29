using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;
using MasrafProject.Application.Interfaces;

namespace MasrafProject.Application.Features.ApprovalStatuses.CreateApprovalStatuses
{
    internal sealed class CreateApprovalStatusCommandHandler(
        IApprovalStatusRepository approvalStatusRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ITenantProvider tenantProvider) : IRequestHandler<CreateApprovalStatusCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateApprovalStatusCommand request, CancellationToken cancellationToken)
        {
            ApprovalStatus approvalStatus = mapper.Map<ApprovalStatus>(request);
            approvalStatus.TenantId = tenantProvider.TenantId;
            await approvalStatusRepository.AddAsync(approvalStatus, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Approval Status kaydý yapýldý";
        }
    }
}
