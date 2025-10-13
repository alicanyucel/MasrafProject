using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ApprovalStatuses.UpdateApprovalStatuses;

public sealed record UpdateApprovalStatusCommand(Guid Id,bool Onay, bool IsDeleted) : IRequest<Result<string>>;
