using MasrafProject.Application.Dtos;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace MasrafProject.Application.Features.Users.GetAllUsers;

internal sealed class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, Result<List<UserDto>>>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<AppUser> _userManager;

    public GetAllUserQueryHandler(IUserRepository userRepository, UserManager<AppUser> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task<Result<List<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var userEntities = await _userRepository
            .GetAll()
            .Where(user => !user.IsDeleted)
            .ToListAsync(cancellationToken);

        var users = new List<UserDto>(userEntities.Count);
        foreach (var user in userEntities)
        {
            var roles = await _userManager.GetRolesAsync(user);
            users.Add(new UserDto(
                Id: user.Id,
                FirstName: user.FirstName,
                LastName: user.LastName,
                Email: user.Email ?? string.Empty,
                IsDeleted: user.IsDeleted,
                Roles: roles?.ToList() ?? new List<string>()
            ));
        }

        return users.Count == 0
            ? Result<List<UserDto>>.Failure("Hiç aktif kullanıcı bulunamadı.")
            : Result<List<UserDto>>.Succeed(users);
    }
}