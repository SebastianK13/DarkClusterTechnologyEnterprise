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

        List<DateTime> month = new List<DateTime>();
        private void createMonth(ref List<DateTime> month, int eId)
        {
            for (int i = 0; i <= 30; i++)
            {
                month.Add(repository.ConvertToLocal(DateTime.UtcNow.AddDays(+i), eId));
            }
        }
        public IActionResult SubordinateSchedule(int employeeId = 0)
        {
            createMonth(ref month, employeeId);
            SingleTask task = new SingleTask(repository.GetAllTasks(employeeId));
            var tasks = SplitTask(task);
            SubordinateTaskView employeeTasks = new SubordinateTaskView(tasks, month);

            return Ok(employeeTasks);
        }
        public async Task<IActionResult> TaskSchedule()
        {
            string? username = User.Identity.Name;
            int eId = await repository.GetEmployeeID(username);
            createMonth(ref month, eId);
            SingleTask task = new SingleTask(repository.GetAllTasks(eId));
            var tasks = SplitTask(task);
            var subordinates = repository.GetSubordinates(eId);

            TaskViewModel model = new TaskViewModel(tasks , month, subordinates);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> NewTask(NewTask newTask, int Subordinates)
        {
            if (ModelState.IsValid)
                await repository.CreateTasks(newTask, Subordinates);

            return RedirectToAction("TaskSchedule");
        }

        private List<SingleTask> SplitTask(SingleTask tasks)
        {
            List<SingleTask> tasksSplit = new List<SingleTask>();
            foreach (var t in tasks.Tasks)
            {
                double numOfDays = (t.TaskDeadline.Date - t.TaskBegin.Date).TotalDays;
                if (numOfDays <= 1)
                {
                    if(t.TaskBegin.Date == t.TaskDeadline.Date)
                    {
                        tasksSplit.Add(t);
                    }
                    else
                    {
                        DateTime midnight = t.TaskBegin.Date.AddDays(1).AddSeconds(-1);
                        DateTime nextDay = t.TaskBegin.Date.AddDays(1);

                        tasksSplit.Add( new SingleTask { 
                            Task = t.Task,
                            TaskDesc = t.TaskDesc,
                            TaskBegin = t.TaskBegin,
                            TaskDeadline = midnight,
                            TaskId = t.TaskId
                        });

                        tasksSplit.Add(new SingleTask
                        {
                            Task = t.Task,
                            TaskDesc = t.TaskDesc,
                            TaskBegin = nextDay,
                            TaskDeadline = t.TaskDeadline,
                            TaskId = t.TaskId
                        });
                    }
                    
                }
            }
            tasksSplit.Sort((x,y) => DateTime.Compare(x.TaskBegin,y.TaskBegin));

            return tasksSplit;
        }

    }
}