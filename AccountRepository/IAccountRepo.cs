using FullProjectUsingIdentity.Models;
using Microsoft.AspNetCore.Identity;

namespace FullProjectUsingIdentity.AccountRepository
{
    public interface IAccountRepo
    {
        Task<IdentityResult> UserRegisteration(RegisterUserModel umodel);

        Task<SignInResult> UserLogin(LoginUserModel lmodel);

        Task LogOut();
    }
}
