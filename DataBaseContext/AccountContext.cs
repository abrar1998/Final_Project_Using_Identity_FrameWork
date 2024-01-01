using FullProjectUsingIdentity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FullProjectUsingIdentity.DataBaseContext
{
    public class AccountContext:IdentityDbContext<ApplicationUser>
    {
        public AccountContext(DbContextOptions<AccountContext> opt):base(opt)
        {
            
        }

        public DbSet<StudentModel> Students { get; set; }

    }
}
