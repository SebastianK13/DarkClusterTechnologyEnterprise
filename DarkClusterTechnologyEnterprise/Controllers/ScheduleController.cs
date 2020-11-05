using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarkClusterTechnologyEnterprise.Models;
using DarkClusterTechnologyEnterprise.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DarkClusterTechnologyEnterprise.Controllers
{
    public class ScheduleController : Controller
    {
        IEmployeeRepository repository;
        public ScheduleController(IEmployeeRepository repo)
        {
            repository = repo;
        }
        public IActionResult TaskSchedule()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewTask(NewTask newTask)
        {
            string? username = HttpContext.User.Identity.Name;
            await repository.CreateTasks(newTask, username);

            return RedirectToAction("TaskSchedule");
        }
    }
}