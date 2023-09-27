using Microsoft.AspNetCore.Identity;

namespace RecipeWebApp.Seeds
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> SeedDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedUsers(services);

            return app;
        }

        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


            IdentityResult roleResult;

            var roleExist = await roleManager.RoleExistsAsync("Admin");

            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

        }

        private static async Task SeedUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (await userManager.FindByNameAsync
                           ("sonqkazakova@gmail.com") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "sonqkazakova@gmail.com";
                user.Email = "sonqkazakova@gmail.com";

                var result = await userManager.CreateAsync
                (user, "690918");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Admin").Wait();
                }
            }
        }
    }
}
