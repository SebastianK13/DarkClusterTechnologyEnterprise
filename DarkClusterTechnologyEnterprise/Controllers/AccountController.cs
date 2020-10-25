using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarkClusterTechnologyEnterprise.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DarkClusterTechnologyEnterprise.Controllers
{
    public class AccountController : Controller
    {
        private IEmployeeRepository repository;
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IEmployeeRepository repository)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.repository = repository;

        }
        [AllowAnonymous]
        public ViewResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect("/Board/MainBoard");
                    }
                }
            }
            ModelState.AddModelError("", "Incorect username or password");
            return View(loginModel);
        }


        //private List<string> adminUser = new List<string> { "HrManager", "Accountant", "Director", "SalarySpec", "Admin" };
        //private const string adminPassword = "Aa123456.";

        //[HttpPost]
        //public async Task<IActionResult> CreateAccount()
        //{
        //    foreach (var a in adminUser)
        //    {
        //        IdentityUser user = await userManager.FindByNameAsync(a);
        //        if (user == null)
        //        {
        //            user = new IdentityUser(a);
        //            //user.Email = "fsfgsgdsgsd";
        //            await userManager.CreateAsync(user, adminPassword);
        //        }
        //        //IdentityUser user2 = await userManager.FindByNameAsync(a);
        //        Employee employee = repository.CreateEmployee(user.Id);
        //        repository.CreateEarnings(employee.EmployeeId);
        //    }
        //    return View("Login");
        //}

        public async Task<RedirectResult> Logout(string returnUrl = "/Account/Login")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}