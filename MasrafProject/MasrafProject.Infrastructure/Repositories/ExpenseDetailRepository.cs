using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MasrafProject.Infrastructure.Context;

namespace MasrafProject.Infrastructure.Repositories;

internal sealed class ExpenseDetailRepository : Repository<ExpenseDetail, ApplicationDbContext>, IExpenseDetailRepository
{
    public ExpenseDetailRepository(ApplicationDbContext context) : base(context)
    {
    }
}
