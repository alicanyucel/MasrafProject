using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MasrafProject.Infrastructure.Context;

namespace MasrafProject.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<AppUser, ApplicationDbContext>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}
