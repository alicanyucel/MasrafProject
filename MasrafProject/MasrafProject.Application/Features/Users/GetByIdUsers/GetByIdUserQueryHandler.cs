using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace MasrafProject.Application.Features.Users.GetByIdUsers;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<AppUser>>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<AppUser> _userManager;
    public GetUserByIdQueryHandler(IUserRepository userRepository, UserManager<AppUser> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }
    public async Task<Result<AppUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );
        if (user is null)
        return Result<AppUser>.Failure("Kullanıcı bulunamadı veya silinmiş.");
        var roles = await _userManager.GetRolesAsync(user);
        user.Roles = roles?.ToList() ?? new List<string>();
        return Result<AppUser>.Succeed(user);
    }
}