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
        List<DateTime> month = new List<DateTime>();
        private void createMonth(ref List<DateTime> month)
        {
            for (int i = 1; i <= 31; i++)
            {
                month.Add(DateTime.Now.ToUniversalTime().AddDays(+i).Date + new TimeSpan(0, 0, 0));
            }
        }

        IEmployeeRepository repository;
        public ScheduleController(IEmployeeRepository repo)
        {
            repository = repo;
            createMonth(ref month);
        }
        public async Task<IActionResult> TaskSchedule()
        {
            string? username = User.Identity.Name;
            int eId = await repository.GetEmployeeID(username);
            SingleTask task = new SingleTask(repository.GetAllTasks(eId));
            TaskViewModel model = new TaskViewModel(task.Tasks, month);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> NewTask(NewTask newTask)
        {
            string? username = User.Identity.Name;
            await repository.CreateTasks(newTask, username);

            return RedirectToAction("TaskSchedule");
        }

    }
}