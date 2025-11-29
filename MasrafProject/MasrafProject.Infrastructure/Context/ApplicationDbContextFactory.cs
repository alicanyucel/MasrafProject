using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MasrafProject.Application.Interfaces;

namespace MasrafProject.Infrastructure.Context;

internal sealed class DesignTimeTenantProvider : ITenantProvider
{
    public int TenantId => 0;
    public void SetTenantId(int tenantId) { }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    ApplicationDbContext IDesignTimeDbContextFactory<ApplicationDbContext>.CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("Default")
            ?? Environment.GetEnvironmentVariable("ConnectionStrings__Default")
            ?? "Data Source=DESKTOP-L6NJT48\\SQLEXPRESS;Initial Catalog=MasrafDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        optionsBuilder.UseSqlServer(connectionString);
        
        var tenantProvider = new DesignTimeTenantProvider();
        return new ApplicationDbContext(optionsBuilder.Options, tenantProvider);
    }
}
