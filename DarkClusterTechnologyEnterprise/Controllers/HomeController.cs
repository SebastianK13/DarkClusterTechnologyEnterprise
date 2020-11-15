using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DarkClusterTechnologyEnterprise.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DarkClusterTechnologyEnterprise.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {

            //return await CreateAccount();
            //return await Create();
            return View();
        }

        private IEmployeeRepository repository;
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private RoleManager<IdentityRole> roleManager;

        public HomeController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IEmployeeRepository repository,
            RoleManager<IdentityRole> roleMgr)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.repository = repository;
            this.roleManager = roleMgr;

        }

        private List<RoleChangerModel> roles = new List<RoleChangerModel>
        {
           new RoleChangerModel{ RoleName = "HRSuper", Username = "1000002"},
           new RoleChangerModel{ RoleName = "HRStandard", Username = "1000003"},
           new RoleChangerModel{ RoleName = "HRStandard", Username = "1000004"},
           new RoleChangerModel{ RoleName = "AccountantSuper", Username = "2000005"},
           new RoleChangerModel{ RoleName = "AccountantStandard", Username = "2000006"},
           new RoleChangerModel{ RoleName = "MarketingSuper", Username = "3000007"},
           new RoleChangerModel{ RoleName = "MarketingStandard", Username = "3000008"},
           new RoleChangerModel{ RoleName = "Admin", Username = "4000009"},
           new RoleChangerModel{ RoleName = "Admin", Username = "4000010"},
           new RoleChangerModel{ RoleName = "Admin", Username = "4000011"},
           new RoleChangerModel{ RoleName = "ResearchSuper", Username = "5000012"},
           new RoleChangerModel{ RoleName = "ResearchStandard", Username = "5000013"},
           new RoleChangerModel{ RoleName = "PurchasingSuper", Username = "6000014"},
           new RoleChangerModel{ RoleName = "PurchasingStandard", Username = "6000015"},
           new RoleChangerModel{ RoleName = "ControllingSuper", Username = "7000016"},
           new RoleChangerModel{ RoleName = "ControllingStandard", Username = "7000017"},
           new RoleChangerModel{ RoleName = "EngeneeringSuper", Username = "8000018"},
           new RoleChangerModel{ RoleName = "EngeneeringStandard", Username = "8000019"}
        };
        private List<string> usersLogin = new List<string>
        {
            "1000003",
            "1000004",
            "1000005",
            "2000006",
            "2000007",
            "3000008",
            "3000009",
            "4000010",
            "4000011",
            "4000012",
            "5000013",
            "5000014",
            "6000015",
            "6000016",
            "7000017",
            "7000018",
            "8000019",
            "8000020",
        };
        public async Task<IActionResult> Create()
        {
            foreach (var r in roles)
            {
                IdentityResult result
                    = await roleManager.CreateAsync(new IdentityRole(r.RoleName));
            }

            return await UserToRole();
        }

        public async Task<IActionResult> UserToRole()
        {
            IdentityResult result;
            foreach (var r in roles)
            {
                IdentityUser user = await userManager.FindByNameAsync(r.Username);
                result = await userManager.AddToRoleAsync(user,
                    r.RoleName);
            }

            return View("Index");
        }

        //How to create Login?
        //HR - 1 + XXXXXX
        //AaF - 2 + XXXXXX
        //M - 3 + XXXXXX
        //IT - 4 + XXXXXX
        //RaD - 5 + XXXXXX
        //P - 6 + XXXXXX
        //C - 7 + XXXXXX
        //E - 8 + XXXXXX
        //Example first USER ever login who work in HR dept. : 1000000
        private List<string> adminUser = new List<string> { "", "Accountant", "Director", "SalarySpec", "Admin" };
        private List<string> depts = new List<string>
        {
            "",
            "Human Resources",
            "Human Resources",
            "Human Resources",
            "Accounting and Finance",
            "Accounting and Finance",
            "Marketing",
            "Marketing",
            "IT",
            "IT",
            "IT",
            "Research and Development",
            "Research and Development",
            "Purchasing",
            "Purchasing",
            "Controlling",
            "Controlling",
            "Engeneering",
            "Engeneering"
        };
        private const string adminPassword = "Aa123456.";

        public async Task<IActionResult> CreateAccount()
        {
            int count = 0;
            List<Employee> employees = new List<Employee>
            {
                new Employee{ Firstname="Frank", Surname="Maverick", Position="Chairman"},

                new Employee{ Firstname="Lizette", Surname="Madoline", Position="HR Chief Department"},
                new Employee{ Firstname="Israel", Surname="Clementine", Position="HR Manager"},
                new Employee{ Firstname="Elwyn", Surname="Alton", Position="HR Specialist"},

                new Employee{ Firstname="Azaela", Surname="Damon", Position="Accounting and Finance Chief Department"},
                new Employee{ Firstname="Wayland", Surname="Nolan", Position="Accountant"},

                new Employee{ Firstname="Emil", Surname="Calvin", Position="Marketing Chief Department"},
                new Employee{ Firstname="Roland", Surname="Harper", Position="Marketing Specialist"},

                new Employee{ Firstname="Ronny", Surname="Cecelia", Position="IT Chief Department"},
                new Employee{ Firstname="David", Surname="Wilton", Position="IT Specialist"},
                new Employee{ Firstname="Will", Surname="Thom", Position="IT Specialist"},

                new Employee{ Firstname="Peggie", Surname="Adriana", Position="Research and Development Chief Department"},
                new Employee{ Firstname="Nikolas", Surname="Rodney", Position="Lab. Technician"},

                new Employee{ Firstname="Scherilyn", Surname="Linda", Position="Purchasing Chief Department"},
                new Employee{ Firstname="Brenden", Surname="Wayne", Position="Purchase Specialist"},

                new Employee{ Firstname="Abbie", Surname="Giffard", Position="Controlling Chief Department"},
                new Employee{ Firstname="Leroy", Surname="Walker", Position="Controller"},

                new Employee{ Firstname="Boone", Surname="Jake", Position="Engeneering Chief Department"},
                new Employee{ Firstname="Keegan", Surname="Percival", Position="Engeneer"},

            };
            List<Department> departments = new List<Department>
            {
                new Department{ DepartmentName="Human Resources"},
                new Department{ DepartmentName="Accounting and Finance"},
                new Department{ DepartmentName="Marketing"},
                new Department{ DepartmentName="IT"},
                new Department{ DepartmentName="Research and Development"},
                new Department{ DepartmentName="Purchasing"},
                new Department{ DepartmentName="Controlling"},
                new Department{ DepartmentName="Engeneering"}
            };
            List<Location> locations = new List<Location>
            {
                new Location{ City = "Cincinati", Country="USA", StreetAddress="2097 Cerullo Road", Room="10", ZipCode="45224"},
                new Location{ City = "Cincinati", Country="USA", StreetAddress="2097 Cerullo Road", Room="14C", ZipCode="45224"},
                new Location{ City = "Cincinati", Country="USA", StreetAddress="2097 Cerullo Road", Room="12A", ZipCode="45224"},
                new Location{ City = "Cincinati", Country="USA", StreetAddress="2097 Cerullo Road", Room="13B", ZipCode="45224"},
                new Location{ City = "Emporia", Country="USA", StreetAddress="877  Jacobs Street", Room="131C", ZipCode="23847"},
                new Location{ City = "Emporia", Country="USA", StreetAddress="877  Jacobs Street", Room="126D", ZipCode="23847"},
                new Location{ City = "Springfield", Country="USA", StreetAddress="1230  Kinney Street", Room="43", ZipCode="01109"},
                new Location{ City = "Springfield", Country="USA", StreetAddress="1230  Kinney Street", Room="40", ZipCode="01109"},
                new Location{ City = "Springfield", Country="USA", StreetAddress="1230  Kinney Street", Room="35", ZipCode="01109"},
                new Location{ City = "Springfield", Country="USA", StreetAddress="1230  Kinney Street", Room="31", ZipCode="01109"},
                new Location{ City = "Springfield", Country="USA", StreetAddress="1230  Kinney Street", Room="32A", ZipCode="01109"},
                new Location{ City = "Springfield", Country="USA", StreetAddress="1230  Kinney Street", Room="33B", ZipCode="01109"},
                new Location{ City = "Springfield", Country="USA", StreetAddress="1230  Kinney Street", Room="33C", ZipCode="01109"},
                new Location{ City = "Manhattan", Country="USA", StreetAddress="4589  Oakwood Avenue", Room="24B", ZipCode="10016"},
                new Location{ City = "Manhattan", Country="USA", StreetAddress="4589  Oakwood Avenue", Room="34A", ZipCode="10016"},
                new Location{ City = "Manhattan", Country="USA", StreetAddress="4589  Oakwood Avenue", Room="28", ZipCode="10016"},
                new Location{ City = "Manhattan", Country="USA", StreetAddress="4589  Oakwood Avenue", Room="5C", ZipCode="10016"},
                new Location{ City = "Manhattan", Country="USA", StreetAddress="4589  Oakwood Avenue", Room="31D", ZipCode="10016"},
                new Location{ City = "Manhattan", Country="USA", StreetAddress="4589  Oakwood Avenue", Room="9", ZipCode="10016"},
            };

            List<Earnings> earnings = new List<Earnings> 
            { 
                new Earnings{ Currency = "USD", Salary = 1500, Premium = 350},
                new Earnings{ Currency = "USD", Salary = 1700, Premium = 550},
                new Earnings{ Currency = "USD", Salary = 2500, Premium = 550},
                new Earnings{ Currency = "USD", Salary = 4300, Premium = 250},
                new Earnings{ Currency = "USD", Salary = 1550, Premium = 450},
                new Earnings{ Currency = "USD", Salary = 2400, Premium = 250},
                new Earnings{ Currency = "USD", Salary = 1800, Premium = 650},
                new Earnings{ Currency = "USD", Salary = 1900, Premium = 750},
                new Earnings{ Currency = "USD", Salary = 2800, Premium = 150},
                new Earnings{ Currency = "USD", Salary = 4100, Premium = 250},
                new Earnings{ Currency = "USD", Salary = 5000, Premium = 350},
                new Earnings{ Currency = "USD", Salary = 1300, Premium = 450},
                new Earnings{ Currency = "USD", Salary = 1200, Premium = 550},
                new Earnings{ Currency = "USD", Salary = 3500, Premium = 650},
                new Earnings{ Currency = "USD", Salary = 3100, Premium = 750},
                new Earnings{ Currency = "USD", Salary = 2600, Premium = 150},
                new Earnings{ Currency = "USD", Salary = 1800, Premium = 250},
                new Earnings{ Currency = "USD", Salary = 1450, Premium = 350},
                new Earnings{ Currency = "USD", Salary = 1100, Premium = 450},
            };

            CreateAllDepartments(departments);

            for (int i = 0; i < employees.Count(); i++)
            {
                string login = CreateLogin(depts[i]);
                IdentityUser user = await userManager.FindByNameAsync(login);
                if (user == null)
                {
                    user = new IdentityUser(login);
                    await userManager.CreateAsync(user, adminPassword);
                }
                repository.CreateLocations(locations[i]);
                int deptID = repository.GetDepartmentId(depts[i]);
                if (deptID != 0)
                {
                    employees[i].DepartmentId = deptID;
                }
                employees[i].LocationId = repository.GetLastLocation();
                employees[i].UserId = user.Id;
                Employee employee = repository.CreateEmployee(employees[i]);
                //earnings[i].EmployeeId = employee.EmployeeId; Database reorganize foreignkey removed from employee table
                repository.CreateEarnings(earnings[i]);
                if (employee.Position.Contains("Chief"))
                {
                    repository.SetChief(count, employee.EmployeeId);
                    count++;
                }
                //repository.CreatePresences();
                //repository.CreatTasks();
            }
            repository.SetSuperior();

            return RedirectToAction("Create");
        }
        private string CreateLogin(string departmentName)
        {
            //Chairman code
            string departmentCode = "9";
            switch (departmentName)
            {
                case "Human Resources":
                    departmentCode = "1";
                    break;
                case "Accounting and Finance":
                    departmentCode = "2";
                    break;
                case "Marketing":
                    departmentCode = "3";
                    break;
                case "IT":
                    departmentCode = "4";
                    break;
                case "Research and Development":
                    departmentCode = "5";
                    break;
                case "Purchasing":
                    departmentCode = "6";
                    break;
                case "Controlling":
                    departmentCode = "7";
                    break;
                case "Engeneering":
                    departmentCode = "8";
                    break;
            }
            int lastId = repository.GetLastEmployeeId() + 1;
            departmentCode += "000000";
            lastId += Convert.ToInt32(departmentCode);
            return lastId.ToString();
        }
        private void CreateAllDepartments(List<Department> departments)
        {
            foreach (var d in departments)
            {
                repository.CreateDepartments(d);
            }
        }
    }
}