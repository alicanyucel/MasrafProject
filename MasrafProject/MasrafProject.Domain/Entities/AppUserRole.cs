using Microsoft.AspNetCore.Identity;

namespace MasrafProject.Domain.Entities;

public sealed class AppUserRole : IdentityUserRole<Guid>
{
    public bool IsDelete { get; set; } = false;
}
