using AutoMapper;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ApprovalStatuses.GetByIdApprovalStatuses;

public sealed class GetApprovalStatusByIdQueryHandler : IRequestHandler<GetApprovalStatusByIdQuery, Result<ApprovalStatus>>
{
    private readonly IApprovalStatusRepository _approvalStatusRepository;
    private readonly IMapper _mapper;

    public GetApprovalStatusByIdQueryHandler(IApprovalStatusRepository approvalStatusRepository, IMapper mapper)
    {
        _approvalStatusRepository = approvalStatusRepository;
        _mapper = mapper;
    }

    public async Task<Result<ApprovalStatus>> Handle(GetApprovalStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var approvalStatusEntity = await _approvalStatusRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (approvalStatusEntity is null)
        return Result<ApprovalStatus>.Failure("Approval Status bulunamadı veya silinmiş.");
        var approvalStatus = _mapper.Map<ApprovalStatus>(approvalStatusEntity);
        return Result<ApprovalStatus>.Succeed(approvalStatus);
    }
}