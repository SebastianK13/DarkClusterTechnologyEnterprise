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
        ScheduleController(IEmployeeRepository repo)
        {
            repository = repo;
        }
        public IActionResult TaskSchedule()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewTask(NewTask newTask)
        {
            int eId = Convert.ToInt32(HttpContext.User.Identity.Name);
            repository.CreateTasks(newTask, eId);

            return RedirectToAction("TaskSchedule");
        }
    }
}