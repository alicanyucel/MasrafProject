using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MasrafProject.Infrastructure.Context;

namespace MasrafProject.Infrastructure.Repositories;

internal sealed class ServiceCardRepository : Repository<ServiceCard, ApplicationDbContext>, IServiceCardRepository
{
    public ServiceCardRepository(ApplicationDbContext context) : base(context)
    {
    }
}
