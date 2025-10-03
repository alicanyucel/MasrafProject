using MasrafProject.Application.Features.Auth.Roles.GetAllRole;
using MediatR;

namespace MasrafProject.Application.Features.Auth.Roles.GetRole;

public sealed record GetAllRoleQuery() : IRequest<List<GetAllRolesQueryResponse>>;