using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MasrafProject.Infrastructure.Context;

namespace MasrafProject.Infrastructure.Repositories;

internal sealed class ExpenseCenterCardRepository : Repository<ExpenseCenterCard, ApplicationDbContext>, IExpenseCenterCardRepository
{
    public ExpenseCenterCardRepository(ApplicationDbContext context) : base(context)
    {
    }
}
