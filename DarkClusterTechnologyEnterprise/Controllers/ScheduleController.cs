﻿using System;
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
            for (int i = 0; i <= 30; i++)
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
            TaskViewModel model = new TaskViewModel(SplitTask(task), month);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> NewTask(NewTask newTask)
        {
            if (ModelState.IsValid)
            {
                string? username = User.Identity.Name;
                await repository.CreateTasks(newTask, username);
            }

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
            return tasksSplit;
        }

    }
}