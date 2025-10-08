using GenericRepository;
using MasrafProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MasrafProject.Infrastructure.Context;

internal sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ExpenseDetail> ExpenseDetails => Set<ExpenseDetail>();
    public DbSet<ProjectCard> ProjectCards => Set<ProjectCard>();
    public DbSet<ServiceCard> ServiceCards => Set<ServiceCard>();
    public DbSet<ExpenseCenterCard> ExpenseCenterCards => Set<ExpenseCenterCard>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<ApprovalStatus> ApprovalStatuses => Set<ApprovalStatus>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // AppUser yapılandırması
        builder.Entity<AppUser>(b =>
        {
            b.ToTable("AppUsers");

            b.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            b.Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();

            b.Property(u => u.RefreshToken)
                .HasMaxLength(500);

            b.Property(u => u.RefreshTokenExpires);
        });

        // AppRole yapılandırması
        builder.Entity<AppRole>(b =>
        {
            b.ToTable("AppRoles");

            b.Property(r => r.Name)
                .HasMaxLength(100)
                .IsRequired();
        });

        // Identity tabloları
        builder.Entity<IdentityUserRole<Guid>>(b =>
        {
            b.ToTable("AppUserRoles");
            b.HasKey(ur => new { ur.UserId, ur.RoleId });
        });

        builder.Entity<IdentityUserClaim<Guid>>(b =>
        {
            b.ToTable("UserClaims");
        });

        builder.Entity<IdentityUserLogin<Guid>>(b =>
        {
            b.ToTable("UserLogins");
            b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
        });

        builder.Entity<IdentityRoleClaim<Guid>>(b =>
        {
            b.ToTable("RoleClaims");
        });

        builder.Entity<IdentityUserToken<Guid>>(b =>
        {
            b.ToTable("UserTokens");
        });

        // Domain entity konfigürasyonları
        builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
    }
}
