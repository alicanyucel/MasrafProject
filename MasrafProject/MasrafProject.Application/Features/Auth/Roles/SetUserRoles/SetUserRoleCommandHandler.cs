using MasrafProject.Application.Constant;
using MasrafProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace MasrafProject.Application.Features.Auth.Roles.SetUserRoles;

public sealed class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand, Result<string>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public SetUserRoleCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Result<string>> Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            return Result<string>.Failure("Kullanıcı bulunamadı.");

       
        var constantRoles = ConstantsRole.GetRoles();
        var validRoleNames = constantRoles.Select(r => r.Name!).ToHashSet(StringComparer.InvariantCultureIgnoreCase);
        var invalidRoles = request.Roles.Where(r => !validRoleNames.Contains(r)).ToList();
        if (invalidRoles.Any())
            return Result<string>.Failure($"Geçersiz roller: {string.Join(", ", invalidRoles)}");

     
        foreach (var constantRole in constantRoles)
        {
            if (!await _roleManager.RoleExistsAsync(constantRole.Name!))
            {
                await _roleManager.CreateAsync(new AppRole
                {
                    Id = constantRole.Id,
                    Name = constantRole.Name,
                    NormalizedName = constantRole.Name!.ToUpperInvariant()
                });
            }
        }
        var currentRoles = await _userManager.GetRolesAsync(user);
        if (currentRoles.Any())
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
                return Result<string>.Failure(string.Join(" | ", removeResult.Errors.Select(e => e.Description)));
        }

        var targetRoles = request.Roles.Distinct(StringComparer.InvariantCultureIgnoreCase).ToList();
        if (targetRoles.Any())
        {
            var addResult = await _userManager.AddToRolesAsync(user, targetRoles);
            if (!addResult.Succeeded)
                return Result<string>.Failure(string.Join(" | ", addResult.Errors.Select(e => e.Description)));
        }

        return Result<string>.Succeed("Roller başarıyla güncellendi.");
    }
}
