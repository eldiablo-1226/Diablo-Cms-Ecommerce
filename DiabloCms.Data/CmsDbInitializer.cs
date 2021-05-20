using System.Threading.Tasks;
using DiabloCms.Entities.Models;
using DiabloCms.Shared.ConstContent;
using Microsoft.AspNetCore.Identity;

namespace DiabloCms.MsSql
{
    public static class CmsDbInitializer
    {
        public static async Task<IdentityResult> SeedData(UserManager<CmsUser> userManager,
            RoleManager<CmsRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);

            return IdentityResult.Success;
        }

        private static async Task SeedRoles(RoleManager<CmsRole> roleManager)
        {
            var exists = await roleManager.RoleExistsAsync(CmsUserRoles.AdminRole)
                .ConfigureAwait(false);
            if (!exists)
                await roleManager.CreateAsync(new CmsRole(CmsUserRoles.AdminRole))
                    .ConfigureAwait(false);
        }

        private static async Task SeedUsers(UserManager<CmsUser> userManager)
        {
            const string adminName = "admin";
            
            var usr = await userManager.FindByNameAsync(adminName)
                .ConfigureAwait(false);
            if (usr == null)
            {
                var user = new CmsUser
                {
                    UserName = adminName,
                    Email = "admin@admin.net",
                    FirstName = "admin",
                    LastName = "admin"
                };

                var result = await userManager.CreateAsync(user, "admin")
                    .ConfigureAwait(false);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, CmsUserRoles.AdminRole)
                        .ConfigureAwait(false);
            }
        }
    }
}