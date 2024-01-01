using Microsoft.AspNetCore.Identity;

namespace FullProjectUsingIdentity.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name {  get; set; }
    }
}
