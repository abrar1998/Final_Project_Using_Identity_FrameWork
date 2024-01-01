using FullProjectUsingIdentity.DataBaseContext;
using FullProjectUsingIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullProjectUsingIdentity.Controllers
{
    public class TeacherController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly AccountContext context;

        public TeacherController(UserManager<ApplicationUser> userManager, AccountContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet]
        public IActionResult SaquibTeacher()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> StudentsofSaquib()
        {
            var userLogin = await userManager.FindByNameAsync(User.Identity.Name);
            var data = await context.Students.Where(x=>x.SelectTeacher == userLogin.Id).Include(x=>x.Teacher).ToListAsync();

            return View(data);
        }

        [HttpGet]
        public IActionResult UzairTeacher()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> StudentsofUzair()
        {
            var userLogin = await userManager.FindByNameAsync(User.Identity.Name);
            var data = await context.Students.Where(x => x.SelectTeacher == userLogin.Id).Include(x => x.Teacher).ToListAsync();

            return View(data);
           
        }

        [HttpGet]
        public IActionResult UmmerTeacher()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> StudentsofUmmer()
        {
            var userLogin = await userManager.FindByNameAsync(User.Identity.Name);
            var data = await context.Students.Where(x => x.SelectTeacher == userLogin.Id).Include(x => x.Teacher).ToListAsync();

            return View(data);
        }


        //Perform Crud on Student Table
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await context.Students.FindAsync(id);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(StudentModel std, int id)
        {
            var data = await context.Students.FindAsync(std.Id);
            if(data !=null)
            {
                context.Students.Remove(data);
                context.SaveChanges();
                return RedirectToAction("StudentsofSaquib", "Teacher");
            }
            return View(data);

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var data = await context.Students.FindAsync(id);
            if(data !=null)
            {
                return View(data);
            }    
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await context.Students.FindAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentModel std)
        {
            var loggedInTeacherId = userManager.GetUserId(User);
            var originalStudent = context.Students.Find(std.Id);

            if(originalStudent !=null & originalStudent.SelectTeacher == loggedInTeacherId)
            {
                originalStudent.Name = std.Name; 
                originalStudent.Email = std.Email;
                originalStudent.Class = std.Class;

                await context.SaveChangesAsync();
                return RedirectToAction("StudentsofSaquib", "Teacher");
            }



            return View(std);
        }
    }
}
