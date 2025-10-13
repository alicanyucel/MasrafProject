using MasrafProject.Application.Constant;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Auth.Roles.SetUserRoles;

public sealed class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;

    public SetUserRoleCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByExpressionAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
        return Result<string>.Failure("Kullanıcı bulunamadı.");
        var validRoleNames = ConstantsRole.GetRoles().Select(r => r.Name).ToList();
        var invalidRoles = request.Roles.Except(validRoleNames).ToList();
        if (invalidRoles.Any())
        return Result<string>.Failure($"Geçersiz roller: {string.Join(", ", invalidRoles)}");
        user.Roles = request.Roles.ToList(); 
        _userRepository.Update(user);
        return Result<string>.Succeed("Roller başarıyla güncellendi.");
    }
}
