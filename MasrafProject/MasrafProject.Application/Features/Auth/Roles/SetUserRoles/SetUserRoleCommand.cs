using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Auth.Roles.SetUserRoles;

public sealed record SetUserRoleCommand(
Guid UserId,
IList<string> Roles
) : IRequest<Result<string>>;
