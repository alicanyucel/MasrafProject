using MasrafProject.Application.Features.Auth.Roles.GetAllRole;
using MasrafProject.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MasrafProject.Application.Features.Auth.Roles.GetRole;

internal sealed class GetRolesQueryHandler : IRequestHandler<GetAllRoleQuery, List<GetAllRolesQueryResponse>>
{
    private readonly IRoleRepository _roleRepository;

    public GetRolesQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<List<GetAllRolesQueryResponse>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
    {
        var response =
            await _roleRepository.GetAll()
            .Select(r => new GetAllRolesQueryResponse(r.Id, r.Name ?? string.Empty))
            .ToListAsync(cancellationToken);
        return response;
    }
}