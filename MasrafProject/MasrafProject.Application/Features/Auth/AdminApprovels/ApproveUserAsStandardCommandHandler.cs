using MasrafProject.Application.Features.Auth.AdminApprovels;
using MasrafProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

public sealed class ApproveUserAsStandardCommandHandler(UserManager<AppUser> userManager)
    : IRequestHandler<ApproveUserAsStandardCommand, Result<string>>
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<Result<string>> Handle(ApproveUserAsStandardCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            return Result<string>.Failure("Kullanıcı bulunamadı.");

        var currentRoles = await _userManager.GetRolesAsync(user);

        // Zaten atanmışsa işlem yapma
        if (request.Roles.All(role => currentRoles.Contains(role)))
            return Result<string>.Failure("Kullanıcı zaten bu rollerle onaylanmış.");

        // Önceki rolleri kaldır
        if (currentRoles.Any())
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

        // Yeni rolleri ata
        var result = await _userManager.AddToRolesAsync(user, request.Roles);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result<string>.Failure($"Roller atanamadı: {errors}");
        }

        var roleList = string.Join(", ", request.Roles);
        return Result<string>.Succeed($"Kullanıcı başarıyla onaylandı. Atanan roller: {roleList}");
    }
}

