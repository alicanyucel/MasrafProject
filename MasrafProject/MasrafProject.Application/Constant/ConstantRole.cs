
using MasrafProject.Domain.Entities;

namespace MasrafProject.Application.Constant;

public static class RoleNames
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const string Manager = "Manager";
}

public static class ConstantsRole
{
    public static List<AppRole> GetRoles()
    {
        return new List<AppRole>
        {
            new AppRole
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = RoleNames.Admin,
                NormalizedName = RoleNames.Admin.ToUpperInvariant()
            },
            new AppRole
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = RoleNames.User, 
                NormalizedName = RoleNames.User.ToUpperInvariant()
            },
            new AppRole
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = RoleNames.Manager,
                NormalizedName = RoleNames.Manager.ToUpperInvariant()
            }
        };
    }
}