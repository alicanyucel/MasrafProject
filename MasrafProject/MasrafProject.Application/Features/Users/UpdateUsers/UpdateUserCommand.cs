using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Users.UpdateUsers;

public sealed record UpdateUserCommand(
Guid Id,
string FirstName,
string LastName,
string Email,
string Password,
IList<string> Roles,
bool IsDeleted
) : IRequest<Result<string>>;
