using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MasrafProject.Infrastructure.Context;

namespace MasrafProject.Infrastructure.Repositories;

internal sealed class ApprovalStatusRepository : Repository<ApprovalStatus, ApplicationDbContext>, IApprovalStatusRepository
{
    public ApprovalStatusRepository(ApplicationDbContext context) : base(context)
    {
    }
}
