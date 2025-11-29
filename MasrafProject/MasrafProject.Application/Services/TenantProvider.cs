using MasrafProject.Application.Interfaces;

namespace MasrafProject.Application.Services;

public sealed class TenantProvider : ITenantProvider
{
    private int _tenantId;
    public int TenantId => _tenantId;
    public void SetTenantId(int tenantId) => _tenantId = tenantId;
}
