using MasrafProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace MasrafProject.Application.Features.Auth.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterCommandResponse>>
{
    private readonly UserManager<AppUser> _userManager;

    public RegisterCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (request.Password != request.RePassword)
        {
            return Result<RegisterCommandResponse>.Failure("Şifreler eşleşmiyor.");
        }

        var isEmail = request.EmailOrUserName.Contains("@");

        var userName = isEmail
            ? request.EmailOrUserName.Split('@')[0]
            : request.EmailOrUserName;

        var email = isEmail
            ? request.EmailOrUserName
            : null;

        var user = new AppUser
        {
            UserName = userName,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result<RegisterCommandResponse>.Failure($"Kayıt başarısız: {errors}");
        }

        var response = new RegisterCommandResponse(
            UserId: user.Id,
            UserName: user.UserName,
            Email: user.Email
        );
        return Result<RegisterCommandResponse>.Succeed(response);
    }
}