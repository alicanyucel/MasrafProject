namespace MasrafProject.Application.Interfaces;

public interface ITenantProvider
{
    Guid TenantId { get; }
    void SetTenantId(Guid tenantId);
}
