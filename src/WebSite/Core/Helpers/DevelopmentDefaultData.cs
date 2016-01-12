using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.Extensions.OptionsModel;

using BusinessLogic.Models;
using BusinessLogic.Helpers;

namespace WebSite.Core.Helpers
{
    using Config;

    public class DevelopmentDefaultData
    {
        private DevelopmentSettings settings;
        private IDataContext dataContext;
        private UserManager<User> userManager;
        private RoleManager<Role> roleManager;

        public DevelopmentDefaultData(IOptions<DevelopmentSettings> options, IDataContext dataContext, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.settings = options.Value;
            this.dataContext = dataContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// Creates default data is no database exists.
        /// </summary>
        /// <returns></returns>
        public async Task CreateIfNoDatabaseExists()
        {
            if (!dataContext.Database.EnsureCreated())
                return;

            await CreateRoles();
            await CreateAdminUser();
        }

        /// <summary>
        /// Creates default roles.
        /// </summary>
        private async Task CreateRoles()
        {
            if (!await roleManager.RoleExistsAsync(RoleNames.Admin))
            {
                var result = await roleManager.CreateAsync(new Role(RoleNames.Admin));

                if (!result.Succeeded)
                    throw new Exception("Error creating admin role: " + result.Errors.FirstOrDefault().Description);
            }

            if (!await roleManager.RoleExistsAsync(RoleNames.User))
            {
                var result = await roleManager.CreateAsync(new Role(RoleNames.User));

                if (!result.Succeeded)
                    throw new Exception("Error creating user role: " + result.Errors.FirstOrDefault().Description);
            }
        }

        /// <summary>
        /// Creates default admin user.
        /// </summary>
        private async Task CreateAdminUser()
        {
            var user = await userManager.FindByNameAsync(settings.DefaultAdminUserEmail);

            if (user != null)
                return;

            user = new User { UserName = settings.DefaultAdminUserEmail, Email = settings.DefaultAdminUserEmail, FirstName = "Admin", LastName = "Admin", CreateDate = DateTime.Now };

            var result = await userManager.CreateAsync(user, settings.DefaultAdminUserPassword);

            if (!result.Succeeded)
                throw new Exception("Error creating default admin user: " + result.Errors.FirstOrDefault().Description);

            result = await userManager.AddToRoleAsync(user, RoleNames.Admin);

            if (!result.Succeeded)
                throw new Exception("Error adding default admin user to the role: " + result.Errors.FirstOrDefault().Description);

            //await userManager.AddClaimAsync(user, new Claim("ManageStore", "Allowed"));
        }
    }
}
