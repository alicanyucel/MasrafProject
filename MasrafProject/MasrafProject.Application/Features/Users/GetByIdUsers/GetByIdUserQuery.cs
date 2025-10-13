using MasrafProject.Domain.Entities;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Users.GetByIdUsers;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<Result<AppUser>>;
