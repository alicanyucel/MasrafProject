using MasrafProject.Application.Interfaces;

namespace MasrafProject.WebAPI.Middlewares;

public class TenantResolutionMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolutionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITenantProvider tenantProvider)
    {
        int? tenantId = null;

        if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var headerVal) &&
            int.TryParse(headerVal.FirstOrDefault(), out var parsed))
        {
            tenantId = parsed;
        }
        else if (context.User?.Identity?.IsAuthenticated == true)
        {
            var claim = context.User.FindFirst("tenant")
                        ?? context.User.FindFirst("tenant_id")
                        ?? context.User.FindFirst("tid");

            if (claim is not null && int.TryParse(claim.Value, out parsed))
            {
                tenantId = parsed;
            }
        }

        if (tenantId is null)
        {
            
        }

        tenantProvider.SetTenantId(tenantId ?? 0);
        await _next(context);
    }
}