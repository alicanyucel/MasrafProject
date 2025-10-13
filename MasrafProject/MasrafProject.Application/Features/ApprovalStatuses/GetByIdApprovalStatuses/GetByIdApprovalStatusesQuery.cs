using MasrafProject.Domain.Entities;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ApprovalStatuses.GetByIdApprovalStatuses;

public sealed record GetApprovalStatusByIdQuery(Guid Id) : IRequest<Result<ApprovalStatus>>;
