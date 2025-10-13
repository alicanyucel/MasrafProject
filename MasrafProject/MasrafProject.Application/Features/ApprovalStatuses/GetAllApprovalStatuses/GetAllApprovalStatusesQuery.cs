using MasrafProject.Domain.Entities;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ApprovalStatuses.GetAllApprovalStatuses;

public sealed record GetAllApprovalStatusQuery : IRequest<Result<List<ApprovalStatus>>>;
