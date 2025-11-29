using MasrafProject.Application.Interfaces;

namespace MasrafProject.Application.Services;

public sealed class TenantProvider : ITenantProvider
{
    private Guid _tenantId;
    public Guid TenantId => _tenantId;
    public void SetTenantId(Guid tenantId) => _tenantId = tenantId;
}
