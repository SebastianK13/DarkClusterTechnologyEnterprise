using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarkClusterTechnologyEnterprise.Models;
using DarkClusterTechnologyEnterprise.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DarkClusterTechnologyEnterprise.Controllers
{
    public class EmployeeInformationController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;
        private IEmployeeRepository repository;

        public EmployeeInformationController(RoleManager<IdentityRole> roleMgr,
            UserManager<IdentityUser> userMrg, IEmployeeRepository repository)
        {
            roleManager = roleMgr;
            userManager = userMrg;
            this.repository = repository;
        }
        private int PageSize = 10;
        [Authorize(Roles = "HRStandard, HRSuper, Admin")]
        public async Task<IActionResult> ShowPrivileges(int page = 1)
        {
            var users = userManager.Users.ToList();
            List<Employee> employee = repository.GetEmployees(users.Select(u => u.Id).ToList());
            List<EmployeeDetails> employeeDetails = new List<EmployeeDetails>();
            for (int i = 0; i < users.Count(); i++)
            {
                var r = await userManager.GetRolesAsync(users[i]);
                if (r.Count() > 0)
                {
                    employeeDetails.Add(new EmployeeDetails
                    {
                        Login = users[i].UserName,
                        RoleName = r[0].ToString(),
                        DepartmentName = employee[i].Department.DepartmentName,
                        Firstname = employee[i].Firstname,
                        Lastname = employee[i].Surname
                    });
                }
            }

            PageInfo pages = new PageInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = employeeDetails.Count()
            };

            PrivilegesViewModel model = new PrivilegesViewModel
            {
                Employee = employeeDetails.Skip((page - 1) * PageSize).Take(PageSize),
                PagesInfo = pages
            };
            return View(model);
        }
        public async Task<IActionResult> ShowEarnings(int page = 1)
        {
            var users = userManager.Users.OrderBy(i=>i.Id);
            List<LoginByID> loginByIDs = new List<LoginByID>();
            foreach (var u in users)
            {
                loginByIDs.Add(new LoginByID { 
                    Login = u.UserName,
                    Id = u.Id
                });
            }

            PageInfo pages = new PageInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = loginByIDs.Count()
            };

            var earnings = repository.GetEarnings(loginByIDs).Skip((page - 1) * PageSize).Take(PageSize);

            EarningsViewModel model = new EarningsViewModel 
            { 
                employeesEarnings = earnings,
                PagesInfo = pages
            };

            return View(model);
        }
    }
}