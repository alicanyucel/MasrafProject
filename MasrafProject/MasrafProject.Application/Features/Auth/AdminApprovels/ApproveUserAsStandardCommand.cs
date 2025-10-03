using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Auth.AdminApprovels;

public sealed record ApproveUserAsStandardCommand(Guid UserId, List<string> Roles) : IRequest<Result<string>>;


