using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Auth.Login
{
    public sealed record LoginCommand(
        string EmailOrUserName,
        string Password) : IRequest<Result<LoginCommandResponse>>;
}
