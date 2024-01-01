using FullProjectUsingIdentity.DataBaseContext;
using FullProjectUsingIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullProjectUsingIdentity.Controllers
{
    [Authorize(Roles =RolesClasses.Admin)]
    public class AdminController : Controller
    {
        private readonly AccountContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(AccountContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult AdminDashBoard()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var data = await userManager.Users.ToListAsync();
            if(data !=null)
            {
                return View(data);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> StudentList()
        {
            var data = await context.Students.Include(x=>x.Teacher).ToListAsync();
            if(data!=null)
            {
                return View(data);
            }

            return View();
        }
    }
}
