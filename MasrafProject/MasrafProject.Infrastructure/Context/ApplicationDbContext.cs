using GenericRepository;
using MasrafProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MasrafProject.Application.Interfaces;

namespace MasrafProject.Infrastructure.Context;

internal sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IUnitOfWork
{
    private readonly ITenantProvider _tenantProvider;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantProvider tenantProvider) : base(options)
    {
        _tenantProvider = tenantProvider;
    }
    
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<ExpenseDetail> ExpenseDetails => Set<ExpenseDetail>();
    public DbSet<ProjectCard> ProjectCards => Set<ProjectCard>();
    public DbSet<ServiceCard> ServiceCards => Set<ServiceCard>();
    public DbSet<ExpenseCenterCard> ExpenseCenterCards => Set<ExpenseCenterCard>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<ApprovalStatus> ApprovalStatuses => Set<ApprovalStatus>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>(b =>
        {
            b.ToTable("AppUsers");
            b.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
            b.Property(u => u.LastName).HasMaxLength(100).IsRequired();
            b.Property(u => u.RefreshToken).HasMaxLength(500);
            b.Property(u => u.RefreshTokenExpires);
        });

        builder.Entity<AppRole>(b =>
        {
            b.ToTable("AppRoles");
            b.Property(r => r.Name).HasMaxLength(100).IsRequired();
        });

        builder.Entity<IdentityUserRole<Guid>>(b =>
        {
            b.ToTable("AppUserRoles");
            b.HasKey(ur => new { ur.UserId, ur.RoleId });
        });
        
        builder.Entity<IdentityUserClaim<Guid>>(b => { b.ToTable("UserClaims"); });
        
        builder.Entity<IdentityUserLogin<Guid>>(b =>
        {
            b.ToTable("UserLogins");
            b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
        });
        
        builder.Entity<IdentityRoleClaim<Guid>>(b => { b.ToTable("RoleClaims"); });
        builder.Entity<IdentityUserToken<Guid>>(b => { b.ToTable("UserTokens"); });

        builder.Entity<Company>(b =>
        {
            b.ToTable("Companies");
            b.Property(c => c.Id).UseIdentityColumn(1, 1);
            b.HasIndex(c => c.Name).IsUnique();
            b.HasQueryFilter(c => c.TenantId == _tenantProvider.TenantId);
        });

        builder.Entity<Expense>(b =>
        {
            b.ToTable("Expenses");
            b.HasQueryFilter(e => e.TenantId == _tenantProvider.TenantId);
        });

        builder.Entity<ExpenseDetail>(b =>
        {
            b.ToTable("ExpenseDetails");
            b.HasQueryFilter(e => e.TenantId == _tenantProvider.TenantId);
        });

        builder.Entity<ExpenseCenterCard>(b =>
        {
            b.ToTable("ExpenseCenterCards");
            b.HasQueryFilter(e => e.TenantId == _tenantProvider.TenantId);
        });

        builder.Entity<ProjectCard>(b =>
        {
            b.ToTable("ProjectCards");
            b.HasQueryFilter(e => e.TenantId == _tenantProvider.TenantId);
        });

        builder.Entity<ServiceCard>(b =>
        {
            b.ToTable("ServiceCards");
            b.HasQueryFilter(e => e.TenantId == _tenantProvider.TenantId);
        });

        builder.Entity<ApprovalStatus>(b =>
        {
            b.ToTable("ApprovalStatuses");
            b.HasQueryFilter(e => e.TenantId == _tenantProvider.TenantId);
        });

        builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
    }
}
