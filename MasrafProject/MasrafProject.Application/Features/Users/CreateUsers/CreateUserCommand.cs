using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Users.CreateUsers;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    IList<string> Roles,
    bool IsDeleted
) : IRequest<Result<string>>;