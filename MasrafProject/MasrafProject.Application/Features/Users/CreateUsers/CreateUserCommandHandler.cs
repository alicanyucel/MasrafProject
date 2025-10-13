using AutoMapper;
using MasrafProject.Application.Constant;
using MasrafProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace MasrafProject.Application.Features.Users.CreateUsers;

internal sealed class CreateUserCommandHandler(
 UserManager<AppUser> userManager,
 RoleManager<AppRole> roleManager,
 IMapper mapper
) : IRequestHandler<CreateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var appUser = mapper.Map<AppUser>(request);
        appUser.UserName = request.Email;
        appUser.EmailConfirmed = true;

        var result = await userManager.CreateAsync(appUser, request.Password);

        if (!result.Succeeded)
        return Result<string>.Failure(string.Join(" | ", result.Errors.Select(e => e.Description)));

        foreach (var role in ConstantsRole.GetRoles())
        {
            if (!await roleManager.RoleExistsAsync(role.Name!))
            await roleManager.CreateAsync(role);
            await userManager.AddToRoleAsync(appUser, role.Name!);
        }
        return Result<string>.Succeed("Kullanıcı kaydı yapıldı");
    }
}