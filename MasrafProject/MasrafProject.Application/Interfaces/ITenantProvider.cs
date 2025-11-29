namespace MasrafProject.Application.Interfaces;

public interface ITenantProvider
{
    int TenantId { get; }
    void SetTenantId(int tenantId);
}
