using MasrafProject.Application.Dtos;
using MasrafProject.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace MasrafProject.Application.Features.Users.GetAllUsers;

internal sealed class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, Result<List<UserDto>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<List<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var userEntities = await _userRepository
            .GetAll()
            .Where(user => !user.IsDeleted)
            .ToListAsync(cancellationToken);
        var users = userEntities.Select(user => new UserDto(
            Id: user.Id,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Email: user.Email ?? string.Empty,
            IsDeleted: user.IsDeleted,
            Roles: new List<string>() 
        )).ToList();
        return users.Count == 0
            ? Result<List<UserDto>>.Failure("Hiç aktif kullanıcı bulunamadı.")
            : Result<List<UserDto>>.Succeed(users);
    }
}