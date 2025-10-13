using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Users.DeleteUsers;

public sealed record DeleteUserCommand(Guid Id) : IRequest<Result<string>>;
