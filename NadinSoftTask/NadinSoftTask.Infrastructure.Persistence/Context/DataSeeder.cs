//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;

//namespace NadinSoftTask.Infrastructure.Persistence.Context
//{
//    public static class DataSeeder
//    {
//        public static void Seed(this IServiceProvider serviceProvider)
//        {
//            try
//            {
//                using (var scope = serviceProvider.CreateScope())
//                {
//                    var context = serviceProvider.GetRequiredService<IdentityContext>();
//                    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

//                    context.Database.Migrate();

//                    CreateRolesIfNotExists(context);
//                    CreateAdminUserIfNotExists(userManager);
//                }
//            }
//            catch (Exception exception)
//            {
//                Console.WriteLine(exception.Message);
//            }
//        }

//        #region PrivateMethods

//        private static void CreateRolesIfNotExists(IdentityContext context)
//        {
//            if (!context.Roles.Any(q => q.Name == "admin"))
//                context.Roles.Add(new IdentityRole("admin") { NormalizedName = "admin" });

//            context.SaveChanges();
//        }

//        private static void CreateAdminUserIfNotExists(UserManager<IdentityUser> userManager)
//        {
//            const string username = "Admin";
//            const string password = "123456";

//            if (userManager.FindByNameAsync(username).Result == null)
//            {
//                var admin = new IdentityUser(username);
//                var result = userManager.CreateAsync(admin, password).Result;
//                if (result.Succeeded)
//                    userManager.AddToRoleAsync(admin, "admin").Wait();
//            }
//        }

//        #endregion
//    }
//}
