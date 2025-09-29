using GenericRepository;
using MasrafProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MasrafProject.Infrastructure.Context
{
    internal sealed class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ExpenseDetail> ExpenseDetails => Set<ExpenseDetail>();
        public DbSet<ProjectCard> ProjectCards => Set<ProjectCard>();
        public DbSet<ServiceCard> ServiceCards => Set<ServiceCard>();
        public DbSet<ExpenseCenterCard> ExpenseCenterCards => Set<ExpenseCenterCard>();
        public DbSet<Expense> expenses => Set<Expense>();
        public DbSet<ApprovalStatus> approvalStatuses   => Set<ApprovalStatus>();



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityRoleClaim<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityUserRole<Guid>>();
            builder.Ignore<IdentityUserClaim<Guid>>();
        }
    }
}
