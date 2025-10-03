using MasrafProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MasrafProject.WebAPI.Middlewares;

public static class ExtensionsMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var db = scoped.ServiceProvider.GetRequiredService<MasrafProject.Infrastructure.Context.ApplicationDbContext>();
            db.Database.Migrate();

            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!userManager.Users.Any(p => p.UserName == "admin"))
            {
                AppUser user = new()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "Mudbey",
                    LastName = "Yazılım",
                    EmailConfirmed = true
                };

                userManager.CreateAsync(user, "Xr!92@Klm#2025_Secure").Wait();
            }
        }
    }
}
