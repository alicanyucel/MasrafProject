using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ApprovalStatuses.DeleteApprovalStatuses;

public sealed record DeleteApprovalStatusCommand(Guid Id) : IRequest<Result<string>>;