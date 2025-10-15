using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MasrafProject.Infrastructure.Context;

namespace MasrafProject.Infrastructure.Repositories;

internal sealed class ProjectCardRepository : Repository<ProjectCard, ApplicationDbContext>, IProjectCardRepository
{
    public ProjectCardRepository(ApplicationDbContext context) : base(context)
    {
    }
}
