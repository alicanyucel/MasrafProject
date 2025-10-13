using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace MasrafProject.Application.Features.ApprovalStatuses.GetAllApprovalStatuses;

internal sealed class GetAllApprovalStatusQueryHandler : IRequestHandler<GetAllApprovalStatusQuery, Result<List<ApprovalStatus>>>
{
    private readonly IApprovalStatusRepository _approvalStatusRepository;

    public GetAllApprovalStatusQueryHandler(IApprovalStatusRepository approvalStatusRepository)
    {
        _approvalStatusRepository = approvalStatusRepository;
    }

    public async Task<Result<List<ApprovalStatus>>> Handle(GetAllApprovalStatusQuery request, CancellationToken cancellationToken)
    {
        var approvalStatus = await _approvalStatusRepository
            .GetAll()
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);
        return Result<List<ApprovalStatus>>.Succeed(approvalStatus);
    }
}