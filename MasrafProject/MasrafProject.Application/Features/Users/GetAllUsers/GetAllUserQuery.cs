using MasrafProject.Application.Dtos;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Users.GetAllUsers;

public sealed record GetAllUserQuery : IRequest<Result<List<UserDto>>>;
