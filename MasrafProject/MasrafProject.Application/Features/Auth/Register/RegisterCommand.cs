using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Auth.Register;

public sealed record RegisterCommand(
string EmailOrUserName,
string Password,
string RePassword) : IRequest<Result<RegisterCommandResponse>>;
