using FullProjectUsingIdentity.AccountRepository;
using FullProjectUsingIdentity.DataBaseContext;
using FullProjectUsingIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace FullProjectUsingIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepo repo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AccountContext context;

        public AccountController(IAccountRepo repo, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AccountContext context)
        {
            this.repo = repo;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserModel umodel)
        {
            if(ModelState.IsValid)
            {
                var result = await repo.UserRegisteration(umodel);
                if(!result.Succeeded)
                {
                    foreach (var errors in result.Errors)
                    {
                        ModelState.AddModelError("", errors.Description);
                    }
                }
                else
                {
                    return RedirectToAction("LoginUser", "Account");
                }
            }
            else
            {
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public  IActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUserModel lmodel)
        {
            if (ModelState.IsValid)
            {
                var result = await repo.UserLogin(lmodel);
                if(result.Succeeded)
                {
                    if(User.IsInRole(RolesClasses.Admin))
                    {
                        return RedirectToAction("AdminDashBoard", "Admin");
                    }
                    else if(User.IsInRole(RolesClasses.Teacher))
                    {
                        var teacher = await userManager.FindByNameAsync(lmodel.Email);
                        if (teacher != null && teacher.Email == "saquib@gmail.com")
                        {
                            return RedirectToAction("SaquibTeacher", "Teacher");
                        }
                        else if(teacher.Email !=null && teacher.Email =="uzair@gmail.com")
                        {
                            return RedirectToAction("UzairTeacher", "Teacher");
                        }
                        else if (teacher.Email !=null && teacher.Email == "umer@gmail.com")
                        {
                            return RedirectToAction("UmmerTeacher", "Teacher");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }   
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
            }
            else
            {
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public IActionResult LogoutUser()
        {
            repo.LogOut();
            return RedirectToAction("LoginUser", "Account");
        }


        [HttpGet]
        public async Task<IActionResult> RegisterStudents()
        {
            ViewBag.data = await userManager.Users.ToListAsync();
            return View();
        }

        [HttpPost]
        public  IActionResult RegisterStudents(StudentModel std)
        {


            context.Students.Add(std);
            context.SaveChanges();
            return RedirectToAction("LoginUser", "Account");


            //here if i try to validate modelstate then i is not working 

            /*  if (ModelState.IsValid)
              {
                  await context.Students.AddAsync(std);
                  await context.SaveChangesAsync();
                  return RedirectToAction("LoginUser", "Account");
              }
              else
              {
                  ModelState.Clear();
              }

             return RedirectToAction("RegisterStudents", "Account");*/
        }




        //Adding Roles
        public string AddRoles()
        {
            roleManager.CreateAsync(new IdentityRole(RolesClasses.Admin)).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole(RolesClasses.Teacher)).GetAwaiter().GetResult();

            return "Roles Added Successfully";
        }


        //Access Denied
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
