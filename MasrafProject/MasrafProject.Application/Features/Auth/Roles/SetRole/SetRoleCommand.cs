using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Auth.Roles.SetRole;

public sealed record SetRoleCommand() : IRequest<Result<string>>;
