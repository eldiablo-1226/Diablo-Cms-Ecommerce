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
            var usr = await userManager.FindByNameAsync("shaxzod12262000")
                .ConfigureAwait(false);
            if (usr == null)
            {
                var user = new CmsUser
                {
                    UserName = "shaxzod12262000",
                    Email = "shaxzod12262000@gmail.com",
                    FirstName = "Shahzod",
                    LastName = "Sayfiyev"
                };

                var result = await userManager.CreateAsync(user, "eldiablo1226")
                    .ConfigureAwait(false);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, CmsUserRoles.AdminRole)
                        .ConfigureAwait(false);
            }
        }
    }
}