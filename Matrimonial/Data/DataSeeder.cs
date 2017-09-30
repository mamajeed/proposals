using Matrimonial.context;
using Matrimonial.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matrimonial.Data
{
    public class DataSeeder
    {
        private readonly DatabaseContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;

        public DataSeeder(DatabaseContext ctx, UserManager<ApplicationUser> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.Users.Any())
            {

                var user = new ApplicationUser()
                {
                    Email = "bob@aol.com",
                    UserName = "bob@aol.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                }
            }
        }
    }
}
