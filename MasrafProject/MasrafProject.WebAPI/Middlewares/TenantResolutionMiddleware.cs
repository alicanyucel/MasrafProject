using MasrafProject.Application.Interfaces;

namespace MasrafProject.WebAPI.Middlewares;

public sealed class TenantResolutionMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolutionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITenantProvider tenantProvider)
    {
        Guid tenantId = Guid.Empty;
        
       
        if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var headerVal)
            && Guid.TryParse(headerVal.FirstOrDefault(), out var parsed))
        {
            tenantId = parsed;
        }
      
        else if (context.User?.Identity?.IsAuthenticated == true)
        {
            var claim = context.User.FindFirst("tenant") ?? context.User.FindFirst("tenant_id") ?? context.User.FindFirst("tid");
            if (claim is not null && Guid.TryParse(claim.Value, out parsed))
            {
                tenantId = parsed;
            }
        }

        tenantProvider.SetTenantId(tenantId);

        await _next(context);
    }
}
