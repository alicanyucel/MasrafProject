using MasrafProject.Application.Constant;
using MasrafProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MasrafProject.WebAPI.Middlewares;

public static class ExtensionsMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
            foreach (var role in ConstantsRole.GetRoles())
            {
                if (!roleManager.RoleExistsAsync(role.Name!).GetAwaiter().GetResult())
                {
                    roleManager.CreateAsync(new AppRole
                    {
                        Id = role.Id,
                        Name = role.Name,
                        NormalizedName = role.Name!.ToUpperInvariant()
                    }).GetAwaiter().GetResult();
                }
            }

            var admin = userManager.Users.FirstOrDefault(p => p.UserName == "admin");
            if (admin is null)
            {
                AppUser user = new()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "Mudbey",
                    LastName = "Yazılım",
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(user, "Mudbey123.").GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    
                    userManager.AddToRoleAsync(user, RoleNames.Admin).GetAwaiter().GetResult();
                }
            }
            else
            {
              
                var roles = userManager.GetRolesAsync(admin).GetAwaiter().GetResult();
                if (!roles.Contains(RoleNames.Admin))
                {
                    userManager.AddToRoleAsync(admin, RoleNames.Admin).GetAwaiter().GetResult();
                }
            }
        }
    }
}
