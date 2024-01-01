using FullProjectUsingIdentity.Models;
using Microsoft.AspNetCore.Identity;

namespace FullProjectUsingIdentity.AccountRepository
{
    public class AccountRepo:IAccountRepo
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountRepo(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task<IdentityResult> UserRegisteration(RegisterUserModel umodel)
        {
            var user = new ApplicationUser
            {
                UserName = umodel.Email,
                Email = umodel.Email,
                Name = umodel.Name,
            };

            var result = await userManager.CreateAsync(user, umodel.Password);
            if(!string.IsNullOrEmpty(umodel.Roles.ToString()))
            {
                await userManager.AddToRoleAsync(user, umodel.Roles.ToString());
            }
            else
            {
                await userManager.AddToRoleAsync(user, RolesClasses.Teacher);
            }

            return result;
        }

        public async Task<SignInResult> UserLogin(LoginUserModel lmodel)
        {
            var result = await signInManager.PasswordSignInAsync(lmodel.Email, lmodel.Password, lmodel.RememberMe, false);
            return result;
        }

        public async Task LogOut()
        {
            await signInManager.SignOutAsync();
        }
    }
}
