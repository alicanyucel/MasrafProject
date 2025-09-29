using MasrafProject.Application.Features.Auth.Login;
using MasrafProject.Domain.Entities;

namespace MasrafProject.Application.Services
{
    public interface IJwtProvider
    {
        Task<LoginCommandResponse> CreateToken(AppUser user);
    }
}
