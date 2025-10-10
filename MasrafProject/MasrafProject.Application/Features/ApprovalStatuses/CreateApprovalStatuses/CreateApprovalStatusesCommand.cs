using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ApprovalStatuses.CreateApprovalStatuses;

public sealed record CreateApprovalStatusCommand(bool Onay,bool IsDeleted) : IRequest<Result<string>>;
