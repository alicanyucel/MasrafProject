using MasrafProject.Application.Constant;
using MasrafProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace MasrafProject.Application.Features.Auth.Roles.SetRole;

internal sealed class SetRoleCommandHandler(
  RoleManager<AppRole> roleManager) : IRequestHandler<SetRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SetRoleCommand request, CancellationToken cancellationToken)
    {
        List<AppRole> currentRoles = await roleManager.Roles.ToListAsync(cancellationToken);

        List<AppRole> staticRoles = ConstantsRole.GetRoles();

        foreach (var role in currentRoles)
        {
            if (!staticRoles.Any(p => p.Name == role.Name))
            {
                await roleManager.DeleteAsync(role);
            }
        }

        foreach (var role in staticRoles)
        {
            if (!currentRoles.Any(p => p.Name == role.Name))
            {
                await roleManager.CreateAsync(role);
            }
        }
        return "Roller eklendi.";
    }
}