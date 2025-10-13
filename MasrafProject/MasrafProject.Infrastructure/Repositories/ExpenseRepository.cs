using GenericRepository;
using MasrafProject.Domain.Repositories;
using MasrafProject.Infrastructure.Context;

namespace MasrafProject.Infrastructure.Repositories;

internal sealed class ExpenseRepository : Repository<Expense, ApplicationDbContext>, IExpenseRepository
{
    public ExpenseRepository(ApplicationDbContext context) : base(context)
    {
    }
}
